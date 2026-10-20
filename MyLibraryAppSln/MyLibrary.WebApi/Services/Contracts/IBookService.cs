using MyLibrary.WebApi.DTOs.Request;
using MyLibrary.WebApi.DTOs.Response;

namespace MyLibrary.WebApi.Services.Contracts;

public interface IBookService
{
    Task<IEnumerable<BookResponseDTO>> GetAll();
    Task<BookResponseDTO> GetById(int id);
    Task<IEnumerable<BookCategoryResponseDTO>> GetBooksByCategoryId(int categoryId);
    Task<IEnumerable<BookAuthorResponseDTO>> GetBooksByAuthorId(int authorId);
    Task<BookResponseDTO> Add(BookRequestDTO request);
    Task<BookResponseDTO> Update(int id, BookRequestDTO request);
    Task<BookResponseDTO> Delete(int id);
    Task RegisterCategoryAsync(BookCategoryRequestDTO request);
    Task RegisterAuthorAsync(BookAuthorRequestDTO request);
}
