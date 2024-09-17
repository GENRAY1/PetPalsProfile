namespace PetPalsProfile.Infrastructure.Authentication;

public class JwtOptions
{
    public AccessTokenSettings AccessTokenSettings { get; set; }
    public RefreshTokenSettings RefreshTokenSettings { get; set; }
}

public class AccessTokenSettings
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public long LifeTimeInMinutes { get; set; }
    
    public string PublicKey { get; set; }
    public string PrivateKey { get; set; }
}

public class RefreshTokenSettings
{
    public int Length { get; set; }
    public int LifeTimeInMinutes { get; set; }
}