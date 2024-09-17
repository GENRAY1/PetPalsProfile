using PetPalsProfile.Domain.Absractions;

namespace PetPalsProfile.Domain.Accounts;

public class Role(Guid id) : Entity(id)
{
    public const int MaxNameLength = 32;
    public const int MinNameLength = 2;
    
    public const int MaxLocalizationLength = 32;
    public const int MinLocalizationLength = 2;
    
    public static Role Admin { get; } = Create(
        id:new Guid("cc3b8540-aaf6-4ea6-9685-0d752231b1bf"), 
        name:"Admin", 
        localization:"Администратор");

    public static Role User { get; } = Create(
        id:new Guid("8d4b5a5f-2f6a-4f6e-9a1b-5f4b5e5f6b5b"), 
        name:"User", 
        localization:"Пользователь");

    public static IReadOnlyCollection<Role> Roles { get; } = new[] { Admin, User };
    public string Name { get; init; }
    public string Localization { get; init; }
    
    public static Role Create(Guid id, string name, string localization)
    {
        return new Role(id)
        {
            Name = name,
            Localization = localization 
        };
    }
}