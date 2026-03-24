using AssignmentPro.AuthIdentityModel;
using System.ComponentModel.DataAnnotations;

namespace AssignmentPro.Models;

public class Application
{
    [Key]
    public long Id { get; set; }
    [StringLength(12)]
    public string ApplicationId { get; set; }
    public decimal? PresentSalary { get; set; }
    public decimal? ExpectedSalary { get; set; }

    public DateTime ApplicationDate { get; set; } = DateTime.Now;

    [StringLength(50)]
    public string Status { get; set; } = "PENDING";

    [StringLength(200)]
    public string ResumePath { get; set; }
    public long UserId { get; set; }
    public IdentityModel.User User { get; set; }
    public long JobId { get; set; }
    public Job Job { get; set; }
}
