using Microsoft.AspNetCore.Identity;
using SimpleAuth.Models.Entities;

namespace SimpleAuth.Services;

public interface IUserRepository
{
    Task<ApplicationUser?> ReadByUsernameAsync(string username);
}
