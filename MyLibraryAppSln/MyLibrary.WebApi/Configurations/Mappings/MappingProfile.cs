using AutoMapper;
using MyLibrary.WebApi.DTOs.Request;
using MyLibrary.WebApi.DTOs.Response;
using MyLibrary.WebApi.Models;

namespace MyLibrary.WebApi.Configurations.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AuthorRequestDTO, Author>();
        CreateMap<Author, AuthorResponseDTO>();

        CreateMap<CategoryRequestDTO, Category>();
        CreateMap<Category, CategoryResponseDTO>();

        CreateMap<PublisherRequestDTO, Publisher>();
        CreateMap<Publisher, PublisherResponseDTO>();
    }
}
