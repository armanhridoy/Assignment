using AssignmentPro.Models;
using AssignmentPro.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentPro.Controllers;

public class ApplicationController : Controller
{
    private readonly IApplicationRepository _applicationRepository;
    public ApplicationController (IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }
    public async Task <IActionResult> Index(CancellationToken cancellationToken)
    {
        var data = await _applicationRepository.GetAllApplicationsAsync(cancellationToken);
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

    public async Task<IActionResult>CreateOrEdit(Application application,CancellationToken cancellationToken)
    {
        await _applicationRepository.UpdateApplicationAsync(application, cancellationToken);
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
