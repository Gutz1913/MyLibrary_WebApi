using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MyLibrary.WebApi.DTOs.Request;
using MyLibrary.WebApi.DTOs.Response;
using MyLibrary.WebApi.Literals;
using MyLibrary.WebApi.Models;
using MyLibrary.WebApi.Repositories.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Transactions;

namespace MyLibrary.WebApi.Repositories.Implementations;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly MyLibraryAppDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    public UserRepository(MyLibraryAppDbContext context, UserManager<IdentityUser> userManager) : base(context)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IdentityResult> AddUser(RegisterRequestDTO request)
    {
        var response = new IdentityResult();
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var user = new ApplicationUser
            {
                UserName = request.UserName.ToLower(),
                Email = request.UserName
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            response = result;

            if (!result.Succeeded)
            {
                return response;
            }

            var newUser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                AspNetUserId = user.Id
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            scope.Complete();
        }

        return response;
    }
}
public interface IUserService
{
    Task<AuthResponseDTO> AddUserAsync(RegisterRequestDTO request);
    Task<AuthResponseDTO> Login(LoginRequestDTO request);
    Task<AuthResponseDTO> RefreshToken(string email);
    Task<User> GetUserInfo(string email);
}

public class UserService : IUserService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IUserRepository _userRepository;

    public UserService(UserManager<IdentityUser> userManager, IUserRepository userRepository)
    {
        _userManager = userManager;
        _userRepository = userRepository;
    }

    public async Task<AuthResponseDTO> AddUserAsync(RegisterRequestDTO request)
    {
        var response = new AuthResponseDTO();
        try
        {
            var result = await _userRepository.AddUser(request);

            if (result != null)
            {
                response.IsSuccess = result.Succeeded;
                response.Errors = result?.Errors?.Select(x => x.Description).ToArray() ?? new string[] { };
            }
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Errors = new string[] { ex.Message };
        }

        return response;
    }

    public async Task<AuthResponseDTO> Login(LoginRequestDTO request)
    {
        var response = new AuthResponseDTO();
        try
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                };

                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        Environment.GetEnvironmentVariable(MyLibraryLiterals.JWT_KEY)));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    //issuer: _configuration["JwtIssuer"],
                    //audience: _configuration["JwtAudience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(15), //Tiempo de vida del Token
                    signingCredentials: creds
                );

                var tokenResult = new JwtSecurityTokenHandler().WriteToken(token);
                response.Token = tokenResult;
                response.Expiration = token.ValidTo;
                response.IsSuccess = true;
            }
            else
            {
                response.IsSuccess = false;
            }
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Errors = new string[] { ex.Message };
        }

        return response;
    }

    public async Task<AuthResponseDTO> RefreshToken(string email)
    {
        var response = new AuthResponseDTO();
        try
        {
            var user = await _userManager.FindByNameAsync(email);

            if (user != null)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                };

                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        Environment.GetEnvironmentVariable(MyLibraryLiterals.JWT_KEY)));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    //issuer: _configuration["JwtIssuer"],
                    //audience: _configuration["JwtAudience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(15), // Tiempo de vida del Token
                    signingCredentials: creds
                );

                var tokenResult = new JwtSecurityTokenHandler().WriteToken(token);
                response.Token = tokenResult;
                response.Expiration = token.ValidTo;
                response.IsSuccess = true;
            }
            else
            {
                response.IsSuccess = false;
            }
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Errors = new string[] { ex.Message };
        }

        return response;
    }

    public async Task<User> GetUserInfo(string email)
    {
        var user = await _userManager.FindByNameAsync(email);
        var response = await _userRepository
            .FindByAsNoTracking(u => u.AspNetUserId == user.Id)
            .FirstOrDefaultAsync();

        return response;
    }
}

