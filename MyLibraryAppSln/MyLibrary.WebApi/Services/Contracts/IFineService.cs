using MyLibrary.WebApi.DTOs.Request;
using MyLibrary.WebApi.DTOs.Response;

namespace MyLibrary.WebApi.Services.Contracts;

public interface IFineService
{
    Task<IEnumerable<FineResponseDTO>> GetAll();
    Task<FineResponseDTO> GetById(int id);
    Task<IEnumerable<FineResponseDTO>> GetFinesByUserId(int userId);
    Task<IEnumerable<FineResponseDTO>> GetFinesByLoanId(int loanId);
    Task<FineResponseDTO> Add(FineRequestDTO request);
    Task<FineResponseDTO> Update(int id, FineRequestDTO request);
    Task<FineResponseDTO> Delete(int id);
}
