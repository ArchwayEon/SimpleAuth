using Microsoft.AspNetCore.Identity;
using SimpleAuth.Models.Entities;

namespace SimpleAuth.Services;

public interface IUserRepository
{
    Task<ApplicationUser?> ReadByUsernameAsync(string username);
    Task<ApplicationUser> CreateAsync(ApplicationUser user, string password);
    Task AssignUserToRoleAsync(string userName, string roleName);
}
