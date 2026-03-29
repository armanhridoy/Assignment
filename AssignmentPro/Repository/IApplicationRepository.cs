using AssignmentPro.Models;
using Microsoft.EntityFrameworkCore;

namespace AssignmentPro.Repository;

public interface IApplicationRepository
{
    Task<IEnumerable<Application>> GetAllApplicationsAsync(CancellationToken cancellationToken);
    Task<Application> AddApplicationAsync(Application application, CancellationToken cancellationToken);
    Task<Application> UpdateApplicationAsync(Application application, CancellationToken cancellationToken);
    Task<Application> GetApplicationByIdAsync(string id, CancellationToken cancellationToken);
    Task DeleteApplicationAsync(string id, CancellationToken cancellationToken);

    Task<Application> GetApplicationByUserAndJobAsync(long userId, long jobId, CancellationToken cancellationToken);
}

public class ApplicationRepository : IApplicationRepository
{
    private readonly ApplicationDbContext _context;
    private readonly UserIdService _userIdService;
    public ApplicationRepository(ApplicationDbContext context,UserIdService userIdService)
    {
        _context = context;
        _userIdService = userIdService;
    }
    public async Task<IEnumerable<Application>> GetAllApplicationsAsync(CancellationToken cancellationToken)
    {
        return await _context.Applications.ToListAsync(cancellationToken);
    }
    public async Task<Application> AddApplicationAsync(Application application, CancellationToken cancellationToken)
    {
        application.ApplicationId = await _userIdService.GetNextUserIdAsync<Application>(x => x.ApplicationId);
        _context.Applications.Add(application);
        await _context.SaveChangesAsync(cancellationToken);
        return application;
    }
    public async Task<Application> UpdateApplicationAsync(Application application, CancellationToken cancellationToken)
    {
        _context.Applications.Update(application);
        await _context.SaveChangesAsync(cancellationToken);
        return application;
    }
    public async Task<Application> GetApplicationByIdAsync(string Id, CancellationToken cancellationToken)
    {
        var data = await _context.Applications.FirstOrDefaultAsync(x => x.ApplicationId == Id, cancellationToken);
        if (data != null)
        {
            return data;
        }
        return null;
    }
    public async Task DeleteApplicationAsync(string id, CancellationToken cancellationToken)
    {
        var application = await _context.Applications.FindAsync(new object[] { id }, cancellationToken);
        if (application != null)
        {
            _context.Applications.Remove(application);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }


        public async Task<Application> GetApplicationByUserAndJobAsync(long userId, long jobId, CancellationToken cancellationToken)
    {
        return await _context.Applications
                    .FirstOrDefaultAsync(a => a.UserId == userId && a.JobId == jobId, cancellationToken);
    }
}
