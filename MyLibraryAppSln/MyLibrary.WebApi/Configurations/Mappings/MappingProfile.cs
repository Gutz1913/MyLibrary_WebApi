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

        CreateMap<BookRequestDTO, Book>();
        CreateMap<Book, BookResponseDTO>()
            .ForMember(
                dest => dest.CopiesCount, 
                opt => opt.MapFrom(
                    src => src != null ? 
                    src.Copies.Count : 0))
            .ForMember(
                dest => dest.ReservationsCount, 
                opt => opt.MapFrom(
                    src => src != null ?
                    src.Reservations.Count : 0))
            .ForMember(
                dest => dest.PublisherName,
                opt => opt.MapFrom(
                    src => src.Publisher.Name))
            .ForMember(
                dest => dest.AuthorId, 
                opt => opt.MapFrom(
                    src => src.BookAuthors
                    .FirstOrDefault() != null ? src.BookAuthors.FirstOrDefault().AuthorId : 0))
            .ForMember(
                dest => dest.AuthorName, 
                opt => opt.MapFrom(
                    src => src.BookAuthors.FirstOrDefault() != null && src.BookAuthors.FirstOrDefault().Author != null ? src.BookAuthors.FirstOrDefault().Author.Name : string.Empty))
            .ForMember(
                dest => dest.CategoryId,
                opt => opt.MapFrom(
                    src => src.BookCategories.FirstOrDefault() != null ? src.BookCategories.FirstOrDefault().CategoryId : 0))
            .ForMember(
                dest => dest.CategoryName,
                opt => opt.MapFrom(
                    src => src.BookCategories.FirstOrDefault() != null && src.BookCategories.FirstOrDefault().Category != null ? src.BookCategories.FirstOrDefault().Category.Name : string.Empty));

        CreateMap<BookAuthorRequestDTO, BookAuthor>();
        CreateMap<BookAuthor, BookAuthorResponseDTO>()
            .ForMember(
                dest => dest.AuthorId,
                opt => opt.MapFrom(
                    src => src.Author.Id))
            .ForMember(
                dest => dest.AuthorName,
                opt => opt.MapFrom(
                    src => src.Author.Name))
            .ForMember(
                dest => dest.BookId, 
                opt => opt.MapFrom(
                    src => src.Book.Id))
            .ForMember(
                dest => dest.BookTitle, 
                opt => opt.MapFrom(
                    src => src.Book.Title));

        CreateMap<BookCategoryRequestDTO, BookCategory>();
        CreateMap<BookCategory, BookCategoryResponseDTO>()
            .ForMember(
                dest => dest.CategoryId,
                opt => opt.MapFrom(
                    src => src.Category.Id))
            .ForMember(
                dest => dest.CategoryName,
                opt => opt.MapFrom(
                    src => src.Category.Name))
            .ForMember(
                dest => dest.BookId,
                opt => opt.MapFrom(
                    src => src.Book.Id))
            .ForMember(
                dest => dest.BookTitle,
                opt => opt.MapFrom(
                    src => src.Book.Title));

        CreateMap<CopyRequestDTO, Copy>();
        CreateMap<Copy, CopyResponseDTO>()
            .ForMember(
                dest => dest.BookId, 
                opt => opt.MapFrom(
                    src => src.Book.Id))
            .ForMember(
                dest => dest.BookTitle, 
                opt => opt.MapFrom(
                    src => src.Book.Title));

        CreateMap<ReservationRequestDTO, Reservation>();
        CreateMap<Reservation, ReservationResponseDTO>()
            .ForMember(
                dest => dest.BookId, 
                opt => opt.MapFrom(
                    src => src.Book.Id))
            .ForMember(
                dest => dest.BookTitle, 
                opt => opt.MapFrom(
                    src => src.Book.Title))
            .ForMember(
                dest => dest.UserId, 
                opt => opt.MapFrom(
                    src => src.User.Id))
            .ForMember(
                dest => dest.UserFullName, 
                opt => opt.MapFrom(
                    src => src.User.FirstName + " "+ src.User.LastName));
    }
}
