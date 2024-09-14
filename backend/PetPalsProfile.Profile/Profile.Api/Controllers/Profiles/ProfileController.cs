using MediatR;
using Microsoft.AspNetCore.Mvc;
using Profile.Application.Common;
using Profile.Application.Profile.Change;
using Profile.Application.Profile.Create;
using Profile.Application.Profile.Get;
using Profile.Application.Profile.Search;

namespace Profile.Api.Controllers.Profiles;

[ApiController]
[Route("profiles")]
public class ProfileController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CreateProfileDtoResponse>> CreateProfile(
        [FromBody] CreateProfileRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateProfileCommand
        {
            AccountId = request.AccountId,
            Username = request.Username,
            MainPhoto = request.MainPhoto,
            Description = request.Description,
            DateOfBirth = request.DateOfBirth,
            Contacts = request.Contacts
        };

        CreateProfileDtoResponse response = await sender.Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetProfileDtoResponse>> GetProfileById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        GetProfileDtoResponse response = await sender.Send(
            new GetProfileQuery { ProfileId = id },
            cancellationToken);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<ListDtoResponse<SearchProfileDtoResponse>>> GetProfiles(
        [FromQuery] SearchProfileRequest request,
        CancellationToken cancellationToken)
    {
        ListDtoResponse<SearchProfileDtoResponse> response = await sender.Send(
            new SearchProfilesQuery { Pagination = request.Pagination },
            cancellationToken);

        return Ok(response);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> ChangeProfile(
        [FromRoute] Guid id,
        [FromBody] ChangeProfileRequest request,
        CancellationToken cancellationToken)
    {
        var command = new ChangeProfileCommand
        {
            Id = id,
            Username = request.Username,
            MainPhoto = request.MainPhoto,
            Description = request.Description,
            DateOfBirth = request.DateOfBirth,
            Contacts = request.Contacts
        };
        
        await sender.Send(command, cancellationToken);

        return Ok();
    }
}