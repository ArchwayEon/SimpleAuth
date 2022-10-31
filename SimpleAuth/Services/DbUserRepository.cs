using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleAuth.Models.Entities;

namespace SimpleAuth.Services;

public class DbUserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;

    public DbUserRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task<ApplicationUser?> ReadByUsernameAsync(string username)
    {
        return await _db.Users.FirstOrDefaultAsync(u => u.UserName == username);
    }
}
