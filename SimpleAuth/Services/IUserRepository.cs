using Microsoft.AspNetCore.Identity;

namespace SimpleAuth.Services;

public interface IUserRepository
{
    Task<IdentityUser?> ReadByUsernameAsync(string username);
}
