using AssignmentPro.Models;

using Microsoft.EntityFrameworkCore;
using static AssignmentPro.AuthIdentityModel.IdentityModel;

namespace AssignmentPro.Repository;

public interface IJobRepository
{
    Task<IEnumerable<Job>> GetAllJobAsync(CancellationToken cancellationToken);
    Task<Job> AddJobAsync(Job job , CancellationToken cancellationToken);
    Task<Job> UpdateJobAsync(Job job , CancellationToken cancellationToken);
    Task<Job> DeleteJobAsync(string id, CancellationToken cancellationToken);
    Task<Job> GetJobByIdAsync(string id, CancellationToken cancellationToken);
    Task DeleteJob(string id, CancellationToken cancellationToken);
}
public class JobRepository : IJobRepository
{
    private readonly ApplicationDbContext _context;
    private readonly UserIdService _userIdService;
    public  JobRepository(ApplicationDbContext context, UserIdService userIdService)
    {
        _context = context;
        _userIdService = userIdService;

    }
    public async Task<IEnumerable<Job>> GetAllJobAsync(CancellationToken cancellationToken)
    {
        var data = await _context.Jobs.ToListAsync(cancellationToken);
        if(data != null)
        {
            return data;
        }
        return null;
    }
    public async Task DeleteJob(string id, CancellationToken cancellationToken)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            // Get job with related applications
            var job = await _context.Jobs
                .Include(j => j.Applications)
                .FirstOrDefaultAsync(j => j.JobID == id, cancellationToken);

            if (job == null)
                return;

            // Remove child records first
            _context.Applications.RemoveRange(job.Applications);

            // Remove parent
            _context.Jobs.Remove(job);

            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
    public async Task<Job> AddJobAsync(Job job, CancellationToken cancellationToken)
    {
        try
        {
            job.JobID = await _userIdService.GetNextUserIdAsync<Job>(x => x.JobID);
            await _context.AddAsync(job, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return job;

        }
        catch (Exception ex)
        {

            throw;
        }
     
    }
    public async Task<Job> UpdateJobAsync(Job job, CancellationToken cancellationToken)
    {
        var data = await _context.Jobs.FirstOrDefaultAsync(x=>x.JobID==job.JobID, cancellationToken);
        if(data != null)
        {
            data.JobTitle = job.JobTitle;
            data.Description = job.Description;
            data.SalaryRange = job.SalaryRange;
            data.Deadline = job.Deadline;
            data.Status = job.Status;
            await _context.SaveChangesAsync(cancellationToken);
            return data;
        }
        return null;
    }
    public async Task<Job> DeleteJobAsync(string id, CancellationToken cancellationToken)
    {
        var data = await _context.Jobs.FirstOrDefaultAsync(x=>x.JobID==id, cancellationToken);
        if(data != null)
        {
            _context.Remove(data);
            await _context.SaveChangesAsync(cancellationToken);
            return data;
        }
        return null;
    }
    public async Task<Job> GetJobByIdAsync(string id, CancellationToken cancellationToken)
    {
        var data = await _context.Jobs.FirstOrDefaultAsync(x => x.JobID == id, cancellationToken);
        if(data != null)
        {
            return data;
        }
        return null;
    }
}
