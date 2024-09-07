using AutoMapper;
using UrbanNest.Models;
using UrbanNest.Payloads;
using UrbanNest.Repositories;
using UrbanNest.Utils;

namespace UrbanNest.Services.Impl;

public class UserService : IUserService
{
    private readonly IMapper _mapper;

    private readonly UserRepository _userRepository;

    private readonly IAuthService _authService;

    public UserService(IMapper mapper, UserRepository userRepository, IAuthService authService)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _authService = authService;
    }

    public async Task<ServiceResponse<UserWithTokenResponseDto>> CreateNewUser(UserRequestDto userRequestDto)
    {
        var response = new ServiceResponse<UserWithTokenResponseDto>();

        try
        {
            var newUser = _mapper.Map<User>(userRequestDto);
            PasswordUtil.CreatePasswordHash(userRequestDto.Password, out var passwordHash, out var passwordSalt);
            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;

            await _userRepository.save(newUser);

            var token = _authService.GenerateJwtToken(newUser);
            
            var userResponse = _mapper.Map<UserResponseDto>(newUser);

            response.Data = new UserWithTokenResponseDto { User = userResponse, Token = token };            
            response.Message = "User created successfully!";
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = $"Failed to create user: {ex.Message}";
        }

        return response;
    }
}
