using UrbanNest.Payloads;

namespace UrbanNest.Services;

public interface IUserService
{
    Task<ServiceResponse<UserWithTokenResponseDto>> CreateNewUser(UserRequestDto userRequestDto);
}