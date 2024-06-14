using Microsoft.EntityFrameworkCore;
using BaobabBackEndSerice.Models;

namespace BaobabBackEndSerice.Data
{
  public class BaobabDataBaseContext : DbContext
  {
    public BaobabDataBaseContext(DbContextOptions<BaobabDataBaseContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Coupon> Coupons { get; set; }
    public DbSet<MarketingUser> MarketingUsers { get; set; }
    public DbSet<MassiveCoupon> MassiveCoupons { get; set; }
    public DbSet<ChangeHistory> ChangesHistory { get; set; }

  }

}