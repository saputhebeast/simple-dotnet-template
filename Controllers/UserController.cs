using Microsoft.AspNetCore.Mvc;
using UrbanNest.Payloads;
using UrbanNest.Services;

namespace UrbanNest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserRequestDto userRequestDto)
    {
        var response = await _userService.CreateNewUser(userRequestDto);
        return Ok(response);
    }
}
