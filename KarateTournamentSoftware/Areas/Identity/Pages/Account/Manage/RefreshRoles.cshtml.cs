using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace KarateTournamentSoftware.Areas.Identity.Pages.Account.Manage {
    public class RefreshRolesModel : PageModel {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ILogger<LoginModel> logger;
        public RefreshRolesModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<LoginModel> logger) {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
        }

        public string? ReturnUrl { get; set; }

        public async Task<IActionResult> OnGetAsync(string? returnUrl = null) {
            ReturnUrl = returnUrl ?? Url.Content("./Index");

            logger.LogInformation("Finding user...");
            var user = await userManager.GetUserAsync(User);
            logger.LogInformation("Refreshing roles for user {id}...", user?.Id);
            await signInManager.RefreshSignInAsync(user!);
            logger.LogInformation("Refresh succeded for user {id}, redirecting to {url}...", user?.Id, ReturnUrl);

            return Redirect(ReturnUrl);
        }
    }
}
