using System.ComponentModel.DataAnnotations;

namespace AssignmentPro.Models.Auth;

public class RegisterViewModel
{
    [Required]
    [Display(Name = "Full Name")]
    [StringLength(100,MinimumLength =2, ErrorMessage = "Full Name must be at least 2 characters.")]
    public string FullName { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Invalid Email Address.")]
    [Display(Name = "Email Address")] 
    public string Email { get; set; }

    //[Required]
    //public string Address { get; set; }

    [Required]
    [Phone]
    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; } 

    [Required]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } 

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; } 
}
