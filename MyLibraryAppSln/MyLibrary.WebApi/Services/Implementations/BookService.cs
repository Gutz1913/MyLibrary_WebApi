using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyLibrary.WebApi.DTOs.Request;
using MyLibrary.WebApi.DTOs.Response;
using MyLibrary.WebApi.Models;
using MyLibrary.WebApi.Repositories.Contracts;
using MyLibrary.WebApi.Services.Contracts;

namespace MyLibrary.WebApi.Services.Implementations;

public class BookService : IBookService
{
    private readonly IBaseRepository<Book> _bookRepository;
    private readonly IBaseRepository<Author> _authorRepository;
    private readonly IBaseRepository<Category> _categoryRepository;
    private readonly IBaseRepository<BookAuthor> _bookAuthorRepository;
    private readonly IBaseRepository<BookCategory> _bookCategoryRepository;
    private readonly IMapper _mapper;

    public BookService(
        IBaseRepository<Book> bookRepository, 
        IBaseRepository<Author> authorRepository, 
        IBaseRepository<Category> categoryRepository, 
        IBaseRepository<BookAuthor> bookAuthorRepository, 
        IBaseRepository<BookCategory> bookCategoryRepository, 
        IMapper mapper)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
        _categoryRepository = categoryRepository;
        _bookAuthorRepository = bookAuthorRepository;
        _bookCategoryRepository = bookCategoryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BookResponseDTO>> GetAll()
    {
        var books = await _bookRepository
            .GetAll()
            .Include(b => b.Publisher)
            .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
            .Include(b => b.BookCategories)
                .ThenInclude(ba => ba.Category)
            .Include(b => b.Copies)
            .ToListAsync();

        var response = _mapper.Map<IEnumerable<BookResponseDTO>>(books);

        return response;
    }

    public async Task<BookResponseDTO> GetById(int id)
    {
        var book = await _bookRepository
            .FindByAsNoTracking(b => b.Id == id)
            .Include(b => b.Publisher)
            .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
            .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
            .Include(b => b.Copies)
            .FirstOrDefaultAsync();

        var response = _mapper.Map<BookResponseDTO>(book);

        return response;
    }

    public async Task<IEnumerable<BookCategoryResponseDTO>> GetBooksByCategoryId(int categoryId)
    {
        var booksByCategory = await _bookCategoryRepository
            .FindByAsNoTracking(b => b.CategoryId == categoryId)
            .Include(bc => bc.Book)            
            .Include(bc => bc.Category)
            .ToListAsync();

        var response = _mapper.Map<IEnumerable<BookCategoryResponseDTO>>(booksByCategory);

        return response;
    }

    public async Task<IEnumerable<BookAuthorResponseDTO>> GetBooksByAuthorId(int authorId)
    {
        var booksByAuthor = await _bookAuthorRepository
            .FindByAsNoTracking(b => b.AuthorId == authorId)
            .Include(ba => ba.Book)
            .Include(ba => ba.Author)
            .ToListAsync();

        var response = _mapper.Map<IEnumerable<BookAuthorResponseDTO>>(booksByAuthor);

        return response;
    }

    public async Task<BookResponseDTO> Add(BookRequestDTO request)
    {
        var book = new Book()
        {
            Isbn = request.Isbn,
            Title = request.Title,
            Subtitle = request.Subtitle,
            PublicationYear = request.PublicationYear,
            Language = request.Language,
            NumPages = request.NumPages,
            Synopsis = request.Synopsis,
            Active = request.Active,
            PublisherId = request.PublisherId
        };

        await _bookRepository.Add(book);
        var bookWithDetails = await _bookRepository
            .FindBy(b => b.Id == book.Id)
            .Include(b => b.Publisher)
            .Include(b => b.BookAuthors)
            .ThenInclude(ba => ba.Author)
            .Include(b => b.BookCategories)
            .ThenInclude(bc => bc.Category)
            .FirstOrDefaultAsync();

        var response = _mapper.Map<BookResponseDTO>(bookWithDetails);

        return response;
    }

    public async Task<BookResponseDTO> Update(int id, BookRequestDTO request)
    {
        var book = await _bookRepository
            .FindBy(b => b.Id == id)
            .Include(b => b.Publisher)
            .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
            .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
            .FirstOrDefaultAsync();

        book.Isbn = request.Isbn;
        book.Title = request.Title;
        book.Subtitle = request.Subtitle;
        book.PublicationYear = request.PublicationYear;
        book.Language = request.Language;
        book.NumPages = request.NumPages;
        book.Synopsis = request.Synopsis;
        book.Active = request.Active;
        book.PublisherId = request.PublisherId;

        await _bookRepository.Update(book);
        var response = _mapper.Map<BookResponseDTO>(book);

        return response;
    }

    public async Task<BookResponseDTO> Delete(int id)
    {
        var book = await _bookRepository
            .FindBy(b => b.Id == id)
            .Include(b => b.Publisher)
            .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
            .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
            .Include(b => b.Copies)
            .FirstOrDefaultAsync();

        await _bookRepository.Delete(book);
        var response = _mapper.Map<BookResponseDTO>(book);

        return response;
    }

    public async Task RegisterAuthorAsync(BookAuthorRequestDTO request)
    {
        var book = await _bookRepository
            .FindBy(b => b.Id == request.BookId)
            .Include(b => b.Publisher)
            .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
            .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
            .FirstOrDefaultAsync();

        if (book == null)
            throw new KeyNotFoundException($"Libro con ID: {request.BookId} no encontrado.");

        var author = await _authorRepository
            .FindBy(a => a.Id == request.AuthorId)
            .FirstOrDefaultAsync();

        if (author == null)
            throw new KeyNotFoundException($"Autor con ID: {request.AuthorId} no encontrado.");

        var bookAuthor = new BookAuthor()
        {
            BookId = request.BookId,
            AuthorId = request.AuthorId,
            RegisteredAt = DateTime.UtcNow
        };

        await _bookAuthorRepository.Add(bookAuthor);
        _mapper.Map<BookAuthorResponseDTO>(bookAuthor);
    }

    public async Task RegisterCategoryAsync(BookCategoryRequestDTO request)
    {
        var book = await _bookRepository
            .FindBy(b => b.Id == request.BookId)
            .Include(b => b.Publisher)
            .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
            .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
            .FirstOrDefaultAsync();

        if (book == null)
            throw new KeyNotFoundException($"Libro con ID: {request.BookId} no encontrado.");

        var category = await _categoryRepository
            .FindBy(c => c.Id == request.CategoryId)
            .FirstOrDefaultAsync();

        if (category == null)
            throw new KeyNotFoundException($"Categoría con ID: {request.CategoryId} no encontrada.");

        var bookCategory = new BookCategory()
        {
            BookId = request.BookId,
            CategoryId = request.CategoryId,
            RegisteredAt = DateTime.UtcNow
        };

        await _bookCategoryRepository.Add(bookCategory);
        _mapper.Map<BookCategoryResponseDTO>(bookCategory);
    }
}
