using HRApp_back.DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HRApp_back.Helpers;

public static class IdentityEndpoints
{
    public static void MapIdentityEndpoints(this WebApplication app)
    {
        app.MapPost("/api/auth/login", async (UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, LoginDto loginDto, ApplicationDbContext dbContext) =>
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Results.NotFound("User not found.");

            var result = await signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);
            if (!result.Succeeded) return Results.Unauthorized();

            var roles = await userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "Employee";

            var employee = await dbContext.Employees.FirstOrDefaultAsync(e => e.Email == user.Email);

            return Results.Ok(new
            {
                userId = user.Id, // Always return the ASP.NET Identity userId
                employeeId = employee?.Id, // Return null if no employee record exists
                role
            });
        });



        // Register endpoint
        app.MapPost("/api/auth/register", async (UserManager<IdentityUser> userManager, RegisterDto registerDto) =>
        {
            var user = new IdentityUser
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return Results.BadRequest(result.Errors);

            return Results.Ok("User registered successfully.");
        });
    }
}

public class LoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class RegisterDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}
