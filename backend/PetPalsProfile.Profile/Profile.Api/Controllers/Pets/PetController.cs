using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profile.Api.Common;
using Profile.Application.Common;
using Profile.Application.Pets.Create;
using Profile.Application.Pets.Get;
using Profile.Application.Pets.PetTypes.Create;
using Profile.Application.Pets.PetTypes.Delete;
using Profile.Application.Pets.PetTypes.GetAll;
using Profile.Application.Pets.Search;
using Profile.Application.Pets.Update;
using Profile.Infrastructure.Authorization;

namespace Profile.Api.Controllers.Pets;

[ApiController]
[Route("pets")]
public class PetController : ControllerBase
{
    private readonly ISender _sender;

    public PetController(ISender sender)
    {
        _sender = sender;
    }

    [RoleAuthorize(Roles.User)]
    [HttpPost]
    public async Task<ActionResult<CreatePetDtoResponse>> CreatePet(
        [FromBody] CreatePetRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreatePetCommand
        {
            ProfileId = request.ProfileId,
            MainPhoto = request.MainPhoto,
            Description = request.Description,
            DateOfBirth = request.DateOfBirth,
            Name = request.Name,
            TypeId = request.TypeId,
            Breed = request.Breed,
            Gender = request.Gender
        };

        CreatePetDtoResponse response = await _sender.Send(command, cancellationToken);

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<GetPetDtoResponse>> GetPetById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        GetPetDtoResponse response = await _sender.Send(
            new GetPetQuery { PetId = id },
            cancellationToken);

        return Ok(response);
    }
    
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<ListDtoResponse<SearchPetDtoResponse>>> GetPets(
        [FromQuery] SearchPetsRequest request,
        CancellationToken cancellationToken)
    {
        ListDtoResponse<SearchPetDtoResponse> response = await _sender.Send(
            new SearchPetsQuery
            {
                Pagination = request.Pagination,
                ProfileId = request.ProfileId
            },
            cancellationToken);

        return Ok(response);
    }

    [RoleAuthorize(Roles.User)]
    [HttpPatch("{id}")]
    public async Task<ActionResult> ChangePet(
        [FromRoute] Guid id,
        [FromBody] ChangePetRequest request,
        CancellationToken cancellationToken)
    {
        var command = new ChangePetCommand
        {
            Id = id,
            Name = request.Name,
            MainPhoto = request.MainPhoto,
            Description = request.Description,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            TypeId = request.TypeId,
            Breed = request.Breed
        };

        await _sender.Send(command, cancellationToken);

        return NoContent();
    }
    
    [RoleAuthorize(Roles.Admin)]
    [HttpPost("types")]
    public async Task<ActionResult<CreatePetTypeDtoResponse>> CreatePetType(
        [FromBody] CreatePetTypeRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreatePetTypeCommand
        {
            Name = request.Name
        };

        CreatePetTypeDtoResponse response = await _sender.Send(command, cancellationToken);

        return Ok(response);
    }
    
    [AllowAnonymous]
    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyCollection<IdNamePairDtoResponse>>> GetPetTypes(
        CancellationToken cancellationToken)
    {
        IReadOnlyCollection<IdNamePairDtoResponse> response = await _sender.Send(
            new GetPetTypesQuery(),
            cancellationToken);

        return Ok(response);
    }

    [RoleAuthorize(Roles.User)]
    [HttpDelete("types/{id}")]
    public async Task<ActionResult> DeletePetType(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeletePetTypeCommand
        {
            PetTypeId = id
        };

        await _sender.Send(command, cancellationToken);

        return NoContent();
    }
}