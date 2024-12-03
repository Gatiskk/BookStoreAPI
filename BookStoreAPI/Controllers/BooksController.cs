using BookStoreAPI.Data;
using BookStoreAPI.Models;
using BookStoreAPI.Models.BookStoreAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;  // Make sure to add this for logging

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly BookstoreContext _context;
    private readonly ILogger<BooksController> _logger;  // Injecting ILogger for logging

    public BooksController(BookstoreContext context, ILogger<BooksController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // 1. GET /api/books: Retrieves all books
    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        try
        {
            _logger.LogInformation("Entered GetAllBooks method.");

            var books = await _context.Books.ToListAsync();

            // Log count of books fetched
            _logger.LogInformation("Successfully retrieved {Count} books.", books.Count);

            if (books == null || !books.Any())
            {
                _logger.LogInformation("No books found in the database.");
                return NotFound("No books found.");
            }

            return Ok(books);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching all books.");
            return StatusCode(500, "Internal server error.");
        }  
}

// 2. GET /api/books/{id}: Retrieves a specific book by ID
[HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        _logger.LogInformation("Fetching book with ID: {BookId}", id);

        try
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                _logger.LogWarning("Book with ID {BookId} not found.", id);
                return NotFound();
            }

            _logger.LogInformation("Successfully retrieved book with ID: {BookId}", id);
            return Ok(book);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching book with ID: {BookId}", id);
            return StatusCode(500, "Internal server error.");
        }
    }

    // 3. POST /api/books: Adds a new book (Admin only)
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateBook([FromBody] Book book)
    {
        _logger.LogInformation("Adding new book with title: {BookTitle}", book.Title);

        try
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Successfully added book with ID: {BookId}", book.Id);
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while adding book with title: {BookTitle}", book.Title);
            return StatusCode(500, "Internal server error.");
        }
    }

    // 4. PUT /api/books/{id}: Updates an existing book (Admin only)
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] Book updatedBook)
    {
        _logger.LogInformation("Updating book with ID: {BookId}", id);

        try
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                _logger.LogWarning("Book with ID {BookId} not found for update.", id);
                return NotFound();
            }

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Price = updatedBook.Price;
            book.PublishedDate = updatedBook.PublishedDate;
            book.Genre = updatedBook.Genre;

            await _context.SaveChangesAsync();
            _logger.LogInformation("Successfully updated book with ID: {BookId}", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating book with ID: {BookId}", id);
            return StatusCode(500, "Internal server error.");
        }
    }

    // 5. DELETE /api/books/{id}: Deletes a book (Admin only)
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        _logger.LogInformation("Deleting book with ID: {BookId}", id);

        try
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                _logger.LogWarning("Book with ID {BookId} not found for deletion.", id);
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Successfully deleted book with ID: {BookId}", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting book with ID: {BookId}", id);
            return StatusCode(500, "Internal server error.");
        }
    }

    // 6. GET /api/books/genre/{genre}: Retrieves books by genre with pagination and sorting
    [HttpGet("genre/{genre}")]
    public async Task<IActionResult> GetBooksByGenre(string genre, int page = 1, int size = 5, string sortBy = "asc")
    {
        try
        {
            var query = _context.Books
                .Where(b => b.Genre.ToLower() == genre.ToLower())
                .AsQueryable();

            // Sorting logic
            if (sortBy.ToLower() == "asc")
            {
                query = query.OrderBy(b => b.PublishedDate);
            }
            else if (sortBy.ToLower() == "desc")
            {
                query = query.OrderByDescending(b => b.PublishedDate);
            }

            // Paginate results
            var books = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            // Ensure Ok() is explicitly returned for an empty or populated list
            return Ok(books);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching books by genre.");
            return StatusCode(500, "Internal server error.");
        }
    }
}
