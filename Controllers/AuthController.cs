using Microsoft.AspNetCore.Mvc;
using VKS.Credimatic.API.Models;
using VKS.Credimatic.API.Services;

namespace VKS.Credimatic.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService) => _authService = authService;

    /// <summary>
    /// Login con usuario y clave. Devuelve JWT para usar en header Authorization.
    /// </summary>
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request, CancellationToken ct)
    {
        var result = await _authService.LoginAsync(request, ct);
        if (!result.Success)
            return Ok(result);
        return Ok(result);
    }
}
