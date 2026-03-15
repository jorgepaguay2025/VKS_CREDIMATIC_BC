namespace VKS.Credimatic.API.Configuration;

/// <summary>
/// Configuración JWT. Clave secreta y tiempo de expiración parametrizados.
/// </summary>
public class JwtSettings
{
    public const string SectionName = "Jwt";

    public string SecretKey { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int ExpirationHours { get; set; } = 8;
}
