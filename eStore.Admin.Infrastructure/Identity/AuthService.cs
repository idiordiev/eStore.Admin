using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using eStore.Admin.Application.AuthDTOs;
using eStore.Admin.Application.Interfaces;
using eStore.Admin.Application.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace eStore.Admin.Infrastructure.Identity;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtSettings _jwtSettings;
    private readonly IClock _clock;
    private readonly ILogger<AuthService> _logger;

    public AuthService(UserManager<IdentityUser> userManager, IOptions<JwtSettings> jwtSettings, IClock clock, ILogger<AuthService> logger)
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
        _clock = clock;
        _logger = logger;
    }

    public async Task<string> CreateTokenAsync(LoginCredentials credentials, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(credentials.UserName);
        if (user is null)
        {
            throw new InvalidCredentialException($"The user with name {credentials.UserName} has not been found.");
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, credentials.Password);
        if (!isPasswordValid)
        {
            throw new InvalidCredentialException($"Wrong password for user {credentials.UserName}.");
        }

        var claims = await GetClaimsAsync(user);
        var signingCredentials = GetSigningCredentials();

        var tokenOptions = new JwtSecurityToken(_jwtSettings.ValidIssuer,
            _jwtSettings.ValidAudience,
            claims,
            expires: _clock.UtcNow().AddHours(3),
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"));
        var secret = new SymmetricSecurityKey(key);
        var signingCredentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        return signingCredentials;
    }

    private async Task<IEnumerable<Claim>> GetClaimsAsync(IdentityUser user)
    {
        var claims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return claims;
    }

    public async Task<bool> AddUserWithRolesAsync(UserDto user, CancellationToken cancellationToken)
    {
        var identityUser = new IdentityUser()
        {
            UserName = user.UserName,
            NormalizedUserName = user.UserName.ToUpper(),
            Email = user.Email,
            NormalizedEmail = user.Email.ToUpper()
        };
        
        await _userManager.CreateAsync(identityUser, user.Password);
        _logger.LogInformation("New user has been added to Identity. Username: {0}, roles: {1}",
            identityUser.UserName, string.Join(", ", user.Roles));
        
        await _userManager.AddToRolesAsync(identityUser, user.Roles);

        return true;
    }

    public async Task<bool> AddRoleToUserAsync(string userName, string roleName,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null)
        {
            return false;
        }
        
        await _userManager.AddToRoleAsync(user, roleName);
        _logger.LogInformation("User {0} has been added to role {1}", userName, roleName);

        return true;
    }

    public async Task<bool> RemoveRoleFromUserAsync(string userName, string roleName,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null)
        {
            return false;
        }

        await _userManager.RemoveFromRoleAsync(user, roleName);
        _logger.LogInformation("User {0} has been removed from role {1}", userName, roleName);
        
        return true;
    }

    public async Task<bool> DeleteUserAsync(string userName, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null)
        {
            return false;
        }

        await _userManager.DeleteAsync(user);
        _logger.LogInformation("User has been deleted from identity. Username: {0}", userName);
        
        return true;
    }
}