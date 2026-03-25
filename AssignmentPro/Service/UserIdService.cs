using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class UserIdService
{
    private readonly ApplicationDbContext _context;

    public UserIdService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> GetNextUserIdAsync<T>(
        Expression<Func<T, string>> idSelector) where T : class
    {
        string prefix = DateTime.Now.ToString("yyyyMM");

        var lastUserId = await _context.Set<T>()
            .OrderByDescending(idSelector)
            .Select(idSelector)
            .FirstOrDefaultAsync();

        if (string.IsNullOrEmpty(lastUserId))
            return prefix + "0001";

        string lastPrefix = lastUserId.Substring(0, 6);

        if (lastPrefix != prefix)
            return prefix + "0001";

        int lastNumber = int.Parse(lastUserId.Substring(6));
        return prefix + (lastNumber + 1).ToString("D4");
    }
}