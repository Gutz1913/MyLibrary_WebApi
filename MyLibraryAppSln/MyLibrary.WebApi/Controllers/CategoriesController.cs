using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyLibrary.WebApi.DTOs.Request;
using MyLibrary.WebApi.Services.Contracts;

namespace MyLibrary.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IValidator<CategoryRequestDTO> _validator;

    public CategoriesController(ICategoryService categoryService, IValidator<CategoryRequestDTO> validator)
    {
        _categoryService = categoryService;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryService.GetAll();

        return Ok(categories);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await _categoryService.GetById(id);

        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryRequestDTO categoryDto)
    {
        var validationResult = await _validator.ValidateAsync(categoryDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var category = await _categoryService.Add(categoryDto);

        return Ok(category);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CategoryRequestDTO categoryDto)
    {
        var category = await _categoryService.Update(id, categoryDto);
        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _categoryService.Delete(id);
        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }
}
