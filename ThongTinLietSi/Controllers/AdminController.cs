using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThongTinLietSi.Models;

namespace ThongTinLietSi.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly BrowserStatsService _browserStatsService;
        private readonly ILogger<AdminController> _logger; // Inject logger


        public AdminController(ApplicationDbContext context, BrowserStatsService browserStatsService, ILogger<AdminController> logger)
        {
            _context = context;
            _browserStatsService = browserStatsService;
            _logger = logger;   
        }


        public IActionResult Dashboard()
        {

            var browserStats = _context.BrowserVisits.ToList();
            ViewBag.BrowserStats = browserStats;

            return View();
        }


        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult TrackBrowserVisit([FromBody] BrowserVisitData visitData)
        {
            _logger.LogInformation("Received browser visit data. Browser: {BrowserName}", visitData.BrowserName);

            // Track the visit
            _browserStatsService.TrackVisit(visitData.BrowserName);

            //// Log the updated visit count for the browser
            //_logger.LogInformation("Updated visit count for browser: {BrowserName}. Current visit count: {VisitCount}",
            //    visitData.BrowserName,
            //    _browserStatsService.GetBrowserStats().FirstOrDefault(b => b.BrowserName == visitData.BrowserName)?.VisitCount);

            return Ok();
        }
    }

}
