using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyLibrary.WebApi.DTOs.Request;
using MyLibrary.WebApi.Services.Contracts;

namespace MyLibrary.WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly IValidator<BookRequestDTO> _validator;

    public BooksController(IBookService bookService, IValidator<BookRequestDTO> validator)
    {
        _bookService = bookService;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var books = await _bookService.GetAll();

        return Ok(books);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _bookService.GetById(id);

        return Ok(book);
    }

    [HttpGet("get-books-by-category/{categoryId}")]
    public async Task<IActionResult> GetBooksByCategoryId(int categoryId)
    {
        var books = await _bookService.GetBooksByCategoryId(categoryId);

        return Ok(books);
    }

    [HttpGet("get-books-by-author/{authorId}")]
    public async Task<IActionResult> GetBooksByAuthorId(int authorId)
    {
        var books = await _bookService.GetBooksByAuthorId(authorId);

        return Ok(books);
    }

    [HttpPost]
    public async Task<IActionResult> Create(BookRequestDTO bookDto)
    {
        var validationResult = await _validator.ValidateAsync(bookDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var book = await _bookService.Add(bookDto);

        return Ok(book);
    }

    [HttpPost("register-categories")]
    public async Task<IActionResult> RegisterCategory(BookCategoryRequestDTO request)
    {
        try
        {
            await _bookService.RegisterCategoryAsync(request);
            return Ok(new { message = "Categoría registrada exitosamente." });
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPost("register-authors")]
    public async Task<IActionResult> RegisterAuthor(BookAuthorRequestDTO request)
    {
        try
        {
            await _bookService.RegisterAuthorAsync(request);
            return Ok(new { message = "Autor registrado exitosamente." });
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, BookRequestDTO bookDto)
    {
        var book = await _bookService.Update(id, bookDto);
        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var book = await _bookService.Delete(id);
        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }
}
