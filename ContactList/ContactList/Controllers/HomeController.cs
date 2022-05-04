using ContactList.Configuration;
using ContactList.Models;
using ContactList.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ContactList.Filters;

namespace ContactList.Controllers
{
    public class HomeController : Controller
    {
        private int i = 0;

        private readonly HostingOptions _options;
        private readonly ILogger<HomeController> _logger;
        private readonly ITransientService _transientService;
        private readonly IScopedService _scopedService;
        private readonly ISingletonService _singletonService;

        public HomeController(
            IOptions<HostingOptions> options,
            ITransientService transientService,
            IScopedService scopedService,
            ISingletonService singletonService,
            ILogger<HomeController> logger)
        {
            _options = options.Value;

            _transientService = transientService;
            _scopedService = scopedService;
            _singletonService = singletonService;
            _logger = logger;
        }

        [ServiceFilter(typeof(ExecutionMonitorFilter))]
        public IActionResult Index()
        {
            ViewData["i"] = i;
            ViewBag.TransientServiceIdentifier = _transientService.Identifier;
            ViewBag.ScopedServiceIdentifier = _scopedService.Identifier;
            ViewBag.SingletonServiceIdentifier = _singletonService.Identifier;

            ViewBag.HostingProvider = _options.ProviderName;
            ViewBag.HostingUrl = _options.DeployUrl;
            return View();
        }

        public IActionResult Increment()
        {
            throw new InvalidOperationException("Increment is not allowed");
            i++;
            ViewData["i"] = i;
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
