using AssignmentPro.Models.Auth;
using Microsoft.AspNetCore.Identity;
using static AssignmentPro.Auth_IdentityModel.IdentityModel;

namespace AssignmentPro.Repository;

public interface IAuthService
{
    Task<RegistrationResponse> Register (RegisterViewModel model);
}
public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    public AuthService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    public async Task<RegistrationResponse> Register(RegisterViewModel request)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
        {
            return new RegistrationResponse
            {
                Success = false,
                Errors = new List<string> { "Email is already registered." }
            };
        }
        var user = new User
        {
            //UserName = request.Email,
            //FullName = request.FullName,
            Email = request.Email,
            Phone = request.PhoneNumber,
            EmailConfirmed = true, // Set to true if you want to skip email confirmation for this example
            SecurityStamp = Guid.NewGuid().ToString(),
           
        };
        //var result = await _userManager.CreateAsync(user, request.Password);
        //if (!result.Succeeded)
        //{
        //    return new RegistrationResponse
        //    {
        //        Success = false,
        //        Errors = result.Errors.Select(e => e.Description).ToList()
        //    };
        //}
        await _userManager.AddToRoleAsync(user, "User"); // Assign default role if needed

        return new RegistrationResponse
        {
            Success = true,
            UserId = user.Id
        };
    }
}

