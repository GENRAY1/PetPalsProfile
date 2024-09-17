using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetPalsProfile.Application.Account.GetLogged;
using PetPalsProfile.Application.Account.Login;
using PetPalsProfile.Application.Account.Logout;
using PetPalsProfile.Application.Account.RefreshToken;
using PetPalsProfile.Application.Account.Register;

namespace PetPalsProfile.Api.Controllers.AccountController;

[ApiController]
[Route("identity/[action]")]
public class AccountController(ISender sender) : ControllerBase
{
    [AllowAnonymous]
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

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> RefreshToken(
        [FromBody] RefreshTokenRequest request,
        CancellationToken cancellationToken)
    {
        var response = await sender.Send(new RefreshTokenCommand
        {
            AccessToken = request.AccessToken,
            RefreshToken = request.RefreshToken
        }, cancellationToken);

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> Register(
        [FromBody] RegisterAccountRequest request,
        CancellationToken cancellationToken)
    {
        await sender.Send(new RegisterAccountCommand
        {
            Email = request.Email,
            Password = request.Password,
            Phone = request.Phone
        }, cancellationToken);

        return Ok();
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> Logout(CancellationToken cancellationToken)
    {
        var accountId = HttpContext
            .User
            .FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (accountId is null)
            return BadRequest("Не удалось получить ID пользователя");
        
        await sender.Send(new LogoutAccountCommand
        {
            AccountId = Guid.Parse(accountId)
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

        if (accountId is null)
            return BadRequest("Не удалось получить ID пользователя");

        var loggedAccountResponse = await sender.Send(new GetLoggedAccountQuery()
        {
            AccountId = Guid.Parse(accountId)
        }, cancellationToken);

        return Ok(loggedAccountResponse);
    }
}