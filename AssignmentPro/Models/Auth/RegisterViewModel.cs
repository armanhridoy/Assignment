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

   
    
}
