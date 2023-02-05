using System.Threading;
using System.Threading.Tasks;
using eStore_Admin.Application.AuthDTOs;
using eStore_Admin.Application.RequestDTOs;

namespace eStore_Admin.Application.Interfaces.Services;

public interface IAuthService
{
    Task<string> CreateTokenAsync(LoginCredentials credentials, CancellationToken cancellationToken);
    Task<bool> AddUserWithRolesAsync(UserDto user, CancellationToken cancellationToken);
    Task<bool> AddRoleToUserAsync(string userName, string roleName, CancellationToken cancellationToken);
    Task<bool> RemoveRoleFromUserAsync(string userName, string roleName, CancellationToken cancellationToken);
    Task<bool> DeleteUserAsync(string userName, CancellationToken cancellationToken);
}