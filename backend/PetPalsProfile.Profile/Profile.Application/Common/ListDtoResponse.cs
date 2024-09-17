namespace Profile.Application.Common;

public class ListDtoResponse<T>
{
    public List<T> List { get; init; }
    
    public int TotalCount { get; init; }
}