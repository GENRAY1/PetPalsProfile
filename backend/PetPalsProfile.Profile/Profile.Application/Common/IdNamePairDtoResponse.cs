namespace Profile.Application.Common;

public class IdNamePairDtoResponse
{
    public required Guid Id { get; init; }

    public required string Name { get; init; }
}