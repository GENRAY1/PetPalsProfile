using Profile.Domain.Abstractions;

namespace Profile.Domain.Subscription;

public class Subscription : Entity
{
    /// <summary>
    /// ID профиля, который подписывается
    /// </summary>
    public required Guid SubscriberId { get; init; }

    /// <summary>
    /// ID профиля, к которому подписываются
    /// </summary>
    public required Guid SubscribedToId { get; init; }

    public required DateTime SubscriptionDate { get; init; }

    public DateTime? UnsubscriptionDate { get; set; }
}
