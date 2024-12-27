using Microsoft.EntityFrameworkCore;
using PublisherDomain;

namespace PublisherData
{
    public class PubContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Cover> Covers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if you use different database provider you can configure it here
            optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = PubDatabase").LogTo(Console.WriteLine, new[] {DbLoggerCategory.Database.Command.Name},Microsoft.Extensions.Logging.LogLevel.Information).EnableSensitiveDataLogging();
            //optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = PubDatabase").UseLazyLoadingProxies().EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var someArtists = new Artist[]
            {
                new Artist { ArtistId = 1, FirstName = "Pablo", LastName = "picasso" },
                new Artist { ArtistId = 2, FirstName = "Dee", LastName = "Bell" },
                new Artist { ArtistId = 3, FirstName = "Katharine", LastName = "Kuharic" }
            };
            modelBuilder.Entity<Artist>().HasData(someArtists); 
            var someCovers = new Cover[]
            {
                new Cover { CoverId = 1,BookId = 1, DesignIdeas = "Its an Idea for cover 1", DigitalOnly = false },
                new Cover { CoverId = 2,BookId = 2, DesignIdeas = "Its an Idea for cover 2", DigitalOnly =  true },
                new Cover { CoverId = 3,BookId = 3, DesignIdeas = "Its an Idea for cover 3", DigitalOnly = false }
            };
            modelBuilder.Entity<Cover>().HasData(someCovers);

        }
    }
}
