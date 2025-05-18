using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KarateTournamentSoftware.Controllers {
    [Authorize(Roles = Other.Constants.RoleNameRoleAdmin)]
    public class EnrollmentController : Controller {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        public EnrollmentController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager) {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        // GET: EnrollmentController
        public async Task<IActionResult> Index() {
            var usersInRoles = new Dictionary<string, IList<IdentityUser>>();
            foreach (var role in roleManager.Roles.ToList()) { // .ToList() otherwise we get `There is already an open DataReader associated with this Connection which must be closed first.`
                usersInRoles.Add(role.Name!, await userManager.GetUsersInRoleAsync(role.Name!));
            }
            return View(usersInRoles);
        }

        // GET: EnrollmentController/Create
        public IActionResult Create() {
            var roles = roleManager.Roles.Select(r => r.Name).ToList();
            var users = userManager.Users.ToList();
            return View((Roles: roles, Users: users));
        }
        // POST: EnrollmentController/Create {role="roleName", user="userID"}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection collection) {
            try {
                var roleName = collection["role"];
                var userID = collection["user"];
                var user = await userManager.FindByIdAsync(userID!);

                await userManager.AddToRoleAsync(user!, roleName!);
                return RedirectToAction(nameof(Index));
            } catch (System.Exception ex) {
                return BadRequest($"{ex.GetType().FullName}: {ex.Message}");
            }
        }

        // POST: EnrollmentController/Delete/5 {user="userID"}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string roleName, IFormCollection collection) {
            try {
                var userID = collection["user"];
                var user = await userManager.FindByIdAsync(userID!);

                await userManager.RemoveFromRoleAsync(user!, roleName);
                return RedirectToAction(nameof(Index));
            } catch (System.Exception ex) {
                return BadRequest($"{ex.GetType().FullName}: {ex.Message}");
            }
        }

        // GET: EnrollmentController/Users
        public async Task<IActionResult> Users() {
            var users = await userManager.Users.ToListAsync();
            return View(users);
        }

        // POST: EnrollmentController/DeleteUser/{userID}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string userID) {
            try {
                var user = await userManager.FindByIdAsync(userID);

                await userManager.DeleteAsync(user!);
                return RedirectToAction(nameof(Users));
            } catch (System.Exception ex) {
                return BadRequest($"{ex.GetType().FullName}: {ex.Message}");
            }
        }
    }
}
