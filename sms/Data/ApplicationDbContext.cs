using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;
using System.Security.Policy;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using sms.Models;

namespace sms.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("dbo");
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "User");
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");

            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });
        }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Entry> Entries { get; set; }
        public virtual DbSet<MeasurementUnit> MeasurementUnits { get; set; }
        public virtual DbSet<Out> Outs { get; set; }
        public virtual DbSet<Parent> Parents { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }

        public virtual DbSet<Statusdel> Statusdels { get; set; }
        public virtual DbSet<StockItem> StockItems { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }

        public virtual DbSet<Car> Cars { get; set; } 
        public virtual DbSet<Driver> Drivers { get; set; } 
        public virtual DbSet<Fuel> Fuels { get; set; } 
        public virtual DbSet<General> Generals { get; set; } 
        public virtual DbSet<Litre> Litres { get; set; } 
        public virtual DbSet<Month> Months { get; set; } 
        public virtual DbSet<Targa> Targas { get; set; } 
        public virtual DbSet<Year> Years { get; set; } 
        public virtual DbSet<Garage> Garages { get; set; } 
        public virtual DbSet<Replacement> Replacements { get; set; } 
        public virtual DbSet<Service> Services { get; set; } 


    }
}
