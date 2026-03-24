using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static AssignmentPro.AuthIdentityModel.IdentityModel;

namespace AssignmentPro.DataBase.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        var hasher = new PasswordHasher<User>();
        builder.HasData(new User
        {
            Id = 1,
            Name = "Admin User", // ✅ required
            Email = "admin@localhost.com",
            NormalizedEmail = "ADMIN@LOCALHOST.COM",
            UserName = "admin@localhost.com",
            NormalizedUserName = "ADMIN@LOCALHOST.COM",
            PasswordHash = hasher.HashPassword(null, "P@ssword1"),
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString(),
            Degree = "MSc in CSE", // optional, but good to provide
            ResumePath = "default.pdf",    // MUST provide non-null value
            University = "Dhaka University",
            CreatedAt = DateTime.Now
        });

    }
}