using MyLibrary.WebApi.DTOs.Request;
using MyLibrary.WebApi.DTOs.Response;

namespace MyLibrary.WebApi.Services.Contracts;

public interface IAuthorService
{
    Task<IEnumerable<AuthorResponseDTO>> GetAll();
    Task<AuthorResponseDTO> GetById(int id);
    Task<AuthorResponseDTO> Add(AuthorRequestDTO request);
    Task<AuthorResponseDTO> Update(int id, AuthorRequestDTO request);
    Task<AuthorResponseDTO> Delete(int id);
}
