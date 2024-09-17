using MediatR;
using Profile.Domain.Abstractions;
using Profile.Domain.Profiles;

namespace Profile.Application.Profile.Change;

public class ChangeProfileQueryHandler(
    IProfileRepository profileRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<ChangeProfileCommand>
{
    public async Task Handle(ChangeProfileCommand request, CancellationToken cancellationToken)
    {
        var profile = await profileRepository.GetById(request.Id, cancellationToken);

        if (profile is null) 
            throw new Exception("Profile not found");
        
        if(request.Username is not null)
            profile.Username = request.Username;

        if(request.MainPhoto is not null)
            profile.MainPhoto = request.MainPhoto;
        
        if(request.Description is not null)
            profile.Description = request.Description;
        
        if(request.DateOfBirth is not null)
            profile.DateOfBirth = request.DateOfBirth;

        if (request.Contacts is not null && request.Contacts.Count > 0)
        {
            foreach (var contact in request.Contacts)
            {
                var existingContact = profile.Contacts.FirstOrDefault(c => c.ContactType == contact.ContactType);

                if (existingContact is not null)
                {
                    existingContact.Link = contact.Link;
                }
                else
                {
                    var newContact = new ProfileContact
                    {
                        ContactType = contact.ContactType,
                        Link = contact.Link,
                        ProfileId = profile.Id
                    };
                    
                    profile.Contacts.Add(newContact);
                }
            }
        }
        
        profileRepository.UpdateAsync(profile);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
    
}