using VKS.Credimatic.API.Models;

namespace VKS.Credimatic.API.Services;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
}
