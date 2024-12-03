using BookStoreAPI.Models.BookStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Data
{
    public class BookstoreContext : DbContext
    {
        public BookstoreContext(DbContextOptions<BookstoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Books
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "To Kill a Mockingbird",
                    Author = "Harper Lee",
                    Genre = "Fiction",
                    PublishedDate = new DateTime(1960, 7, 11),
                    Price = 9.99m
                },
                new Book
                {
                    Id = 2,
                    Title = "1984",
                    Author = "George Orwell",
                    Genre = "Dystopian",
                    PublishedDate = new DateTime(1949, 6, 8),
                    Price = 8.99m
                },
                new Book
                {
                    Id = 3,
                    Title = "Pride and Prejudice",
                    Author = "Jane Austen",
                    Genre = "Romance",
                    PublishedDate = new DateTime(1813, 1, 28),
                    Price = 7.99m
                },
                new Book
                {
                    Id = 4,
                    Title = "The Great Gatsby",
                    Author = "F. Scott Fitzgerald",
                    Genre = "Fiction",
                    PublishedDate = new DateTime(1925, 4, 10),
                    Price = 10.99m
                },
                new Book
                {
                    Id = 5,
                    Title = "The Catcher in the Rye",
                    Author = "J.D. Salinger",
                    Genre = "Fiction",
                    PublishedDate = new DateTime(1951, 7, 16),
                    Price = 8.99m
                },
                new Book
                {
                    Id = 6,
                    Title = "Moby-Dick",
                    Author = "Herman Melville",
                    Genre = "Adventure",
                    PublishedDate = new DateTime(1851, 10, 18),
                    Price = 11.99m
                },
                new Book
                {
                    Id = 7,
                    Title = "War and Peace",
                    Author = "Leo Tolstoy",
                    Genre = "Historical",
                    PublishedDate = new DateTime(1869, 1, 1),
                    Price = 12.99m
                },
                new Book
                {
                    Id = 8,
                    Title = "Crime and Punishment",
                    Author = "Fyodor Dostoevsky",
                    Genre = "Psychological",
                    PublishedDate = new DateTime(1866, 1, 1),
                    Price = 9.99m
                },
                new Book
                {
                    Id = 9,
                    Title = "The Hobbit",
                    Author = "J.R.R. Tolkien",
                    Genre = "Fantasy",
                    PublishedDate = new DateTime(1937, 9, 21),
                    Price = 9.99m
                },
                new Book
                {
                    Id = 10,
                    Title = "The Lord of the Rings",
                    Author = "J.R.R. Tolkien",
                    Genre = "Fantasy",
                    PublishedDate = new DateTime(1954, 7, 29),
                    Price = 19.99m
                },
                new Book
                {
                    Id = 11,
                    Title = "Harry Potter and the Sorcerer's Stone",
                    Author = "J.K. Rowling",
                    Genre = "Fantasy",
                    PublishedDate = new DateTime(1997, 6, 26),
                    Price = 10.99m
                },
                new Book
                {
                    Id = 12,
                    Title = "Brave New World",
                    Author = "Aldous Huxley",
                    Genre = "Dystopian",
                    PublishedDate = new DateTime(1932, 8, 18),
                    Price = 8.99m
                },
                new Book
                {
                    Id = 13,
                    Title = "The Alchemist",
                    Author = "Paulo Coelho",
                    Genre = "Adventure",
                    PublishedDate = new DateTime(1988, 1, 1),
                    Price = 9.99m
                },
                new Book
                {
                    Id = 14,
                    Title = "The Kite Runner",
                    Author = "Khaled Hosseini",
                    Genre = "Fiction",
                    PublishedDate = new DateTime(2003, 5, 29),
                    Price = 9.99m
                },
                new Book
                {
                    Id = 15,
                    Title = "The Hunger Games",
                    Author = "Suzanne Collins",
                    Genre = "Dystopian",
                    PublishedDate = new DateTime(2008, 9, 14),
                    Price = 8.99m
                },
                new Book
                {
                    Id = 16,
                    Title = "Animal Farm",
                    Author = "George Orwell",
                    Genre = "Political Satire",
                    PublishedDate = new DateTime(1945, 8, 17),
                    Price = 7.99m
                },
                new Book
                {
                    Id = 17,
                    Title = "Jane Eyre",
                    Author = "Charlotte Brontë",
                    Genre = "Gothic",
                    PublishedDate = new DateTime(1847, 10, 16),
                    Price = 8.99m
                },
                new Book
                {
                    Id = 18,
                    Title = "Wuthering Heights",
                    Author = "Emily Brontë",
                    Genre = "Gothic",
                    PublishedDate = new DateTime(1847, 12, 1),
                    Price = 8.99m
                },
                new Book
                {
                    Id = 19,
                    Title = "Frankenstein",
                    Author = "Mary Shelley",
                    Genre = "Gothic",
                    PublishedDate = new DateTime(1818, 1, 1),
                    Price = 7.99m
                }
            );

            // Ensure the Title column can't have empty strings
            modelBuilder.Entity<Book>()
                .Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(200);

            // Ensure the Author column can't have empty strings
            modelBuilder.Entity<Book>()
                .Property(b => b.Author)
                .IsRequired();

            // Ensure the Genre column can't have empty strings
            modelBuilder.Entity<Book>()
                .Property(b => b.Genre)
                .IsRequired();

            // Ensure the Price column is configured properly
            modelBuilder.Entity<Book>()
                .Property(b => b.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
        }
    }
}