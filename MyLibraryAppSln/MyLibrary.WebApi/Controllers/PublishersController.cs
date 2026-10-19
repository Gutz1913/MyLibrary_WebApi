using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyLibrary.WebApi.DTOs.Request;
using MyLibrary.WebApi.Services.Contracts;

namespace MyLibrary.WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class PublishersController : ControllerBase
{
    private readonly IPublisherService _publisherService;
    private readonly IValidator<PublisherRequestDTO> _validator;

    public PublishersController(IPublisherService publisherService, IValidator<PublisherRequestDTO> validator)
    {
        _publisherService = publisherService;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var publishers = await _publisherService.GetAll();

        return Ok(publishers);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var publisher = await _publisherService.GetById(id);

        return Ok(publisher);
    }

    [HttpPost]
    public async Task<IActionResult> Create(PublisherRequestDTO publisherDto)
    {
        var validationResult = await _validator.ValidateAsync(publisherDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var publisher = await _publisherService.Add(publisherDto);

        return Ok(publisher);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, PublisherRequestDTO publisherDto)
    {
        var publisher = await _publisherService.Update(id, publisherDto);
        if (publisher == null)
        {
            return NotFound();
        }

        return Ok(publisher);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var publisher = await _publisherService.Delete(id);
        if (publisher == null)
        { 
            NotFound(); 
        }

        return Ok(publisher);        
    }
}
