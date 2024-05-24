using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MushroomPocket.EntityFrameworkCore
{
    // Database context class for MushroomPocket application
    public class MushroomDatabase : DbContext
    {
        // DbSet properties representing tables in the database
        public DbSet<Character> Characters { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Wario> Warios { get; set; }
        public DbSet<Luigi> Luigis { get; set; }
        public DbSet<Peach> Peachs { get; set; }
        public DbSet<Mario> Marios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=MushroomPocket.db");
        }

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