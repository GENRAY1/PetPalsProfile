using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetPalsProfile.Api.Controllers.AccountController.LoginAccount;
using PetPalsProfile.Api.Controllers.AccountController.RegisterAccount;
using PetPalsProfile.Application.Account.GetLogged;
using PetPalsProfile.Application.Account.Login;
using PetPalsProfile.Application.Account.Register;

namespace PetPalsProfile.Api.Controllers.AccountController;

[ApiController]
[Route("api/[action]")]
public class AccountController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<LoginAccountResponse>> Login(
        [FromBody] LoginAccountRequest request,
        CancellationToken cancellationToken)
    {
        var loginResponse = await sender.Send(new LoginAccountCommand
        {
            Email = request.Email,
            Password = request.Password,
        }, cancellationToken);
        
        return Ok(loginResponse);
    }

    [HttpPost]
    public async Task<ActionResult> Register(
        [FromBody] RegisterAccountRequest request,
        CancellationToken cancellationToken)
    {
        await sender.Send(new RegisterAccountCommand
        {
            Email = request.Email,
            Password = request.Password,
            Username = request.UserName
        }, cancellationToken);

        return Ok();
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<GetLoggedAccountResponse>> GetLoggedAccount(CancellationToken cancellationToken)
    {
        var accountId = HttpContext
            .User
            .FindFirstValue(ClaimTypes.NameIdentifier);
        
        if(accountId is null)
            return BadRequest("Не удалось получить ID пользователя");
        
        var loggedAccountResponse = await sender.Send(new GetLoggedAccountQuery()
        {
            AccountId = Guid.Parse(accountId)
        }, cancellationToken);
        
        return Ok(loggedAccountResponse);
    }
}