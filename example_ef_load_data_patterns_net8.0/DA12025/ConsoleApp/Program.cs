using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp;

class Program
{
    static void Main(string[] args)
    {
        AppDbContext context = new AppDbContext();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        SeedData(context);

        /*
         * Entity Framework Core allows you to use the navigation properties in your model to load related entities.
         * There are three common ORM patterns used to load related data:
         * Eager loading means that the related data is loaded from the database as part of the initial query.
         * Lazy loading means that the related data is transparently loaded from the database when the navigation
         * property is accessed.
         * Explicit loading means that the related data is explicitly loaded from the database at a later time.
         */

        Console.WriteLine("=== EAGER LOADING WITH MULTIPLE RELATIONSHIPS ===");
        var books = context.Books
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .Include(b => b.Genres)
            .ToList();

        foreach (var book in books)
        {
            String genres = string.Join(", ", book.Genres.Select(g => g.Name));
            Console.WriteLine(
                $"Book: {book.Title}, Written By: {book.Author.Name}, Publisher: {book.Publisher.Name}, Genres: {genres}");
        }

        Console.WriteLine("\n=== LAZY LOADING OF A BOOK PUBLISHER ===");
        Publisher publisher = context.Publishers.First();
        foreach (var b in publisher.Books)
        {
            Console.WriteLine($"Publisher: {publisher.Name}, Published: {b.Title}");
        }

        Console.WriteLine("\n=== EXPLICIT LOADING WITH SPECIFIC BOOK GENRE ===");
        Book specificBook = context.Books.First();
        context.Entry(specificBook).Collection(b => b.Genres).Load();
        Console.WriteLine(
            $"Book:'{specificBook.Title}', Genres: {string.Join(", ", specificBook.Genres.Select(g => g.Name))}");
    }

    private static void SeedData(AppDbContext context)
    {
        if (context.Books.Any()) return;

        Author author1 = new Author { Name = "J.K. Rowling" };
        Author author2 = new Author { Name = "George Orwell" };

        Publisher publisher1 = new Publisher { Name = "Scholastic Corporation" };
        Publisher publisher2 = new Publisher { Name = "Harcourt, Brace and Company" };

        Genre genre1 = new Genre { Name = "Fantasy" };
        Genre genre2 = new Genre { Name = "Coming-of-Age" };
        Genre genre3 = new Genre { Name = "Allegory" };
        Genre genre4 = new Genre { Name = "Dystopian Fiction" };

        Book book1 = new Book
        {
            Id = 1,
            Title = "Harry Potter and the Prisoner of Azkaban",
            Author = author1,
            Publisher = publisher1,
            Genres = new List<Genre> { genre1, genre2 }
        };

        Book book2 = new Book
        {
            Id = 2,
            Title = "Animal Farm",
            Author = author2,
            Publisher = publisher2,
            Genres = new List<Genre> { genre3, genre4 }
        };

        context.AddRange(author1, author2);
        context.AddRange(publisher1, publisher2);
        context.AddRange(genre1, genre2, genre3, genre4);
        context.AddRange(book1, book2);
        context.SaveChanges();
    }
}