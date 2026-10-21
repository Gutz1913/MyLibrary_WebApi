using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyLibrary.WebApi.DTOs.Request;
using MyLibrary.WebApi.Services.Contracts;

namespace MyLibrary.WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class FinesController : ControllerBase
{
    private readonly IFineService _fineService;
    private readonly IValidator<FineRequestDTO> _validator;

    public FinesController(IFineService fineService, IValidator<FineRequestDTO> validator)
    {
        _fineService = fineService;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var fines = await _fineService.GetAll();

        return Ok(fines);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var fine = await _fineService.GetById(id);

        return Ok(fine);
    }

    [HttpGet("get-fines-by-userid/{userId}")]
    public async Task<IActionResult> GetFinesByUser(int userId)
    {
        var fines = await _fineService.GetFinesByUserId(userId);

        return Ok(fines);
    }

    [HttpGet("get-fines-by-loanid/{loanId}")]
    public async Task<IActionResult> GetFinesByLoanId(int loanId)
    {
        var fines = await _fineService.GetFinesByLoanId(loanId);

        return Ok(fines);
    }

    [HttpPost]
    public async Task<IActionResult> Create(FineRequestDTO fineDto)
    {
        var validationResult = await _validator.ValidateAsync(fineDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var fine = await _fineService.Add(fineDto);

        return Ok(fine);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, FineRequestDTO fineDto)
    {
        var fine = await _fineService.Update(id, fineDto);
        if (fine == null)
        {
            return NotFound();
        }

        return Ok(fine);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var fine = await _fineService.Delete(id);
        if (fine == null)
        {
            return NotFound();
        }

        return Ok(fine);
    }
}
