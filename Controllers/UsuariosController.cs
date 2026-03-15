using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VKS.Credimatic.API.Models;
using VKS.Credimatic.API.Services;

namespace VKS.Credimatic.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsuariosController : ControllerBase
{
    private readonly IUsuariosService _service;

    public UsuariosController(IUsuariosService service) => _service = service;

    /// <summary>
    /// Consulta el usuario por Empresa y Usuario (ej: usuario logoneado). Requiere JWT.
    /// </summary>
    [HttpGet("{empresa}/{usuario}")]
    [ProducesResponseType(typeof(UsuarioConsultaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UsuarioConsultaResponse>> GetByEmpresaAndUsuario(string empresa, string usuario, CancellationToken ct)
    {
        var result = await _service.GetByEmpresaAndUsuarioAsync(empresa, usuario, ct);
        if (result == null)
            return NotFound();
        return Ok(result);
    }
}
