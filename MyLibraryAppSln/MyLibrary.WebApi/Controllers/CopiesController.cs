using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyLibrary.WebApi.DTOs.Request;
using MyLibrary.WebApi.Services.Contracts;

namespace MyLibrary.WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CopiesController : ControllerBase
{
    private readonly ICopyService _copyService;
    private readonly IValidator<CopyRequestDTO> _validator;

    public CopiesController(ICopyService copyService, IValidator<CopyRequestDTO> validator)
    {
        _copyService = copyService;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var copies = await _copyService.GetAll();

        return Ok(copies);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var copy = await _copyService.GetById(id);

        return Ok(copy);
    }

    [HttpGet("get-copies-by-bookid/{bookId}")]
    public async Task<IActionResult> GetCopiesByBookId(int bookId)
    {
        var copies = await _copyService.GetCopiesByBookId(bookId);

        return Ok(copies);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CopyRequestDTO copyDto)
    {
        var validationResult = await _validator.ValidateAsync(copyDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var copy = await _copyService.Add(copyDto);

        return Ok(copy);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CopyRequestDTO copyDto)
    {
        var copy = await _copyService.Update(id, copyDto);
        if (copy == null)
        {
            return NotFound();
        }

        return Ok(copy);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var copy = await _copyService.Delete(id);
        if (copy == null)
        {
            return NotFound();
        }

        return Ok(copy);
    }
}
