using Microsoft.EntityFrameworkCore;
using TrainingApp.Domain.Models;

namespace TrainingApp.Infastructure.Context
{
    public class TrainingContext : DbContext
    {
        public TrainingContext()
        {
            
        }
        public TrainingContext(DbContextOptions<TrainingContext> options) : base(options)
        {

        }

        public DbSet<Wod> Wods { get; set; }
        public DbSet<RunningSession> RunningSessions { get; set; }
        public DbSet<Weight> Weights { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer("Server=DESKTOP-ID96S2G\\SQLEXPRESS;Database=TrainingApp;Trusted_Connection=True;Encrypt=false;TrustServerCertificate=true;");
    }
}
