using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace PetPalsProfile.Api.Controllers.Auth;

[ApiController]
[Route("api/[action]")]
public class AuthController() : ControllerBase
{
    [HttpPost]
    public ActionResult Login()
    {
        return Ok();
    }
    
    [HttpPost]
    public async Task<ActionResult> Register(
        [FromBody] RegisterRequest request, 
        CancellationToken cancellationToken)
    {
        return Ok();
    }
    
    [HttpGet]
    public ActionResult Me()
    {
        return Ok();
    }
}