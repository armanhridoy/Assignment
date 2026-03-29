using AssignmentPro.FilesUpload;
using AssignmentPro.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static AssignmentPro.AuthIdentityModel.IdentityModel;

namespace AssignmentPro.Repository;

public interface IAuthService
{
    Task<RegistrationResponse> Register (RegisterViewModel model);
}
public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IFileService _fileService;
    private readonly UserIdService _userIdService;
    public AuthService(UserManager<User> userManager,IFileService fileService, UserIdService userIdService)
    {
        _userManager = userManager;
        _fileService = fileService;
        _userIdService = userIdService;
    }
    public async Task<RegistrationResponse> Register(RegisterViewModel request)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
        {
            return new RegistrationResponse
            {
                Success = false,
                Errors = new List<string> {"Email is already registered." }
            };
        }
        //Check if phone number already exists

        //var existingPhoneUser = await _userManager.Users
        //    .FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber);
        //if(existingPhoneUser != null)
        //{
        //    return new RegistrationResponse
        //    {
        //        Success = false,
        //        Errors = new List<string> { "Phone Number is Already registered." }
        //    };
        //}

        var user = new User
        {
            UserName = request.Email,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            CreatedAt=DateTime.Now,
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserId= await _userIdService.GetNextUserIdAsync<User>(x => x.UserId)

        };
        var result = await _userManager.CreateAsync(user, request.PasswordHash);
        if (!result.Succeeded)
        {
            return new RegistrationResponse
            {
                Success = false,
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }
        await _userManager.AddToRoleAsync(user, "User"); //Assign default role if needed

        return new RegistrationResponse
        {
            Success = true,
            UserId = user.Id.ToString(),
        };
    }
}

