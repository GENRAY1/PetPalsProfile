﻿namespace Profile.Infrastructure.Authentication;

public class JwtOptions
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string PublicKey { get; set; }
}