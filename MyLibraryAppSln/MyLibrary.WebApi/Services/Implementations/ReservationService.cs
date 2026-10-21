using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyLibrary.WebApi.DTOs.Request;
using MyLibrary.WebApi.DTOs.Response;
using MyLibrary.WebApi.Models;
using MyLibrary.WebApi.Repositories.Contracts;
using MyLibrary.WebApi.Services.Contracts;

namespace MyLibrary.WebApi.Services.Implementations;

public class ReservationService : IReservationService
{
    private readonly IBaseRepository<Reservation> _reservationRepository;
    private readonly IMapper _mapper;

    public ReservationService(IBaseRepository<Reservation> reservationRepository, IBaseRepository<Book> bookRepository, IMapper mapper)
    {
        _reservationRepository = reservationRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ReservationResponseDTO>> GetAll()
    {
        var reservations = await _reservationRepository
            .GetAll()
            .Include(r => r.Book)
            .Include(r => r.User)
            .AsNoTracking()
            .ToListAsync();

        var response = _mapper.Map<IEnumerable<ReservationResponseDTO>>(reservations);

        return response;
    }

    public async Task<ReservationResponseDTO> GetById(int id)
    {
        var reservation = await _reservationRepository
            .FindByAsNoTracking(r => r.Id == id)
            .Include(r => r.Book)
            .Include(r => r.User)
            .FirstOrDefaultAsync();

        var response = _mapper.Map<ReservationResponseDTO>(reservation);

        return response;
    }

    public async Task<IEnumerable<ReservationResponseDTO>> GetReservationsByBookId(int bookId)
    {
        var reservationsByBook = await _reservationRepository
            .FindByAsNoTracking(r => r.BookId == bookId)
            .Include(r => r.Book)
            .Include(r => r.User)
            .ToListAsync();

        var response = _mapper.Map<IEnumerable<ReservationResponseDTO>>(reservationsByBook);

        return response;
    }

    public async Task<IEnumerable<ReservationResponseDTO>> GetReservationsByUserId(int userId)
    {
        var reservationsByUser = await _reservationRepository
            .FindByAsNoTracking(r => r.UserId == userId)
            .Include(r => r.Book)
            .Include(r => r.User)
            .ToListAsync();

        var response = _mapper.Map<IEnumerable<ReservationResponseDTO>>(reservationsByUser);

        return response;
    }

    public async Task<ReservationResponseDTO> Add(ReservationRequestDTO request)
    {
        var reservation = new Reservation()
        {
            BookId = request.BookId,
            UserId = request.UserId,
            ReservationDate = request.ReservationDate,
            ExpirationDate = request.ExpirationDate,
            State = request.State,
            IdAssignedCopy = request.IdAssignedCopy
        };

        await _reservationRepository.Add(reservation);
        var resrevationWithDetails = await _reservationRepository
            .FindBy(r => r.Id == reservation.Id)
            .Include(r => r.Book)
            .Include(r => r.User)
            .FirstOrDefaultAsync();

        var response = _mapper.Map<ReservationResponseDTO>(resrevationWithDetails);

        return response;
    }

    public async Task<ReservationResponseDTO> Update(int id, ReservationRequestDTO request)
    {
        var reservation = await _reservationRepository
            .FindBy(r => r.Id == id)
            .Include(r => r.Book)
            .Include(r => r.User)
            .FirstOrDefaultAsync();

        reservation.BookId = request.BookId;
        reservation.UserId = request.UserId;
        reservation.ReservationDate = request.ReservationDate;
        reservation.ExpirationDate = request.ExpirationDate;
        reservation.State = request.State;
        reservation.IdAssignedCopy = request.IdAssignedCopy;

        await _reservationRepository.Update(reservation);
        var resrevationWithDetails = await _reservationRepository
            .FindBy(r => r.Id == reservation.Id)
            .Include(r => r.Book)
            .Include(r => r.User)
            .FirstOrDefaultAsync();

        var response = _mapper.Map<ReservationResponseDTO>(reservation);

        return response;
    }

    public async Task<ReservationResponseDTO> Delete(int id)
    {
        var reservation = await _reservationRepository
            .FindBy(r => r.Id == id)
            .Include(r => r.Book)
            .Include(r => r.User)
            .FirstOrDefaultAsync();

        await _reservationRepository.Delete(reservation);
        var response = _mapper.Map<ReservationResponseDTO>(reservation);

        return response;
    }
}
