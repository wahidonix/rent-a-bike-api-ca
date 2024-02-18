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

        string[] roleNames = { "Admin", "User", "ServicePersonnel", "Manager" };
        foreach (var roleName in roleNames)
        {
            await EnsureRoleAsync(roleManager, roleName);
        }


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

        };

        foreach (var roleClaimPair in roleClaims)
        {
            var roleName = roleClaimPair.Key;
            var claims = roleClaimPair.Value;
            var role = await roleManager.FindByNameAsync(roleName);

            if (role == null)
            {

                continue; 
            }

            foreach (var claim in claims)
            {

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
                EmailConfirmed = true  
            };
            var adminPassword = "SecurePassword123!"; 
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
