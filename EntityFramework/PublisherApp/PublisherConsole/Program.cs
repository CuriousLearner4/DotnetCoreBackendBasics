using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using PublisherData;
using PublisherDomain;
using System.Globalization;

namespace PublisherConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GetAuthorsLazyLoad();
        }
        public static void NewBookAndCover()
        {
            PubContext context = new PubContext();
            var book = new Book { AuthorId = 1, Title = "Call Me Isthar", PublishDate = new DateTime(1973, 1, 1) };
            book.Cover = new Cover { DesignIdeas = "Image of Ishtar?" };
            context.Books.Add(book);
            context.SaveChanges();
        }
        public static void GetAllBooksTitleandDesignIdea()
        {
            PubContext context = new PubContext();
            var books = context.Books.Where(b => b.Cover != null).Select(b => new { b.Title, b.Cover.DesignIdeas }).ToList();
           
        }
        public static void GetAllBooksWhichDontHaveCover()
        {
            PubContext context = new PubContext();
            var booksandcovers = context.Books.Include(b => b.Cover==null).ToList();
            booksandcovers.ForEach(book => Console.WriteLine(book.Title + (book.Cover == null ? ": No cover yet" : ":" + book.Cover.DesignIdeas)));

        } 
        public static void GetBooksThatHaveACover()
        {
            PubContext context = new PubContext();
            var booksandcovers = context.Books.Include(b => b.Cover).Where(b=>b.Cover!=null).ToList();
            booksandcovers.ForEach(book => Console.WriteLine(book.Title + (book.Cover == null ? ": No cover yet" : ":" + book.Cover.DesignIdeas)));

        }
        public static void GetAllBooksWithCover()
        {
            PubContext context = new PubContext();
            var booksandcovers = context.Books.Include(b => b.Cover).ToList();
            booksandcovers.ForEach(book => Console.WriteLine(book.Title + (book.Cover == null ? ": No cover yet" : ":" + book.Cover.DesignIdeas)));

        }
        public static void ReassignACover()
        {
            PubContext context = new PubContext();
            var coverWithArtist4 = context.Covers.Include(c => c.Artists.Where(a => a.ArtistId == 4)).FirstOrDefault(c=>c.CoverId==7);
            coverWithArtist4.Artists.RemoveAt(0);
            var artist3 = context.Artists.Find(3);
            coverWithArtist4.Artists.Add(artist3);
            context.ChangeTracker.DetectChanges();
        }
        public static void UnAssignAnArtistFromACover()
        {
            PubContext context = new PubContext();
            var coverWithArtist = context.Covers.Include(c => c.Artists.Where(a => a.ArtistId == 1)).FirstOrDefault(c => c.CoverId == 1);
            coverWithArtist.Artists.RemoveAt(0);
            context.ChangeTracker.DetectChanges();
            var debugView = context.ChangeTracker.DebugView.ShortView;
            context.SaveChanges();
        }
        public static void RetreiveAllArtistsWithTheirCovers()
        {
            PubContext context = new PubContext();
            var artistsWithCovers = context.Artists.Include(a => a.Covers).ToList();
            foreach (var a in artistsWithCovers)
            {
                var primaryArtistId = a.ArtistId;
                Console.WriteLine($"{a.FirstName} {a.LastName},Designs to work on:");
                if (a.Covers.Count == 0)
                {
                    Console.WriteLine("No covers");
                }
                else
                {
                    foreach(var c in a.Covers)
                    {
                        string collaborators = "";
                        foreach(var ca in c.Artists.Where(ca => ca.ArtistId != primaryArtistId))
                        {
                            //if (ca.ArtistId == primaryArtistId) continue;
                            collaborators += $"{ca.FirstName} {ca.LastName} ";
                        }
                        if (collaborators.Length > 0)
                        {
                            collaborators = $"(with {collaborators})";
                        }
                        Console.WriteLine($" *{c.DesignIdeas} {collaborators}");
                    }
                }
            }
        }
        public static void RetrieveAllArtistsWhoHaveCovers()
        {
            PubContext context = new PubContext();
            var artistWithCovers = context.Artists.Where(a => a.Covers.Any()).ToList();

        }
        public static void RetrieveACoverWithItsArtists()
        {
            PubContext context = new PubContext();
            var coverWithArtists = context.Covers.Include(c => context.Artists).FirstOrDefault(c => c.CoverId == 1);
        }
        public static void RetrieveAnArtistWithTheirCovers()
        {
            PubContext context = new PubContext();
            var artistWithCovers = context.Artists.Include(a => a.Covers).FirstOrDefault(a => a.ArtistId == 1);
        }
        public static void CreateNewCoverAndArtistTogether()
        {
            PubContext context = new PubContext();
            var newArtist = new Artist { FirstName = "Kir", LastName = "Talmage" };
            var newCover = new Cover { DesignIdeas = "We Like birds!" };
            newArtist.Covers.Add(newCover);
            context.Artists.Add(newArtist);
            context.SaveChanges();
        }
        public static void CreateNewCoverWithExistingArtist()
        {
            PubContext context = new PubContext();
            var artistA = context.Artists.Find(2);
            var cover = new Cover { DesignIdeas = "Is it good if we use blue color" };
            //cover.Artists.Add(artistA);
            artistA.Covers.Add(cover);
            //context.Covers.Add(cover);
            context.SaveChanges();
        }
        public static void ConnectExisitngArtistAndCoverObjects()
        {
            PubContext context = new PubContext();
            var artistA = context.Artists.Find(1);
            var artistB = context.Artists.Find(2);
            var coverA = context.Covers.Find(1);
            coverA.Artists.Add(artistA);
            coverA.Artists.Add(artistB);
            context.SaveChanges();
        }
        public static void CascadeDeleteInActionWhenTracked()
        {
            var context = new PubContext();
            var author = context.Authors.Include(a => a.Books).FirstOrDefault(a => a.Id == 5);
            context.Authors.Remove(author);
            //add,rmove,update will detect changes
            var state = context.ChangeTracker.DebugView.ShortView;
            context.SaveChanges();
        }
        public static void LazyLoadBooksFromAnAuthor()
        {
            //enable navigation proerty in every entity must virtual
            //Reference the microsoft.EntityFramework.Proxies package
            //Use the proxy logic provided by that package - optionBuilder.UseLazyLoadingProxies()
            var context = new PubContext();
            var author = context.Authors.FirstOrDefault(a => a.LastName == "Gopisetti");
            foreach(var book in author.Books)
            {
                Console.WriteLine(book.Title);
            }
        }
        public static void FilterUsingRelatedData()
        {
            var context = new PubContext();
            var recentAuthors = context.Authors.Where(a => a.Books.Any(b => b.PublishDate.Year >= 2012)).ToList();
            DisplayAuthors(recentAuthors);
        }
        public static void ExplicitLoadCollection()
        {
            var context = new PubContext();
            var author = context.Authors.FirstOrDefault(a => a.LastName == "Newf");
            context.Entry(author).Collection(a => a.Books).Load();
        }
        public static void Projections()
        {
            var context = new PubContext();
            var UnknownTypes = context.Authors.Select(a => new
            {
                AuthorId = a.Id,
                Name = a.FirstName.First() + "" + a.LastName,
                Books = a.Books
            }).ToList();
            var debugView = context.ChangeTracker.DebugView.ShortView;
        }
        public static void EagerLoadBooksWithAuthors()
        {
            var context = new PubContext();
            var authors = context.Authors.Include(a => a.Books).ToList();
            DisplayAuthors(authors);

        }
        public static void AddNewBookToExistingAuthorInMemoryViaBook()
        {
            var context = new PubContext();
            var book = new Book
            {
                Title = "Dont make assumptions of the behaviour",
                PublishDate = new DateTime(2013,12,1)
            };
            book.Author = context.Authors.Find(14);
            context.Books.Add(book);
            context.SaveChanges();
        }
        public static void AddNewBookToExistingInMemory()
        {
            var context = new PubContext();
            var author = context.Authors.FirstOrDefault(a => a.LastName == "Gopisetti");
            if (author != null)
            {
                author.Books.Add(new Book { Title = "Wool", PublishDate = new DateTime(2012,1,1) });
            }
            context.SaveChanges();
        }
        public static void InsertNewAuthorWithNewBook()
        {
            var context = new PubContext();
            var author = new Author { FirstName = "Lynda", LastName = "Rutledge" };
            author.Books.Add(new Book
            {
                Title = "West With Giraffes",
                PublishDate = new DateTime(2021, 2, 1)
            });
            context.Authors.Add(author);
            context.SaveChanges();
        }
        static void InsertMultipleAuthors()
        {
            using var context = new PubContext();
            List<Author> authors = new List<Author>() { new Author { FirstName = "Rhoda", LastName = "Lerman" },
                new Author { FirstName = "George", LastName = "Orwell" },
                new Author { FirstName = "Jane", LastName = "Austen" },
                new Author { FirstName = "Mark", LastName = "Twain" },
                new Author { FirstName = "Toni", LastName = "Morrison" }
            };
            context.Authors.AddRange(authors);
            context.SaveChanges();
        }
        static void DeleteAnAuthor()
        {
            using var context = new PubContext();
            var JosieDets = context.Authors.Find(3);
            if (JosieDets != null)
            {
                context.Authors.Remove(JosieDets);
                context.SaveChanges();
            }

        }
        static void CoordinateRetrieveAndUpdateAuthor()
        {
            var author = FindThatAuthor(2);
            if (author?.FirstName == "Kiran")
            {
                author.FirstName = "Uday";
                SaveThatAuthor(author);
            }
        }
        static void SaveThatAuthor(Author author)
        {
            using var anotherShortLivedContext = new PubContext();
            anotherShortLivedContext.Authors.Update(author);
            anotherShortLivedContext.SaveChanges();
        }
        static Author FindThatAuthor(int authorId)
        {
            using var shortLivedContext = new PubContext();
            return shortLivedContext.Authors.Find(authorId)!;
        }
        static void RetrieveAndUpdateMultipleAuthor()
        {
            using var context = new PubContext();
            var authors = context.Authors.Where(a => a.FirstName == "Josie").ToList();
            foreach (var author in authors)
            {
                author.LastName = "Newfa";
            }
            Console.WriteLine("Before" + context.ChangeTracker.DebugView.ShortView);
            context.ChangeTracker.DetectChanges();
            Console.WriteLine("After:" + context.ChangeTracker.DebugView.ShortView);
            context.SaveChanges();
        }
        static void RetrieveAndUpdateAuthor()
        {
            using var context = new PubContext();
            var author = context.Authors.FirstOrDefault(a => a.FirstName == "Josie" && a.LastName == "Newf");
            if (author != null)
            {
                author.FirstName = "Kiran";
                context.SaveChanges();
            }
        }
        static void InsertAuthor()
        {
            using var context = new PubContext();
            var author = new Author() { FirstName = "Ananth", LastName = "Gopisetti" };
            context.Authors.Add(author);
            context.SaveChanges();
        }
        static void QueryAggregateAsNotracking()
        {
            using var context = new PubContext();
            var authors = context.Authors.AsNoTracking().OrderBy(a => a.FirstName).FirstOrDefault(a => a.LastName.Equals("Lerman"));
            authors.FirstName = "July";
            context.SaveChanges();
            var list = new List<Author>();
            if (authors != null) list.Add(authors);
            DisplayAuthors(list);
        }
        static void QueryAggregate()
        {
            using var context = new PubContext();
            var authors = context.Authors.OrderBy(a => a.FirstName).FirstOrDefault(a => a.LastName.Equals("Lerman"));
            authors.FirstName = "Julia";
            var response = context.SaveChanges();
            Console.WriteLine(response);
            var list = new List<Author>();
            if (authors != null) list.Add(authors);
            DisplayAuthors(list);
        }
        static void QueryFilters()
        {
            using var context = new PubContext();
            //var name = "Josie";
            //var authors = context.Authors.Where(s => s.FirstName.Equals(name)).ToList();
            var pattern = "L";
            var authors = context.Authors.Where(a => a.LastName.Contains(pattern)).ToList();
            //var authors = context.Authors.Where(a => EF.Functions.Like(a.LastName, pattern)).ToList();
            DisplayAuthors(authors);
        }

        private static void DisplayAuthors(List<Author> authors)
        {
            foreach (var author in authors)
            {
                Console.WriteLine(author.FirstName + " " + author.LastName);
                foreach(var book in author.Books)
                {
                    Console.WriteLine($"{book.Title} {book.PublishDate}");
                    if (book.Cover != null)
                    {
                        Console.WriteLine(book.Cover.DesignIdeas);
                    }
                }
            }
        }

        static void GetSortedAuthors()
        {
            using var context = new PubContext();
            var authors = context.Authors.OrderByDescending(a => a.FirstName).ThenBy(a => a.LastName).ToList();
            DisplayAuthors(authors);
        }
        static void AddSomeAuthors()
        {
            using var context = new PubContext();
            context.Authors.Add(new Author { FirstName = "Rhoda", LastName = "Lerman" });
            context.Authors.Add(new Author { FirstName = "Don", LastName = "Jones" });
            context.Authors.Add(new Author { FirstName = "Jim", LastName = "Christopher" });
            context.Authors.Add(new Author { FirstName = "Don", LastName = "Haunts" });
            context.SaveChanges();
        }

        static void SkipAndTakeAuthors()
        {
            using var context = new PubContext();
            var groupSize = 2;
            for (int i = 0; i < 5; ++i)
            {
                Console.WriteLine($"Page:{i + 1}");
                var authors = context.Authors.Skip(groupSize * i).Take(groupSize);
                foreach (var author in authors)
                {
                    Console.WriteLine(author.FirstName + " " + author.LastName);
                }
            }
        }
        static void GetAuthors()
        {
            using var context = new PubContext();
            var authors = context.Authors.ToList();
            var lastAuthor = authors.LastOrDefault();
            Console.WriteLine($"{lastAuthor.FirstName} {lastAuthor.LastName}");
            foreach (var author in authors)
            {
                Console.WriteLine(author.FirstName + " " + author.LastName);
            }
        }
        static void GetAuthorsLazyLoad()
        {
            using var context = new PubContext();
            var authors = context.Authors.ToList();
            //var lastAuthor = authors.LastOrDefault();
            DisplayAuthors(authors);
        }
        static void AddAuthors()
        {
            using var context = new PubContext();
            var author = new Author { FirstName = "Josie", LastName = "Newf" };
            context.Authors.Add(author);
            context.SaveChanges();
        }
        static void AddAuthorWithBook()
        {

            var author = new Author { FirstName = "Andrew", LastName = "Newf" };
            author.Books.Add(new Book { Title = "New World", PublishDate = new DateTime(2010, 9, 1) });
            author.Books.Add(new Book { Title = "New World II", PublishDate = new DateTime(2012, 10, 11) });
            using var context = new PubContext();
            context.Authors.Add(author);
            context.SaveChanges();
        }
        static void GetauthorsWithBooks()
        {
            using var context = new PubContext();
            var authors = context.Authors.Include(a => a.Books).ToList();
            foreach (var author in authors)
            {
                Console.WriteLine($"{author.FirstName} {author.LastName}");
                foreach (var book in author.Books)
                {
                    Console.WriteLine($"{book.Title} {book.PublishDate}");
                }
            }
        }
    }
}
