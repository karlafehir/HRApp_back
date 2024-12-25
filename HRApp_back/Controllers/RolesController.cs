using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRApp_back.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;

    public RolesController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("AssignRole")]
    public async Task<IActionResult> AssignRole(string email, string newRole)
    {
        // Find the user by email
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return NotFound("User not found.");
        }

        // Get the user's current roles
        var currentRoles = await _userManager.GetRolesAsync(user);

        // Remove all existing roles
        var removeRolesResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
        if (!removeRolesResult.Succeeded)
        {
            return BadRequest(removeRolesResult.Errors);
        }

        // Assign the new role
        var addRoleResult = await _userManager.AddToRoleAsync(user, newRole);
        if (!addRoleResult.Succeeded)
        {
            return BadRequest(addRoleResult.Errors);
        }

        return Ok($"User {user.Email} is now assigned to the role '{newRole}'.");
    }
}