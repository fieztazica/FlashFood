using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using server.Migrations;

namespace server.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        public static implicit operator IdentityResult(ApplicationUser v)
        {
            throw new NotImplementedException();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CartItem> Cartitems { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole>()
            .HasKey(i => new { i.UserId, i.RoleId });

            modelBuilder.Entity<IdentityUserLogin>()
            .HasKey(i => new { i.UserId, i.LoginProvider });

            modelBuilder.Entity<Order>()
            .HasRequired<ApplicationUser>(s => s.User)
            .WithMany(g => g.Orders)
            .HasForeignKey<string>(s => s.UserId);

            modelBuilder.Entity<OrderItem>()
            .HasKey(e => new { e.MealId, e.OrderId })
            .HasRequired<Order>(s => s.Order)
            .WithMany(g => g.OrderItems)
            .HasForeignKey<int>(s => s.OrderId);

            modelBuilder.Entity<OrderItem>()
            .HasKey(e => new { e.MealId, e.OrderId })
            .HasRequired<Meal>(s => s.Meal)
            .WithMany(g => g.oderItems)
            .HasForeignKey<int>(s => s.MealId);

            modelBuilder.Entity<CartItem>()
            .HasKey(e => new { e.MealId, e.UserId })
            .HasRequired<Meal>(s => s.Meal)
            .WithMany(g => g.cartItems)
            .HasForeignKey<int>(s => s.MealId);

            modelBuilder.Entity<CartItem>()
            .HasKey(e => new { e.MealId, e.UserId })
            .HasRequired<ApplicationUser>(s => s.User)
            .WithMany(g => g.CartItems)
            .HasForeignKey<string>(s => s.UserId);
        }
    }
}