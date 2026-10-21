using MyLibrary.WebApi.DTOs.Request;
using MyLibrary.WebApi.DTOs.Response;

namespace MyLibrary.WebApi.Services.Contracts;

public interface IReservationService
{
    Task<IEnumerable<ReservationResponseDTO>> GetAll();
    Task<ReservationResponseDTO> GetById(int id);
    Task<IEnumerable<ReservationResponseDTO>> GetReservationsByBookId(int bookId);
    Task<IEnumerable<ReservationResponseDTO>> GetReservationsByUserId(int userId);
    Task<ReservationResponseDTO> Add(ReservationRequestDTO request);
    Task<ReservationResponseDTO> Update(int id, ReservationRequestDTO request);
    Task<ReservationResponseDTO> Delete(int id);
}
