using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SeaBreeze.Domain.Entity.Hotels;
using SeaBreeze.Domain.Entity.Payment;
using SeaBreeze.Domain.Entity.RealEsstates;
using SeaBreeze.Domain.Entity.Settings;
using SeaBreeze.Domain.Entity.Stories;
using SeaBreeze.Domain.Entity.Tickets;
using SeaBreeze.Domain.Entity.Users;

namespace SeaBreeze.Domain
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelEssentials> HotelEssentials { get; set; }
        public DbSet<HotelEssentialTranslate> HotelEssentialTranslates { get; set; }
        public DbSet<HotelImages> HotelImages { get; set; }
        public DbSet<HotelReservation> HotelReservations { get; set; }
        public DbSet<HotelTranslate> HotelTranslates { get; set; }
        public DbSet<Stories> Stories { get; set; }
        public DbSet<StoriesTranslate> StoriesTranslates { get; set; }
        public DbSet<BeachClub> BeachClubs { get; set; }
        public DbSet<BeachClubOrder> BeachClubOrders { get; set; }
        public DbSet<BeachClubTicket> BeachClubTickets { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<ResidentInfo> ResidentInfos { get; set; }
        public DbSet<RealEstate> RealEstates { get; set; }
        public DbSet<Detail> RealEstateDetails { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<FeatureTranslate> FeatureTranslates { get; set; }
        public DbSet<Gallery> RealEstateGalleries { get; set; }
        public DbSet<RealEstateTranslate> RealEstateTranslates { get; set; }
        public DbSet<FeatureRealEstate> FeatureRealEstates { get; set; }
        public DbSet<PayriffOrder> PayriffOrders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
