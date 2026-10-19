using Microsoft.AspNetCore.Identity;
using MyLibrary.WebApi.DTOs.Request;
using MyLibrary.WebApi.Models;

namespace MyLibrary.WebApi.Repositories.Contracts;

public interface IUserRepository : IBaseRepository<User>
{
    Task<IdentityResult> AddUser(RegisterRequestDTO request);
}

