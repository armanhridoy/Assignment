using AssignmentPro.FilesUpload;
using AssignmentPro.Models;
using Microsoft.EntityFrameworkCore;

namespace AssignmentPro.Repository;

public interface IApplicationRepository
{
    Task<IEnumerable<Application>> GetAllApplicationsAsync(CancellationToken cancellationToken);
    Task<Application> AddApplicationAsync(Application application, CancellationToken cancellationToken);
    Task<Application> UpdateApplicationAsync(Application application, IFormFile ResumeFile, CancellationToken cancellationToken);
    Task<Application> GetApplicationByIdAsync(string id, CancellationToken cancellationToken);
    Task DeleteApplicationAsync(string id, CancellationToken cancellationToken);
    Task<IEnumerable<Application>> GetApplicationsByUserIdAsync(long userId, CancellationToken cancellationToken);
    Task<Application> GetApplicationByUserAndJobAsync(long userId, long jobId, CancellationToken cancellationToken);
}

public class ApplicationRepository : IApplicationRepository
{
    private readonly ApplicationDbContext _context;
    private readonly UserIdService _userIdService;
    private readonly IFileService _fileService;
    public ApplicationRepository(ApplicationDbContext context,UserIdService userIdService,IFileService fileService)
    {
        _context = context;
        _userIdService = userIdService;
        _fileService = fileService;
    }
    public async Task<IEnumerable<Application>> GetAllApplicationsAsync(CancellationToken cancellationToken)
    {
        return await _context.Applications.Include (x=>x.Job).ToListAsync(cancellationToken);
    }
    public async Task<Application> AddApplicationAsync(Application application, CancellationToken cancellationToken)
    {
        application.ApplicationId = await _userIdService.GetNextUserIdAsync<Application>(x => x.ApplicationId);
        _context.Applications.Add(application);
        await _context.SaveChangesAsync(cancellationToken);
        return application;
    }
    public async Task<Application> UpdateApplicationAsync(Application application, IFormFile ResumeFile, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _context.Applications.FirstOrDefaultAsync(x => x.ApplicationId == application.ApplicationId, cancellationToken);
          
            data.Name = application.Name;
            data.PresentSalary = application.PresentSalary;
            data.ExpectionSalary = application.ExpectionSalary;
            data.Degree = application.Degree;
            data.University = application.University;
            data.CGPA = application.CGPA;
            data.CompletionYear = application.CompletionYear;
            if(ResumeFile !=null)
            {
                data.ResumePath = await _fileService.Upload(ResumeFile, "Cv");
            }
           


            _context.Applications.Update(data);
            await _context.SaveChangesAsync(cancellationToken);
            return application;
        }
        catch (Exception ex)
        {

            throw;
        }

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

    public async Task<IEnumerable<Application>> GetApplicationsByUserIdAsync(long userId, CancellationToken cancellationToken)
    {

        return await _context.Applications
                .Where(a => a.UserId == userId)
                .Include(x => x.Job)
                .ToListAsync(cancellationToken);
    }
}

