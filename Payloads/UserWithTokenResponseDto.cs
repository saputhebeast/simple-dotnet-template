namespace UrbanNest.Payloads;

public class UserWithTokenResponseDto
{
    public UserResponseDto User { get; set; }
    public string Token { get; set; }
}