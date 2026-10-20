using MyLibrary.WebApi.DTOs.Request;
using MyLibrary.WebApi.DTOs.Response;

namespace MyLibrary.WebApi.Services.Contracts;

public interface ICopyService
{
    Task<IEnumerable<CopyResponseDTO>> GetAll();
    Task<CopyResponseDTO> GetById(int id);
    Task<IEnumerable<CopyResponseDTO>> GetCopiesByBookId(int bookId);
    Task<CopyResponseDTO> Add(CopyRequestDTO request);
    Task<CopyResponseDTO> Update(int id, CopyRequestDTO request);
    Task<CopyResponseDTO> Delete(int id);
}
