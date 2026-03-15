namespace VKS.Credimatic.API.Services;

public interface IJwtService
{
    string GenerateToken(string usuario, string empresa);
}
