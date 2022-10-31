using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleAuth.Models;
using SimpleAuth.Models.Entities;
using SimpleAuth.Services;
using System.Diagnostics;

namespace SimpleAuth.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUserRepository _userRepo;
    private Random _random = new Random();

    public HomeController(IUserRepository userRepo, ILogger<HomeController> logger)
    {
        _logger = logger;
        _userRepo = userRepo;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult GetUserName()
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            string username = User.Identity.Name ?? "";
            return Content(username);
        }
        return Content("No user");
    }

    public async Task<IActionResult> GetUserId()
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            string username = User.Identity.Name ?? "";
            var user = await _userRepo.ReadByUsernameAsync(username);
            if(user != null)
            {
                return Content(user.Id);
            }
        }
        return Content("No user");
    }

    public async Task<IActionResult> CreateTestUser()
    {
        var n = _random.Next(100);
        var check = 
            await _userRepo.ReadByUsernameAsync($"test{n}@test.com");
        if(check == null)
        {
            var user = new ApplicationUser
            {
                Email = $"test{n}@test.com",
                UserName = $"test{n}@test.com",
                FirstName = $"User{n}",
                LastName = $"UserLastname{n}"
            };
            await _userRepo.CreateAsync(user, "Pass1!");
            return Content($"Create test user 'test{n}@test.com'");
        }
        return Content("The test user was already created.");
    }

    public async Task<IActionResult> TestAssignUserToRole()
    {
        await _userRepo.AssignUserToRoleAsync("fake@email.com", "TestRole");
        return Content("Assigned fake@email.com to 'TestRole'");
    }

    [AllowAnonymous]
    public IActionResult About()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}