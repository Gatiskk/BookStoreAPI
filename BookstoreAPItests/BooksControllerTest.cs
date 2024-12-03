using BookStoreAPI.Data;
using BookStoreAPI.Models.BookStoreAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // Ensure this namespace is included
using Microsoft.AspNetCore.Mvc;
using Moq;
public class BooksControllerTests
{
    private readonly BooksController _controller;
    private readonly Mock<BookstoreContext> _mockContext;
    private readonly Mock<ILogger<BooksController>> _mockLogger;

    public BooksControllerTests()
    {
        var options = new DbContextOptionsBuilder<BookstoreContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _mockContext = new Mock<BookstoreContext>(options);
        _mockLogger = new Mock<ILogger<BooksController>>(); // Mock the logger

        // Mocking the DbSet
        var books = new List<Book>
        {
            new Book { Id = 1, Title = "Test Book 1", Author = "Test Author", Genre = "Fiction", Price = 10.99M },
            new Book { Id = 2, Title = "Test Book 2", Author = "Another Author", Genre = "Non-fiction", Price = 15.99M }
        }.AsQueryable();

        var mockSet = new Mock<DbSet<Book>>();

        mockSet.As<IQueryable<Book>>()
            .Setup(m => m.Provider)
            .Returns(books.Provider);
        mockSet.As<IQueryable<Book>>()
            .Setup(m => m.Expression)
            .Returns(books.Expression);
        mockSet.As<IQueryable<Book>>()
            .Setup(m => m.ElementType)
            .Returns(books.ElementType);
        mockSet.As<IQueryable<Book>>()
            .Setup(m => m.GetEnumerator())
            .Returns(books.GetEnumerator());

        mockSet.As<IAsyncEnumerable<Book>>()
            .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
            .Returns(new TestAsyncEnumerator<Book>(books.GetEnumerator()));

        _mockContext.Setup(c => c.Books).Returns(mockSet.Object);

        // Pass the mock logger to the controller
        _controller = new BooksController(_mockContext.Object, _mockLogger.Object);
    }

[Fact]
        public async Task GetAllBooks_ReturnsOkResult()
        {
            // Act
            var result = await _controller.GetAllBooks();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<Book>>(okResult.Value);
            Assert.NotEmpty(returnValue);
        }

        [Fact]
        public async Task GetBookById_WhenBookDoesNotExist_ReturnsNotFound()
        {
            // Act
            var result = await _controller.GetBookById(99);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

    // Async enumerator to mock async operations
    public class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
        {
            private readonly IEnumerator<T> _inner;

            public TestAsyncEnumerator(IEnumerator<T> inner)
            {
                _inner = inner;
            }

            public T Current => _inner.Current;

            public ValueTask<bool> MoveNextAsync() => new ValueTask<bool>(_inner.MoveNext());

            public ValueTask DisposeAsync() => new ValueTask();
        }
    }