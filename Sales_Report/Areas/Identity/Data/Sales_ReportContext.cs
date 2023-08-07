using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sales_Report.Areas.Identity.Data;
using System.Reflection.Emit;

namespace Sales_Report.Data;

public class Sales_ReportContext : IdentityDbContext<IdentityUser>
{
    public Sales_ReportContext(DbContextOptions<Sales_ReportContext> options)
        : base(options)
    {
    }
    //Load table into AsoNetUsers
    public DbSet<ApplicationUser> applicationUsers { get; set; }
    public DbSet<SalesTransaction> salesTransactions { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Create many to one realtionship 
        modelBuilder.Entity<ApplicationUser>(e =>
        {
            //Each User can have many transactions
            e.HasMany<SalesTransaction>().WithOne().HasForeignKey(e => e.UserId).IsRequired();
        });
    }
}



