using Microsoft.EntityFrameworkCore;

namespace MushroomPocket.EntityFrameworkCore
{
    // Database context class for MushroomPocket application
    public class MushroomDatabase : DbContext
    {
        // DbSet properties representing tables in the database
        public DbSet<Character> Characters { get; set; }
        public DbSet<Wario> Warios { get; set; }
        public DbSet<Luigi> Luigis { get; set; }
        public DbSet<Peach> Peachs { get; set; }
        public DbSet<Mario> Marios { get; set; }
        public DbSet<MushroomMaster> MushroomMasters { get; set; }

        // Configuring the database to use SQLite with a specified data source
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=MushroomPocket.db");
        }

        // Configuring the model to use a discriminator for the Character hierarchy
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>()
                .HasDiscriminator<string>("CharacterType")
                .HasValue<Waluigi>("Waluigi")
                .HasValue<Daisy>("Daisy")
                .HasValue<Wario>("Wario")
                .HasValue<Luigi>("Luigi")
                .HasValue<Peach>("Peach")
                .HasValue<Mario>("Mario");

            base.OnModelCreating(modelBuilder);
        }
    }
}