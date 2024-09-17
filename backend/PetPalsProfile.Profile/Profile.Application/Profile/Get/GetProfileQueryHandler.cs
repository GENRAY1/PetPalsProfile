using MediatR;
using Profile.Application.Profile.Types;
using Profile.Domain.Profiles;

namespace Profile.Application.Profile.Get;

public class GetProfileQueryHandler(
    IProfileRepository profileRepository
    ) : IRequestHandler<GetProfileQuery, GetProfileDtoResponse>
{
    public async Task<GetProfileDtoResponse> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var profile = await profileRepository.GetById(request.ProfileId, cancellationToken);

        if (profile is null)
            throw new Exception("Profile not found");

        var contacts = profile.Contacts
            .Select(p => new ProfileContactType
            {
                ContactType = p.ContactType,
                Link = p.Link
            });

        return new GetProfileDtoResponse
        {
            Id = profile.Id,
            AccountId = profile.AccountId,
            DateOfBirth = profile.DateOfBirth,
            Description = profile.Description,
            MainPhoto = profile.MainPhoto,
            Username = profile.Username,
            Contacts = contacts.ToList(),
        };
    }
}