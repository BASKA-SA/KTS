using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(KarateTournamentSoftware.Areas.Identity.IdentityHostingStartup))]
namespace KarateTournamentSoftware.Areas.Identity {
    public class IdentityHostingStartup : IHostingStartup {
        public void Configure(IWebHostBuilder builder) {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<Data.AppDBContext>(options =>
                    options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));

                services.AddIdentity<IdentityUser, IdentityRole>(options => {
                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedAccount = true; //                      .AddDefaultUI is required for some files that are not scaffolded, see https://stackoverflow.com/a/65017563/2999220
                }).AddEntityFrameworkStores<Data.AppDBContext>().AddDefaultTokenProviders().AddDefaultUI();
            });
        }
    }
}
