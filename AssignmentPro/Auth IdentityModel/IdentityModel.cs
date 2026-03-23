using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssignmentPro.Auth_IdentityModel;

public class IdentityModel
{
    //------------------- User Table ------------------//
    [Table("Users")]
    public class User : IdentityUser<string>
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime RegisterDate { get; set; } 
        public long CreatedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedDate { get; set; }
    }

    //------------------- Role Table ------------------//
    [Table("Roles")]
    public class Role : IdentityRole<string>
    {
        public Role() { }
        public Role(string name) { Name = name; }
        public int StatusId { get; set; }
        public string Decription { get; set; }
        public long CreatedBy { get; set; }
        public DateTimeOffset CreatedDateUtc { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedDateUtc { get; set; }
    }

    //---------------- UserRole  ------------------//
    [Table("UserRoles")]
    public class UserRole : IdentityUserRole<long>
    {
        
    }

    //------------------ UserClaim  -----------------//
    [Table("UserClaims")]
    public class UserClaim : IdentityUserClaim<long>
    {
        
    }
    //------------------ UserLogin  -----------------//
    [Table("UserLogins")]
    public class UserLogin : IdentityUserLogin<long>
    {

    }
    //------------------ RoleClaim  -----------------//
    [Table("RoleClaims")]
    public class RoleClaim : IdentityRoleClaim<long>
    {
    }

    //------------------ UserToken  -----------------//

    public class UserToken : IdentityUserToken<long>
    {
    }


}
