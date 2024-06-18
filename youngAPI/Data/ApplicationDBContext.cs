using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using youngAPI.Models;


namespace youngAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Song> Song { get; set; }
        public DbSet<Playlist> Playlist { get; set; }
        public DbSet<PlaylistSong> PlaylistSong { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.Songs)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.Playlists)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Playlist>()
                .HasMany(e => e.Songs)
                .WithMany(e => e.Playlists)
                .UsingEntity<PlaylistSong>();

        }
    }
}
