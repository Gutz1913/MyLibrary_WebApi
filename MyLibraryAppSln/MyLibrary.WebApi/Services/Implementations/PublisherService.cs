using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyLibrary.WebApi.DTOs.Request;
using MyLibrary.WebApi.DTOs.Response;
using MyLibrary.WebApi.Models;
using MyLibrary.WebApi.Repositories.Contracts;
using MyLibrary.WebApi.Services.Contracts;

namespace MyLibrary.WebApi.Services.Implementations;

public class PublisherService : IPublisherService
{
    private readonly IBaseRepository<Publisher> _publisherRepository;
    private readonly IMapper _mapper;

    public PublisherService(IBaseRepository<Publisher> publisherRepository, IMapper mapper)
    {
        _publisherRepository = publisherRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PublisherResponseDTO>> GetAll()
    {
        var publishers = await _publisherRepository
            .GetAll()
            .AsNoTracking()
            .ToListAsync();

        var response = _mapper.Map<IEnumerable<PublisherResponseDTO>>(publishers);

        return response;
    }

    public async Task<PublisherResponseDTO> GetById(int id)
    {
        var publisher = await _publisherRepository
            .FindByAsNoTracking(p => p.Id == id)
            .FirstOrDefaultAsync();

        var response = _mapper.Map<PublisherResponseDTO>(publisher);

        return response;
    }

    public async Task<PublisherResponseDTO> Add(PublisherRequestDTO request)
    {
        var publisher = new Publisher()
        {
            Name = request.Name,
            Country = request.Country,
            WebSite = request.WebSite,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber
        };

        await _publisherRepository.Add(publisher);
        var response = _mapper.Map<PublisherResponseDTO>(publisher);

        return response;
    }

    public async Task<PublisherResponseDTO> Update(int id, PublisherRequestDTO request)
    {
        var publisher = await _publisherRepository
            .FindBy(p => p.Id == id)
            .FirstOrDefaultAsync();

        publisher.Name = request.Name;
        publisher.Country = request.Country;
        publisher.WebSite = request.WebSite;
        publisher.Email = request.Email;
        publisher.PhoneNumber = request.PhoneNumber;

        await _publisherRepository.Update(publisher);
        var response = _mapper.Map<PublisherResponseDTO>(publisher);

        return response;
    }

    public async Task<PublisherResponseDTO> Delete(int id)
    {
        var publisher = await _publisherRepository
            .FindBy(p => p.Id == id)
            .FirstOrDefaultAsync();

        await _publisherRepository.Delete(publisher);
        var response = _mapper.Map<PublisherResponseDTO>(publisher);

        return response;
    }
}
