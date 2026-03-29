using AssignmentPro.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssignmentPro.AuthIdentityModel;

public class IdentityModel
{

    [Table("Users")]
    public class User : IdentityUser<long>
    {
        public string? UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Application> Applications { get; set; } = new HashSet<Application>();
    }

    [Table("Roles")]
    public class Role : IdentityRole<long>
    {
        public Role() { }
        public Role(string name) { Name = name; }

        public int StatusId { get; set; }
        public string Description { get; set; }

        public long CreatedBy { get; set; }
        public DateTimeOffset CreatedDateUtc { get; set; }

        public long? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedDateUtc { get; set; }
    }

    [Table("UserRoles")]
    public class UserRole : IdentityUserRole<long> { }

    [Table("UserClaims")]
    public class UserClaim : IdentityUserClaim<long> { }

    [Table("UserLogins")]
    public class UserLogin : IdentityUserLogin<long> { }

  
    [Table("RoleClaims")]
    public class RoleClaim : IdentityRoleClaim<long> { }

  
    [Table("UserTokens")]
    public class UserToken : IdentityUserToken<long> { }
}