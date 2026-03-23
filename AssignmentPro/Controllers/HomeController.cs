using AssignmentPro.Models;
using AssignmentPro.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading;

namespace AssignmentPro.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IJobRepository jobRepository;

        public HomeController(ILogger<HomeController> logger, IJobRepository jobRepository)
        {
            _logger = logger;
            this.jobRepository = jobRepository;
        }

        public async Task< IActionResult> Index(CancellationToken cancellationToken)
        {
            var data = await jobRepository.GetAllJobAsync(cancellationToken);

            return View(data);
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
