using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyLibrary.WebApi.DTOs.Request;
using MyLibrary.WebApi.DTOs.Response;
using MyLibrary.WebApi.Models;
using MyLibrary.WebApi.Repositories.Contracts;
using MyLibrary.WebApi.Services.Contracts;

namespace MyLibrary.WebApi.Services.Implementations;

public class LoanService : ILoanService
{
    private readonly IBaseRepository<Loan> _loanRepository;
    private readonly IMapper _mapper;

    public LoanService(IBaseRepository<Loan> loanRepository, IMapper mapper)
    {
        _loanRepository = loanRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<LoanResponseDTO>> GetAll()
    {
        var loans = await _loanRepository
            .GetAll()
            .Include(l => l.Copy)
                .ThenInclude(c => c.Book)
            .Include(l => l.User)
            .AsNoTracking()
            .ToListAsync();

        var response = _mapper.Map<IEnumerable<LoanResponseDTO>>(loans);

        return response;
    }

    public async Task<LoanResponseDTO> GetById(int id)
    {
        var loan = await _loanRepository
            .FindByAsNoTracking(l => l.Id == id)
            .Include(l => l.Copy)
                .ThenInclude(c => c.Book)
            .Include(l => l.User)
            .FirstOrDefaultAsync();

        var response = _mapper.Map<LoanResponseDTO>(loan);

        return response;
    }

    public async Task<IEnumerable<LoanResponseDTO>> GetLoansByUserId(int userId)
    {
        var loansByUser = await _loanRepository
            .FindByAsNoTracking(l => l.UserId == userId)
            .Include(l => l.Copy)
                .ThenInclude(c => c.Book)
            .Include(l => l.User)
            .ToListAsync();

        var response = _mapper.Map<IEnumerable<LoanResponseDTO>>(loansByUser);

        return response;
    }

    public async Task<LoanResponseDTO> Add(LoanRequestDTO request)
    {
        var loan = new Loan()
        {
            CopyId = request.CopyId,
            UserId = request.UserId,
            LoanDate = request.LoanDate,
            DueDate = request.DueDate,
            ReturnDate = request.ReturnDate,
            State = request.State
        };

        await _loanRepository.Add(loan);

        var loanWithDetails = await _loanRepository
            .FindBy(l => l.Id == loan.Id)
            .Include(l => l.Copy)
                .ThenInclude(c => c.Book)
            .Include(l => l.User)
            .FirstOrDefaultAsync();

        var response = _mapper.Map<LoanResponseDTO>(loanWithDetails);

        return response;
    }

    public async Task<LoanResponseDTO> Update(int id, LoanRequestDTO request)
    {
        var loan = await _loanRepository
            .FindBy(l => l.Id == id)
            .Include(l => l.Copy)
                .ThenInclude(c => c.Book)
            .Include(l => l.User)
            .FirstOrDefaultAsync();


        loan.CopyId = request.CopyId;
        loan.UserId = request.UserId;
        loan.LoanDate = request.LoanDate;
        loan.DueDate = request.DueDate;
        loan.ReturnDate = request.ReturnDate;
        loan.State = request.State;

        await _loanRepository.Update(loan);

        var loanWithDetails = await _loanRepository
            .FindBy(l => loan.Id == id)
            .Include(l => l.Copy)
            .ThenInclude(c => c.Book)
            .Include(l => l.User)
            .FirstOrDefaultAsync();

        var response = _mapper.Map<LoanResponseDTO>(loanWithDetails);

        return response;
    }

    public async Task<LoanResponseDTO> Delete(int id)
    {
        var loan = await _loanRepository
            .FindBy(l => l.Id == id)
            .Include(l => l.Copy)
                .ThenInclude(c => c.Book)
            .Include(l => l.User)
            .FirstOrDefaultAsync();

        await _loanRepository.Delete(loan);
        var response = _mapper.Map<LoanResponseDTO>(loan);

        return response;
    }
}
