using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssignmentPro.Models.Auth;

public class RegisterViewModel
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    [StringLength(100)]
    public string Email { get; set; }

    [Required]
    [StringLength(200)]
    public string PasswordHash { get; set; }

    [StringLength(20)]
    public string PhoneNumber { get; set; }

    public decimal? PresentSalary { get; set; }

    [StringLength(100)]
    public string Degree { get; set; }

    [StringLength(200)]
    public string University { get; set; }

    public decimal? CGPA { get; set; }

    public int? CompletionYear { get; set; }
    public IFormFile ResumePath { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public ICollection<Application> Applications { get; set; } = new HashSet<Application>();
}
