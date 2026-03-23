using System.ComponentModel.DataAnnotations;

namespace AssignmentPro.Models;

public class Application
{

    [Key]
    [StringLength(12)]
    public string ApplicationId { get; set; }

    public decimal? PresentSalary { get; set; }

    public decimal? ExpectedSalary { get; set; }

    public DateTime ApplicationDate { get; set; } = DateTime.Now;

    [StringLength(50)]
    public string Status { get; set; } = "PENDING";

    [StringLength(200)]
    public string ResumePath { get; set; }
    public string UserID { get; set; }
    public User User { get; set; }
    public string JobID { get; set; }
    public Job Job { get; set; }
}
