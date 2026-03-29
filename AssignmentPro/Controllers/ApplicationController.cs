using AssignmentPro.Models;
using AssignmentPro.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AssignmentPro.Controllers;

public class ApplicationController : Controller
{
    private readonly IApplicationRepository _applicationRepository;
    public ApplicationController (IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }
    [Authorize]
public async Task<IActionResult> Index(CancellationToken cancellationToken)
{
    IEnumerable<Application> data;

    if (User.IsInRole("Administrator"))
    {
        // Admin sees all applications
        data = await _applicationRepository.GetAllApplicationsAsync(cancellationToken);
    }
    else
    {
            long userId = Convert.ToInt64(User.FindFirstValue(ClaimTypes.NameIdentifier));
            data = await _applicationRepository.GetApplicationsByUserIdAsync(userId, cancellationToken);
            //// Regular user sees only their own applications
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //data = await _applicationRepository.GetApplicationsByUserIdAsync(userId, cancellationToken);
        }

    return View(data);
}
    [HttpGet]
    public async Task<IActionResult>CreateOrEdit(string Id, CancellationToken cancellationToken)
    {
        var data = await _applicationRepository.GetApplicationByIdAsync(Id, cancellationToken);
        if(data != null)
        {
            return View(data);
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult>CreateOrEdit(Application application, IFormFile ResumeFile, CancellationToken cancellationToken)
    {
        await _applicationRepository.UpdateApplicationAsync(application, ResumeFile, cancellationToken);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public async Task<IActionResult>Details(string Id ,CancellationToken cancellationToken)
    {
        var data = await _applicationRepository.GetApplicationByIdAsync (Id, cancellationToken);
        if(data != null)
        {
            return View(data);
        }
        return NotFound();
    }
}
