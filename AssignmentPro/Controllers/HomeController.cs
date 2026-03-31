using AssignmentPro.FilesUpload;
using AssignmentPro.Models;
using AssignmentPro.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace AssignmentPro.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IJobRepository jobRepository;
        private readonly IFileService fileService;
        private readonly IApplicationRepository applicationRepository; 

        public HomeController(ILogger<HomeController> logger, IJobRepository jobRepository,IFileService fileService,IApplicationRepository applicationRepository )
        {
            _logger = logger;
            this.jobRepository = jobRepository;
            this.fileService = fileService;
            this.applicationRepository = applicationRepository;
        }

        public async Task< IActionResult> Index(CancellationToken cancellationToken)
        {
            var data = await jobRepository.GetAllJobAsync(cancellationToken);

            return View(data);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Apply(long jobId, CancellationToken cancellationToken)
          {

            var userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var existingApp = await applicationRepository
                .GetApplicationByUserAndJobAsync(userId, jobId, cancellationToken);

            if (existingApp != null)
            {
                return RedirectToAction("AlreadyApplied");
            }

            var model = new Application
            {
                JobId = jobId,
                UserId = userId
            };

            return View(model);
        }
        public async Task<IActionResult> ApplyJob(Application model, IFormFile ResumeFile, CancellationToken cancellationToken)
        {
            if (ResumeFile != null)
            {
                model.ResumePath = await fileService.Upload(ResumeFile, "Cv");
            }

            var data = await applicationRepository.AddApplicationAsync(model, CancellationToken.None);
            if (data != null)
            {
                return RedirectToAction("Success");
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult AlreadyApplied()
        {
            return View();
        }


        //[HttpPost]
        //[Authorize]
        //public async Task<IActionResult> ApplyJob(Application model, IFormFile ResumeFile, CancellationToken cancellationToken)
        //{
        //    var userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        //    model.UserId = userId;

        //    // Check if the user already applied
        //    var existingApp = await applicationRepository.GetApplicationByUserAndJobAsync(model.UserId, model.JobId, cancellationToken);
        //    if (existingApp != null)
        //    {
        //        // Redirect to a new "Already Applied" page
        //        return RedirectToAction("AlreadyApplied");
        //    }

        //    // Handle resume upload
        //    if (ResumeFile != null)
        //    {
        //        model.ResumePath = await fileService.Upload(ResumeFile, "Cv");
        //    }

        //    await applicationRepository.AddApplicationAsync(model, cancellationToken);

        //    TempData["Success"] = "Application submitted successfully!";
        //    return RedirectToAction("Success");
        //}
        public ActionResult Success()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id, CancellationToken cancellationToken)
        {
            var data = await jobRepository.GetJobByIdAsync(id, cancellationToken);
            if (data != null)
            {
                return View(data);
            }
            return NotFound();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
