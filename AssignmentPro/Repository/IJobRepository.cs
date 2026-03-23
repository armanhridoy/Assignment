using AssignmentPro.DataBase;
using AssignmentPro.Models;
using Microsoft.EntityFrameworkCore;

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
    public  JobRepository(ApplicationDbContext context)
    {
        _context = context;
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
        try
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            await _context.Database.ExecuteSqlRawAsync(
                "DELETE FROM Application WHERE JobID = {0}", id);

            await _context.Database.ExecuteSqlRawAsync(
                "DELETE FROM Jobs WHERE JobID = {0}", id);

            await transaction.CommitAsync();
        }
        catch ( Exception ex)
        {

            throw;
        }
       
    }
    public async Task<Job> AddJobAsync(Job job, CancellationToken cancellationToken)
    {


        try
        {
            job.JobID = "JOB" + Guid.NewGuid().ToString("N").Substring(0, 9).ToUpper();
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
        var data = await _context.Jobs.FindAsync(job.JobID, cancellationToken);
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
        var data = await _context.Jobs.FindAsync(id, cancellationToken);
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
        var data = await _context.Jobs.FindAsync(id, cancellationToken);
        if(data != null)
        {
            return data;
        }
        return null;
    }
}
