using Microsoft.EntityFrameworkCore;

namespace KarateTournamentSoftware.Data {
    public class AppDBContext : DbContext {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
    }
}
