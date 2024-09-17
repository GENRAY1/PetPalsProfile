using MediatR;

namespace Profile.Application.Profile.Get;

public class GetProfileQuery : IRequest<GetProfileDtoResponse>
{
    public required Guid ProfileId { get; init; }
}