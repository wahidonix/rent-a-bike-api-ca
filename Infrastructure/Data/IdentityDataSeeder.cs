using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data;

public static class IdentityDataSeeder
{
    public static async Task SeedRolesAndAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        // List of default roles
        string[] roleNames = { "Admin", "User", "ServicePersonnel", "Manager" };
        foreach (var roleName in roleNames)
        {
            await EnsureRoleAsync(roleManager, roleName);
        }

        // Optionally seed role-based claims after roles have been created
        await SeedRoleClaimsAsync(roleManager);

        await EnsureAdminUserAsync(userManager);
    }

    private static async Task EnsureRoleAsync(RoleManager<IdentityRole> roleManager, string roleName)
    {
        var alreadyExists = await roleManager.RoleExistsAsync(roleName);
        if (!alreadyExists)
        {
            var result = await roleManager.CreateAsync(new IdentityRole(roleName));
            if (!result.Succeeded)
            {
                throw new Exception($"Failed to create role {roleName}");
            }
        }
    }

    public static async Task SeedRoleClaimsAsync(RoleManager<IdentityRole> roleManager)
    {
        // Define your roles and associated claims
        var roleClaims = new Dictionary<string, List<Claim>>()
        {
            { "Admin", new List<Claim>
                {
                    new Claim("Permission", "ManageEverything")
                }
            },
            { "ServicePersonnel", new List<Claim>
                {
                    new Claim("Permission", "ManageBikes"),
                    new Claim("Permission", "ManageStations")
                }
            },
            // Define other roles and their claims as needed
        };

        foreach (var roleClaimPair in roleClaims)
        {
            var roleName = roleClaimPair.Key;
            var claims = roleClaimPair.Value;
            var role = await roleManager.FindByNameAsync(roleName);

            if (role == null)
            {
                // Optionally log this situation or handle it as needed
                continue; // Skip if the role doesn't exist
            }

            foreach (var claim in claims)
            {
                // Note: This does not check for duplicate claims.
                // AddClaimAsync will not add a duplicate claim (same type and value) to the role.
                await roleManager.AddClaimAsync(role, claim);
            }
        }
    }

    private static async Task EnsureAdminUserAsync(UserManager<ApplicationUser> userManager)
    {
        var adminEmail = "admin@example.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true  // Assuming the admin's email is confirmed for demonstration purposes
            };
            var adminPassword = "SecurePassword123!"; // Consider a more secure way to handle the initial password
            var createUserResult = await userManager.CreateAsync(adminUser, adminPassword);

            if (createUserResult.Succeeded)
            {
                var addToRoleResult = await userManager.AddToRoleAsync(adminUser, "Admin");
                if (!addToRoleResult.Succeeded)
                {
                    throw new Exception("Failed to add the admin user to the Admin role.");
                }
            }
            else
            {
                throw new Exception("Failed to create the admin user.");
            }
        }
    }
}
