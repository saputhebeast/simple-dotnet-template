using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UrbanNest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    // Only SuperAdmin can access this endpoint
    [Authorize(Roles = "SUPER_ADMIN")]
    [HttpGet("superadmin")]
    public IActionResult SuperAdminOnly()
    {
        return Ok("Only SuperAdmin can access this.");
    }

    // Only Seller can access this endpoint
    [Authorize(Roles = "SELLER")]
    [HttpGet("seller")]
    public IActionResult SellerOnly()
    {
        return Ok("Only Sellers can access this.");
    }

    // Only Buyer can access this endpoint
    [Authorize(Roles = "BUYER")]
    [HttpGet("buyer")]
    public IActionResult BuyerOnly()
    {
        return Ok("Only Buyers can access this.");
    }

    // Both Seller and Buyer can access this endpoint
    [Authorize(Roles = "SELLER,BUYER")]
    [HttpGet("seller-buyer")]
    public IActionResult SellerAndBuyerAccess()
    {
        return Ok("Both Sellers and Buyers can access this.");
    }

    // Any authenticated user can access this endpoint
    [Authorize]
    [HttpGet("common")]
    public IActionResult CommonAccess()
    {
        return Ok("Any authenticated user can access this.");
    }
    
    // Any one can access this endpoint
    [HttpGet("open")]
    public IActionResult OpenAccess()
    {
        return Ok("Any one can access this.");
    }
}
