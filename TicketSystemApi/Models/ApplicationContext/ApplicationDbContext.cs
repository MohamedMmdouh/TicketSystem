using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TicketSystemApi.Models.ApplicationContext
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                  .HasOne(a => a.ticket)
                  .WithOne(b => b.user)
                  .HasForeignKey<Ticket>(b => b.userid);

            modelBuilder.Entity<User>()
                .Property(x => x.PhoneNumber).IsRequired(true);
        }
    }
}
