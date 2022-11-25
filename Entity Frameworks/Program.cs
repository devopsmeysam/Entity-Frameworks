using Entity_Framework;
using Entity_Frameworks;
using Microsoft.EntityFrameworkCore;

class Program
{
    static IEnumerable<Author> CreateFakeData()
    {
        var authors = new List<Author>
        {
            new Author
            {
                Name = "Jane Austen", Books = new List<Book>
                {
                    new Book {Title = "Emma", PublicationYear = 1815},
                    new Book {Title = "Persuasion", PublicationYear = 1818},
                    new Book {Title = "Mansfield Park", PublicationYear = 1814}
                }
            },
            new Author
            {
                Name = "Ian Fleming", Books = new List<Book>
                {
                    new Book {Title = "Dr No", PublicationYear = 1958},
                    new Book {Title = "Goldfinger", PublicationYear = 1959},
                    new Book {Title = "From Russia with Love", PublicationYear = 1957}
                }
            }
        };
        return authors;
    }

    static void Main()
    {
        var options = new DbContextOptionsBuilder<BooksContext>()
            .UseSqlite("")
            .Options;
        
        using var db = new BooksContext(options);
        db.Database.EnsureCreated();

        //var authors = CreateFakeData();

        //db.Authors.AddRange(authors);
        //db.SaveChanges();

        var recentBooks = from b in db.Books where b.PublicationYear > 1900 select b;

        foreach (var book in recentBooks.Include(b => b.Author))
        {
            Console.WriteLine($"{book} is by {book.Author}");
        }

        //foreach (var author in db.Authors)
        //{
        //    Console.WriteLine($"{author} wrote...");

        //    foreach (var book in author.Books)
        //    {
        //        Console.WriteLine($"   {book}");
        //        Console.WriteLine();
        //    }
        //}
    }
}