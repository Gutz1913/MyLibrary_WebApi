using MyLibrary.WebApi.DTOs.Request;
using MyLibrary.WebApi.DTOs.Response;
using MyLibrary.WebApi.Models;

namespace MyLibrary.WebApi.Services.Contracts;

public interface IUserService
{
    Task<AuthResponseDTO> AddUserAsync(RegisterRequestDTO request);
    Task<AuthResponseDTO> Login(LoginRequestDTO request);
    Task<AuthResponseDTO> RefreshToken(string email);
    Task<User> GetUserInfo(string email);
}