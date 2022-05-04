using ContactList.Configuration;
using ContactList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using ContactList.Filters;

namespace ContactList.Controllers
{
    public class HomeController : Controller
    {
        private int i = 0;

        private readonly HostingOptions _options;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            IOptions<HostingOptions> options,
            ILogger<HomeController> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        [ServiceFilter(typeof(ExecutionMonitorFilter))]
        public IActionResult Index()
        {
            ViewBag.HostingProvider = _options.ProviderName;
            ViewBag.HostingUrl = _options.DeployUrl;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ErrorViewModel errorViewModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(errorViewModel);
        }
    }
}
