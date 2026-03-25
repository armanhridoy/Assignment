using AssignmentPro.Models;
using AssignmentPro.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentPro.Controllers;
[Authorize(Roles = "Administrator")]

public class JobController : Controller
{
    private readonly IJobRepository _jobRepository;
    public JobController (IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var data = await _jobRepository.GetAllJobAsync(cancellationToken);

        return View(data);
    }
    [HttpGet]
    public async Task<IActionResult> CreateOrEdit(string id, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(id))
        {
            return View(new Job());
        }
        else
        {
            var data = await _jobRepository.GetJobByIdAsync(id, cancellationToken);
            if (data != null)
            {
                return View(data);
            }
            return NotFound();
        }
    }
    [HttpPost]
    public async Task<IActionResult> CreateOrEdit(Job job, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(job.JobID))
        {
            await _jobRepository.AddJobAsync(job, cancellationToken);
           
        }
        else
        {
            await _jobRepository.UpdateJobAsync(job, cancellationToken);
           
        }
        return RedirectToAction("Index");
    }
    [HttpGet]
    public async Task<IActionResult> Details(string id, CancellationToken cancellationToken)
    {
        var data = await _jobRepository.GetJobByIdAsync(id, cancellationToken);
        if (data != null)
        {
            return View(data);
        }
        return NotFound();
    }
     [HttpGet]
     public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var data = await _jobRepository.GetJobByIdAsync(id, cancellationToken);
        if (data == null)
        {
            return NotFound();
        }
        await _jobRepository.DeleteJob(id, cancellationToken);
        return RedirectToAction("Index");
    }
}
