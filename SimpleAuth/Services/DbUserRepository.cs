using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleAuth.Models.Entities;

namespace SimpleAuth.Services;

public class DbUserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DbUserRepository(ApplicationDbContext db, 
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _db = db;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task AssignUserToRoleAsync(string userName, string roleName)
    {
        var roleCheck = await _roleManager.RoleExistsAsync(roleName);
        if(!roleCheck)
        {
            await _roleManager.CreateAsync(new IdentityRole(roleName));
        }
        var user = await ReadByUsernameAsync(userName);
        if(user != null)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }
    }

    public async Task<ApplicationUser> CreateAsync(
        ApplicationUser user, string password)
    {
        await _userManager.CreateAsync(user, password);
        return user;
    }

    public async Task<ApplicationUser?> ReadByUsernameAsync(string username)
    {
        return await _db.Users.FirstOrDefaultAsync(u => u.UserName == username);
    }
}
