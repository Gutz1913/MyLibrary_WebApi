using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyLibrary.WebApi.DTOs.Request;
using MyLibrary.WebApi.DTOs.Response;
using MyLibrary.WebApi.Models;
using MyLibrary.WebApi.Repositories.Contracts;
using MyLibrary.WebApi.Services.Contracts;

namespace MyLibrary.WebApi.Services.Implementations;

public class FineService : IFineService
{
    private readonly IBaseRepository<Fine> _fineRepository;
    private readonly IMapper _mapper;

    public FineService(IBaseRepository<Fine> fineRepository, IMapper mapper)
    {
        _fineRepository = fineRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<FineResponseDTO>> GetAll()
    {
        var fines = await _fineRepository
            .GetAll()
            .Include(f => f.Loan)
            .Include(f => f.User)
            .AsNoTracking()
            .ToListAsync();

        var response = _mapper.Map<IEnumerable<FineResponseDTO>>(fines);

        return response;
    }

    public async Task<FineResponseDTO> GetById(int id)
    {
        var fine = await _fineRepository
            .FindByAsNoTracking(f => f.Id == id)
            .Include(f => f.Loan)
            .Include(f => f.User)
            .FirstOrDefaultAsync();

        var response = _mapper.Map<FineResponseDTO>(fine);

        return response;
    }

    public async Task<IEnumerable<FineResponseDTO>> GetFinesByLoanId(int loanId)
    {
        var finesByLoan = await _fineRepository
            .FindByAsNoTracking(f => f.LoanId == loanId)
            .Include(f => f.Loan)
            .Include(f => f.User)
            .ToListAsync();

        var response = _mapper.Map<IEnumerable<FineResponseDTO>>(finesByLoan);

        return response;
    }

    public async Task<IEnumerable<FineResponseDTO>> GetFinesByUserId(int userId)
    {
        var finesByUser = await _fineRepository
            .FindByAsNoTracking(f => f.UserId == userId)
            .Include(f => f.Loan)
            .Include(f => f.User)
            .ToListAsync();

        var response = _mapper.Map<IEnumerable<FineResponseDTO>>(finesByUser);

        return response;
    }

    public async Task<FineResponseDTO> Add(FineRequestDTO request)
    {
        var fine = new Fine()
        {
            LoanId = request.LoanId,
            UserId = request.UserId,
            FineDate = request.FineDate,
            Reason = request.Reason,
            Amount = request.Amount,
            State = request.State,
            PaymentDate = request.PaymentDate
        };

        await _fineRepository.Add(fine);
        var fineWithDetails = await _fineRepository
            .FindBy(f => f.Id == fine.Id)
            .Include(f => f.Loan)
            .Include(f => f.User)
            .FirstOrDefaultAsync();

        var response = _mapper.Map<FineResponseDTO>(fineWithDetails);

        return response;
    }

    public async Task<FineResponseDTO> Update(int id, FineRequestDTO request)
    {
        var fine = await _fineRepository
            .FindBy(f => f.Id == id)
            .Include(f => f.Loan)
            .Include(f => f.User)
            .FirstOrDefaultAsync();

        fine.LoanId = request.LoanId;
        fine.UserId = request.UserId;
        fine.FineDate = request.FineDate;
        fine.Reason = request.Reason;
        fine.Amount = request.Amount;
        fine.State = request.State;
        fine.PaymentDate = request.PaymentDate;

        await _fineRepository.Update(fine);

        var fineWithDetails = await _fineRepository
            .FindBy(f => f.Id == id)
            .Include(f => f.Loan)
            .Include(f => f.User)
            .FirstOrDefaultAsync();

        var response = _mapper.Map<FineResponseDTO>(fineWithDetails);

        return response;
    }

    public async Task<FineResponseDTO> Delete(int id)
    {
        var fine = await _fineRepository
            .FindBy(f => f.Id == id)
            .Include(f => f.Loan)
            .Include(f => f.User)
            .FirstOrDefaultAsync();

        await _fineRepository.Delete(fine);
        var response = _mapper.Map<FineResponseDTO>(fine);

        return response;
    }
}
