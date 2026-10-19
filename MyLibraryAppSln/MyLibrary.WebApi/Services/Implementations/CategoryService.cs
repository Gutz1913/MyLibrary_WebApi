using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyLibrary.WebApi.DTOs.Request;
using MyLibrary.WebApi.DTOs.Response;
using MyLibrary.WebApi.Models;
using MyLibrary.WebApi.Repositories.Contracts;
using MyLibrary.WebApi.Services.Contracts;

namespace MyLibrary.WebApi.Services.Implementations;

public class CategoryService : ICategoryService
{
    private readonly IBaseRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(IBaseRepository<Category> categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryResponseDTO>> GetAll()
    {
        var categories = await _categoryRepository
            .GetAll()
            .AsNoTracking()
            .ToListAsync();

        var response = _mapper.Map<IEnumerable<CategoryResponseDTO>>(categories);

        return response;
    }

    public async Task<CategoryResponseDTO> GetById(int id)
    {
        var category = await _categoryRepository
            .FindByAsNoTracking(c => c.Id == id)
            .FirstOrDefaultAsync();

        var response = _mapper.Map<CategoryResponseDTO>(category);

        return response;
    }

    public async Task<CategoryResponseDTO> Add(CategoryRequestDTO request)
    {
        var category = new Category()
        {
            Name = request.Name,
            Description = request.Description
        };

        await _categoryRepository.Add(category);
        var response = _mapper.Map<CategoryResponseDTO>(category);

        return response;
    }

    public async Task<CategoryResponseDTO> Update(int id, CategoryRequestDTO request)
    {
        var category = await _categoryRepository
            .FindBy(c => c.Id == id)
            .FirstOrDefaultAsync();

        category.Name = request.Name;
        category.Description = request.Description;
       
        await _categoryRepository.Update(category);
        var response = _mapper.Map<CategoryResponseDTO>(category);

        return response;
    }

    public async Task<CategoryResponseDTO> Delete(int id)
    {
        var category = await _categoryRepository
            .FindBy(c => c.Id == id)
            .FirstOrDefaultAsync();

        await _categoryRepository.Delete(category);
        var response = _mapper.Map<CategoryResponseDTO>(category);

        return response;
    }
}
