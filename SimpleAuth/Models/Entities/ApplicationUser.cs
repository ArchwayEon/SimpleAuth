using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleAuth.Models.Entities;

public class ApplicationUser : IdentityUser
{
    [StringLength(50)]
    public string FirstName { get; set; } = String.Empty;
    [StringLength(50)]
    public string LastName { get; set; } = String.Empty;
    [NotMapped]
    public ICollection<string> Roles { get; set; }
        = new List<string>();

    public bool HasRole(string roleName)
    {
        return Roles.Contains(roleName);
    }
}
