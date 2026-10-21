using MyLibrary.WebApi.DTOs.Request;
using MyLibrary.WebApi.DTOs.Response;

namespace MyLibrary.WebApi.Services.Contracts;

public interface ILoanService
{
    Task<IEnumerable<LoanResponseDTO>> GetAll();
    Task<LoanResponseDTO> GetById(int id);
    Task<IEnumerable<LoanResponseDTO>> GetLoansByUserId(int userId);
    Task<LoanResponseDTO> Add(LoanRequestDTO request);
    Task<LoanResponseDTO> Update(int id, LoanRequestDTO request);
    Task<LoanResponseDTO> Delete(int id);
}
