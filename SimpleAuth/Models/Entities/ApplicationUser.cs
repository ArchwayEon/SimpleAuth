using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SimpleAuth.Models.Entities;

public class ApplicationUser : IdentityUser
{
    [StringLength(50)]
    public string FirstName { get; set; } = String.Empty;
    [StringLength(50)]
    public string LastName { get; set; } = String.Empty;
}
