using UrbanNest.Types;

namespace UrbanNest.Payloads;

public class UserResponseDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
}