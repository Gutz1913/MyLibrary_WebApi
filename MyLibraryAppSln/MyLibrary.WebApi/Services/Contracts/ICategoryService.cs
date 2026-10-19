using MyLibrary.WebApi.DTOs.Request;
using MyLibrary.WebApi.DTOs.Response;

namespace MyLibrary.WebApi.Services.Contracts;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponseDTO>> GetAll();
    Task<CategoryResponseDTO> GetById(int id);
    Task<CategoryResponseDTO> Add(CategoryRequestDTO request);
    Task<CategoryResponseDTO> Update(int id, CategoryRequestDTO request);
    Task<CategoryResponseDTO> Delete(int id);
}
