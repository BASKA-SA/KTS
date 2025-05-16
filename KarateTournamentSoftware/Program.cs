using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KarateTournamentSoftware {
    public class Program {
        public static async Task Main(string[] args) {
            var host = CreateHostBuilder(args).Build();

            _ = Task.Run(async () => {
                var logger = host.Services.GetRequiredService<ILogger<Program>>();
                try {
                    using (var scope = host.Services.CreateScope())
                        await onStartup(scope.ServiceProvider);
                } catch (System.Exception ex) {
                    logger.LogError(ex, "Error running Application OnStartup");
                }
            }).ConfigureAwait(false);

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());

        private static async Task onStartup(System.IServiceProvider serviceProvider) {
            using var dbContext = serviceProvider.GetRequiredService<Data.AppDBContext>();
            await dbContext.Database.MigrateAsync();

            string[] roleNames = new string[] {
                Other.Constants.RoleNameRoleAdmin,
                Other.Constants.RoleNameAdmin,
                Other.Constants.RoleNameEntryManager,
                Other.Constants.RoleNameTatami,
            };

            var dbRoles = await dbContext.Roles.Select(r => r.Name).ToListAsync();
            using var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            foreach (string roleName in roleNames) {
                if (!dbRoles.Contains(roleName))
                    await roleManager.CreateAsync(new IdentityRole(roleName));
            }

        }
    }
}
