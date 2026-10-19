using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyLibrary.WebApi.DTOs.Request;
using MyLibrary.WebApi.DTOs.Response;
using MyLibrary.WebApi.Models;
using MyLibrary.WebApi.Repositories.Contracts;
using MyLibrary.WebApi.Services.Contracts;

namespace MyLibrary.WebApi.Services.Implementations;

public class AuthorService : IAuthorService
{
    private readonly IBaseRepository<Author> _authorRepository;
    private readonly IMapper _mapper;

    public AuthorService(IBaseRepository<Author> authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AuthorResponseDTO>> GetAll()
    {
        var authors = await _authorRepository
            .GetAll()
            .AsNoTracking()
            .ToListAsync();

        var response = _mapper.Map<IEnumerable<AuthorResponseDTO>>(authors);

        return response;
    }

    public async Task<AuthorResponseDTO> GetById(int id)
    {
        var author = await _authorRepository
            .FindByAsNoTracking(a => a.Id == id)
            .FirstOrDefaultAsync();

        var response = _mapper.Map<AuthorResponseDTO>(author);

        return response;
    }

    public async Task<AuthorResponseDTO> Add(AuthorRequestDTO request)
    {
        var author = new Author()
        {
            Name = request.Name,
            Nationality = request.Nationality,
            BirthDate = request.BirthDate,
            Biography = request.Biography
        };

        await _authorRepository.Add(author);
        var response = _mapper.Map<AuthorResponseDTO>(author);

        return response;
    }

    public async Task<AuthorResponseDTO> Update(int id, AuthorRequestDTO request)
    {
        var author = await _authorRepository
            .FindBy(a => a.Id == id)
            .FirstOrDefaultAsync();

        author.Name = request.Name;
        author.Nationality = request.Nationality;
        author.BirthDate = request.BirthDate;
        author.Biography = request.Biography;

        await _authorRepository.Update(author);
        var response = _mapper.Map<AuthorResponseDTO>(author);

        return response;
    }

    public async Task<AuthorResponseDTO> Delete(int id)
    {
        var author = await _authorRepository
            .FindBy(a => a.Id == id)
            .FirstOrDefaultAsync();

        await _authorRepository.Delete(author);
        var response = _mapper.Map<AuthorResponseDTO>(author);

        return response;
    }
}
