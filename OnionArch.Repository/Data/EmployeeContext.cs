using Microsoft.EntityFrameworkCore;
using OnionArch.Data.Entities;

namespace OnionArch.Repository.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<PositionType> PositionTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Employee>().HasOne(ur => ur.PositionType)
             .WithMany()
             .HasForeignKey(ur => ur.PositionTypeId)  
             .OnDelete(DeleteBehavior.NoAction);

        }
     }
}