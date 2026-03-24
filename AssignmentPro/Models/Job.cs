using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssignmentPro.Models;

public class Job
{
    [Key]
    public long Id { get; set; }
    [StringLength(12)]
    public string JobID { get; set; }

    [Required]
    [StringLength(200)]
    public string JobTitle { get; set; }

    [StringLength(500)]
    public string Description { get; set; }

    public string? SalaryRange { get; set; }

    public DateTime? Deadline { get; set; }

    [StringLength(20)]
    public string Status { get; set; } = "Active";

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public ICollection<Application> Applications { get; set; } = new HashSet<Application>();
}
