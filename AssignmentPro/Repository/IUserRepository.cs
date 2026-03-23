using AssignmentPro.DataBase;
using AssignmentPro.Models;
using Microsoft.EntityFrameworkCore;

namespace AssignmentPro.Repository;

public interface IUserRepository
{
        Task<IEnumerable<User>> GetAllUserAsync(CancellationToken cancellationToken);
        Task<User> AddUserAsync(User user , CancellationToken cancellationToken);
        Task<User> UpdateUserAsync(User user , CancellationToken cancellationToken);
        Task<User> DeleteUserAsync(string id, CancellationToken cancellationToken);
        Task<User> GetUserByIdAsync(string id, CancellationToken cancellationToken);
        Task DeleteUser(string userId, CancellationToken cancellationToken);
        //New Paging method 
        //Task<IEnumerable<User>> GetUsersByPageAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
} 

public class UserRepository : IUserRepository   
{
    private readonly ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<User>> GetAllUserAsync(CancellationToken cancellationToken)
    {
        var data = await _context.Users.ToListAsync(cancellationToken);
        if(data != null)
        {
            return data;
        }
        return null;
    }

    public async Task DeleteUser(string userId, CancellationToken cancellationToken)
    {
        try
        {
            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            await _context.Database.ExecuteSqlRawAsync(
               "DELETE FROM Users WHERE Id = {0}", userId);

            await _context.Database.ExecuteSqlRawAsync(
                 "DELETE FROM Application WHERE UserId = {0}", userId);
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            throw;
        }
    }




    //public async Task DeleteUser(string id, CancellationToken cancellationToken)
    //{
    //    try
    //    {
    //        using var transaction = await _context.Database.BeginTransactionAsync();

    //        await _context.Database.ExecuteSqlRawAsync(
    //            "DELETE FROM Application WHERE UserID = {0}", id);

    //        await _context.Database.ExecuteSqlRawAsync(
    //            "DELETE FROM Users WHERE UserID = {0}", id);

    //        await transaction.CommitAsync();
    //    }
    //    catch (Exception ex)
    //    {

    //        throw;
    //    }

    //}

    public async Task<User> AddUserAsync(User user, CancellationToken cancellationToken)
    {
        try
        {
            user.UserID = "USER" + Guid.NewGuid().ToString("N").Substring(0, 9).ToUpper();
            await _context.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return user;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public async Task<User> UpdateUserAsync(User user, CancellationToken cancellationToken)
    {
        var data = await _context.Users.FindAsync(user.UserID, cancellationToken);
        if(data != null)
        {
            data.Name = user.Name;
            data.Email = user.Email;
            data.PasswordHash = user.PasswordHash;
            data.PhoneNumber = user.PhoneNumber;
            data.PresentSalary = user.PresentSalary;
            data.Degree = user.Degree;
            data.University = user.University;
            data.CGPA = user.CGPA;
            data.CompletionYear = user.CompletionYear;
            data.ResumePath = user.ResumePath;
            await _context.SaveChangesAsync(cancellationToken);
            return data;
        }
        return null;


    }
    public async Task<User> DeleteUserAsync(string id, CancellationToken cancellationToken)
    {
        var data = await _context.Users.FindAsync(id, cancellationToken);
        if (data != null)
        {
            _context.Users.Remove(data);
            await _context.SaveChangesAsync(cancellationToken);
        }
        return data;
    }


    public async Task<User> GetUserByIdAsync(string id, CancellationToken cancellationToken)
    {
        var data = await _context.Users.FindAsync(id, cancellationToken);
        if (data != null)
        {
            return data;
        }
        return null;
    }
}