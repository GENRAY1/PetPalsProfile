using MediatR;
using Profile.Application.Profile.Types;
using Profile.Domain.Abstractions;
using Profile.Domain.Profiles;

namespace Profile.Application.Profile.Create;

public class CreateProfileCommandHandler(
    IProfileRepository profileRepository, 
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateProfileCommand, CreateProfileDtoResponse>
{
    
    public async Task<CreateProfileDtoResponse> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
    {
        Guid id = Guid.NewGuid();

        string username = request.Username ?? GenerateUsername(id);
        
        var existingProfile = await profileRepository.GetByUsername(username, cancellationToken);

        if (existingProfile is not null)
            throw new Exception("Username already taken");

        if (request.Contacts is not null)
        {
            var allContactTypesUnique = AreContactTypesUnique(request.Contacts);

            if (!allContactTypesUnique)
                throw new Exception("The contact list contains duplication of types");
        }
        
        var contacts = request.Contacts?.Select(
            c => new ProfileContact
            {
                ProfileId = id,
                Link = c.Link,
                ContactType = c.ContactType
            }
        );
        
        var newProfile = new Domain.Profiles.Profile
        {
            Id = id,
            Username = username,
            AccountId = request.AccountId,
            DateOfBirth = request.DateOfBirth,
            Description = request.Description,
            MainPhoto = request.MainPhoto
        };

        if (contacts is not null)
        {
            newProfile.Contacts.AddRange(contacts);
        }
        
        profileRepository.Add(newProfile);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return new CreateProfileDtoResponse
        {
            ProfileId = id
        };
    }

    private static bool AreContactTypesUnique(List<ProfileContactType> contactTypes)
    {
        for (int i = 0; i < contactTypes.Count; i++)
        {
            for (int j = i + 1; j < contactTypes.Count; j++)
            {
                if (contactTypes[i].ContactType == contactTypes[j].ContactType)
                    return false;
            }
        }

        return true;
    }
    
    //TODO переделать 
    private static string GenerateUsername(Guid guid)
    {
        string prefix = "user_";
        
        string guidString = guid.ToString();
        
        return prefix + guidString.Substring(0, 12);
    }
}