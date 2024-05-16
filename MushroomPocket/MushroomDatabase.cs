using Microsoft.EntityFrameworkCore;

namespace MushroomPocket
{
    public class MushroomDatabase : DbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<Wario> Warios { get; set; }
        public DbSet<Luigi> Luigis { get; set; }
        public DbSet<Peach> Peachs { get; set; }
        public DbSet<Mario> Marios { get; set; }
        public DbSet<MushroomMaster> MushroomMasters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=MushroomPocket.db");
        }
    }
}