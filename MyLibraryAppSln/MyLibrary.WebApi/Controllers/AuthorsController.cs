using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyLibrary.WebApi.DTOs.Request;
using MyLibrary.WebApi.Services.Contracts;

namespace MyLibrary.WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _authorService;
    private readonly IValidator<AuthorRequestDTO> _validator;

    public AuthorsController(IAuthorService authorService, IValidator<AuthorRequestDTO> validator)
    {
        _authorService = authorService;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var authors = await _authorService.GetAll();

        return Ok(authors);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var author = await _authorService.GetById(id);

        return Ok(author);
    }

    [HttpPost]
    public async Task<IActionResult> Create(AuthorRequestDTO authorDto)
    {
        var validationResult = await _validator.ValidateAsync(authorDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var author = await _authorService.Add(authorDto);

        return Ok(author);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, AuthorRequestDTO authorDto)
    {
        var author = await _authorService.Update(id, authorDto);
        if (author == null)
        {
            return NotFound();
        }

        return Ok(author);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var author = await _authorService.Delete(id);
        if (author == null)
        {
            return NotFound();
        }

        return Ok(author);
    }
}
