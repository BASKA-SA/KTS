using System.Diagnostics;
using KarateTournamentSoftware.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KarateTournamentSoftware.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> logger;
        public HomeController(ILogger<HomeController> logger) {
            this.logger = logger;
        }

        public IActionResult Index() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            var exceptionContext = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = exceptionContext?.Error;

            return View(new ErrorViewModel() {
                RequestID = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                ExceptionMessage = exception?.Message,
                ExceptionFull = exception?.ToString(),
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult HttpError(int? code) {
            return View(new HttpErrorModel(code));
        }
    }
}
