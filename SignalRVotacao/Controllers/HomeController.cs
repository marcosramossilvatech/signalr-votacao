using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SignalRVotacao.Models;
using System.Diagnostics;

namespace SignalRVotacao.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDBContext _context;
        public HomeController(ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
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
