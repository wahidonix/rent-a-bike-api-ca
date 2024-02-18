using Application.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;

    public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegistrationDto model)
    {
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "User");
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            return Ok();
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("", error.Description);
        }

        return BadRequest(ModelState);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto model)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                // Adjust the call to match the updated signature
                var token = await JwtTokenGenerator.GenerateJwtToken(_userManager, _configuration, user);
                return Ok(new { Token = token });
            }
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return BadRequest(ModelState);
    }


    [HttpPut("update/{userId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateUser(string userId, UserUpdateDto model)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound();

        user.Credits = model.Credits; // Update credits

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
        {
            return Ok();
        }

        return BadRequest("Failed to update user.");
    }


    [HttpPost("create-role")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateAdminOrServiceWorker(CreateUserDto model, string role)
    {
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            var resultRole = await _userManager.AddToRoleAsync(user, role);
            if (resultRole.Succeeded)
            {
                return Ok();
            }
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("", error.Description);
        }

        return BadRequest(ModelState);
    }

    [HttpGet("users-with-roles")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUsersWithRoles()
    {
        var usersWithRoles = new List<UserWithRolesDto>();

        var users = _userManager.Users.ToList();
        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            usersWithRoles.Add(new UserWithRolesDto
            {
                UserId = user.Id,
                Email = user.Email,
                Credits = user.Credits,
                Roles = roles
            });
        }

        return Ok(usersWithRoles);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] TokenRefreshRequestDto request)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshToken == request.RefreshToken && u.RefreshTokenExpiryTime > DateTime.Now);
        if (user == null)
        {
            return BadRequest("Invalid refresh token.");
        }

        var token = await JwtTokenGenerator.GenerateJwtToken(_userManager, _configuration, user);

        user.RefreshToken = await JwtTokenGenerator.GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
        await _userManager.UpdateAsync(user);

        return Ok(new
        {
            Token = token,
            RefreshToken = user.RefreshToken
        });
    }



}
