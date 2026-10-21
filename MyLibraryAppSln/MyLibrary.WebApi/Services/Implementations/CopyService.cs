using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyLibrary.WebApi.DTOs.Request;
using MyLibrary.WebApi.DTOs.Response;
using MyLibrary.WebApi.Models;
using MyLibrary.WebApi.Repositories.Contracts;
using MyLibrary.WebApi.Services.Contracts;

namespace MyLibrary.WebApi.Services.Implementations;

public class CopyService : ICopyService
{
    private readonly IBaseRepository<Copy> _copyRepository;
    private readonly IMapper _mapper;

    public CopyService(
        IBaseRepository<Copy> copyRepository, 
        IMapper mapper)
    {
        _copyRepository = copyRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CopyResponseDTO>> GetAll()
    {
        var copies = await _copyRepository
            .GetAll()
            .Include(c => c.Book)
            .AsNoTracking()
            .ToListAsync();

        var response = _mapper.Map<IEnumerable<CopyResponseDTO>>(copies);

        return response;
    }

    public async Task<CopyResponseDTO> GetById(int id)
    {
        var copy = await _copyRepository
            .FindByAsNoTracking(c => c.Id == id)
            .Include(c => c.Book)
            .FirstOrDefaultAsync();

        var response = _mapper.Map<CopyResponseDTO>(copy);

        return response;
    }

    public async Task<IEnumerable<CopyResponseDTO>> GetCopiesByBookId(int bookId)
    {
        var copiesByBook = await _copyRepository
            .FindByAsNoTracking(c => c.BookId == bookId)
            .Include(c => c.Book)
            .ToListAsync();

        var response = _mapper.Map<IEnumerable<CopyResponseDTO>>(copiesByBook);

        return response;
    }

    public async Task<CopyResponseDTO> Add(CopyRequestDTO request)
    {
        var copy = new Copy()
        {
            Barcode = request.Barcode,
            Location = request.Location,
            AcquisitionDate = request.AcquisitionDate,
            Condition = request.Condition,
            BookId  = request.BookId
        };

        await _copyRepository.Add(copy);
        var copyWithDetails = await _copyRepository
            .FindBy(c => c.Id == copy.Id)
            .Include(c => c.Book)
            .FirstOrDefaultAsync();

        var response = _mapper.Map<CopyResponseDTO>(copyWithDetails);

        return response;
    }

    public async Task<CopyResponseDTO> Update(int id, CopyRequestDTO request)
    {
        var copy = await _copyRepository
            .FindBy(c => c.Id == id)
            .Include(c => c.Book)
            .FirstOrDefaultAsync();

        copy.Barcode = request.Barcode;
        copy.Location = request.Location;
        copy.AcquisitionDate = request.AcquisitionDate;
        copy.Condition = request.Condition;
        copy.BookId = request.BookId;

        await _copyRepository.Update(copy);
        var response = _mapper.Map<CopyResponseDTO>(copy);

        return response;
    }

    public async Task<CopyResponseDTO> Delete(int id)
    {
        var copy = await _copyRepository
            .FindBy(c => c.Id == id)
            .Include(c => c.Book)
            .FirstOrDefaultAsync();

        await _copyRepository.Delete(copy);
        var response = _mapper.Map<CopyResponseDTO>(copy);

        return response;
    }
}
