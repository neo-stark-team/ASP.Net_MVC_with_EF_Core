using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Models
{
    public class BloodDonorDbContext : DbContext
    {
        public BloodDonorDbContext(DbContextOptions<BloodDonorDbContext> options) : base(options)
        {
        }

        public DbSet<BloodDonor> BloodDonors { get; set; }
    }
}
