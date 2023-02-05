using System.Collections.Generic;

namespace eStore_Admin.Application.AuthDTOs;

public class UserDto
{
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public ICollection<string> Roles { get; set; }
}