using System.Threading;
using System.Threading.Tasks;
using eStore.Admin.Application.AuthDTOs;
using eStore.Admin.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Admin.Api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginCredentials loginCredentials,
        CancellationToken cancellationToken)
    {
        var token = await _authService.CreateTokenAsync(loginCredentials, cancellationToken);
        
        return Ok(token);
    }

    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> AddUser([FromBody] UserDto user, CancellationToken cancellationToken)
    {
        var isSuccess = await _authService.AddUserWithRolesAsync(user, cancellationToken);
        
        if (isSuccess)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpDelete]
    [Route("{username}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteUser(string username, CancellationToken cancellationToken)
    {
        var isSuccess = await _authService.DeleteUserAsync(username, cancellationToken);
        
        if (isSuccess)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpPut]
    [Route("{username}/remove-role")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> RemoveRoleFromUser(string username, [FromBody] string role,
        CancellationToken cancellationToken)
    {
        var isSuccess = await _authService.RemoveRoleFromUserAsync(username, role, cancellationToken);
        
        if (isSuccess)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpPut]
    [Route("{username}/add-role")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> AddRoleToUser(string username, [FromBody] string role,
        CancellationToken cancellationToken)
    {
        var isSuccess = await _authService.AddRoleToUserAsync(username, role, cancellationToken);
        
        if (isSuccess)
        {
            return Ok();
        }

        return BadRequest();
    }
}