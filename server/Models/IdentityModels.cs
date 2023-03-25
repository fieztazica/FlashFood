using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

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

        public ICollection<Oder> Oders { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Meal> meals { get; set; }
        public DbSet<Oder> Oder { get; set; }
        public DbSet<OderItem> oderitem { get; set; }
        public DbSet<CartItem> cartitem { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
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



            modelBuilder.Entity<Oder>()
            .HasRequired<ApplicationUser>(s => s.Seller)
            .WithMany(g => g.Oders)
            .HasForeignKey<string>(s => s.SellerId);

            modelBuilder.Entity<Oder>()
            .HasRequired<ApplicationUser>(s => s.Seller)
            .WithMany(g => g.Oders)
            .HasForeignKey<string>(s => s.SellerId);

            modelBuilder.Entity<OderItem>()
            .HasKey(e => new { e.MealId, e.OderId })
            .HasRequired<Oder>(s => s.Oder)
            .WithMany(g => g.oderItems)
            .HasForeignKey<int>(s => s.OderId);

            modelBuilder.Entity<OderItem>()
            .HasKey(e => new { e.MealId, e.OderId })
            .HasRequired<Meal>(s => s.Meal)
            .WithMany(g => g.oderItems)
            .HasForeignKey<int>(s => s.MealId);

            modelBuilder.Entity<CartItem>()
            .HasKey(e => new { e.MealId, e.UserId })
            .HasRequired<Meal>(s => s.Meal)
            .WithMany(g => g.cartItems)
            .HasForeignKey<int>(s => s.MealId);

            modelBuilder.Entity<CartItem>()
            .HasKey(e => new { e.MealId,e.UserId})
            .HasRequired<ApplicationUser>(s => s.User)
            .WithMany(g => g.CartItems)
            .HasForeignKey<string>(s => s.UserId);
        }
    }
}