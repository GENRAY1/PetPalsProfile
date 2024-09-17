using MediatR;
using Profile.Application.Common;
using Profile.Domain.Profiles;

namespace Profile.Application.Profile.Search;

public class SearchProfilesQueryHandler(
    IProfileRepository profileRepository) 
    : IRequestHandler<SearchProfilesQuery, ListDtoResponse<SearchProfileDtoResponse>>
{
    public async Task<ListDtoResponse<SearchProfileDtoResponse>> Handle(SearchProfilesQuery request, CancellationToken cancellationToken)
    {
        var profiles = await profileRepository
            .Find(request.Pagination.Skip, request.Pagination.Take, cancellationToken);
        
        int totalCount = await profileRepository.Count(cancellationToken);
        
        return new ListDtoResponse<SearchProfileDtoResponse>
        {
            List = profiles
                .Select(p => new SearchProfileDtoResponse
                {
                    Id = p.Id,
                    Username = p.Username,
                    AccountId = p.AccountId,
                    MainPhoto = p.MainPhoto,
                    Description = p.Description,
                    DateOfBirth = p.DateOfBirth
                })
                .ToList(),
            TotalCount = totalCount
        };
    }
}