using MyLibrary.WebApi.DTOs.Request;
using MyLibrary.WebApi.DTOs.Response;

namespace MyLibrary.WebApi.Services.Contracts;

public interface IPublisherService
{
    Task<IEnumerable<PublisherResponseDTO>> GetAll();
    Task<PublisherResponseDTO> GetById(int id);
    Task<PublisherResponseDTO> Add(PublisherRequestDTO request);
    Task<PublisherResponseDTO> Update(int id, PublisherRequestDTO request);
    Task<PublisherResponseDTO> Delete(int id);
}
