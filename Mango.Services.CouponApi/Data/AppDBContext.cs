using Mango.Services.CouponApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponApi.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> dbContext) : base(dbContext)
        {

        }

        public DbSet<Coupon> Coupons { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>().HasData(new List<Coupon>() {
                new Coupon { CouponId = 1, CouponCode = "10OFF",DiscountAmount = 10, MinAmount = 20},
                new Coupon { CouponId = 2, CouponCode = "20OFF",DiscountAmount = 20, MinAmount = 40}
            });
        }
    }
}
