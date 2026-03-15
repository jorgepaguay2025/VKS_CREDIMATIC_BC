using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VKS.Credimatic.API.Entities;
using VKS.Credimatic.API.Services;

namespace VKS.Credimatic.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AuditoriaController : ControllerBase
{
    private readonly IAuditoriaService _service;

    public AuditoriaController(IAuditoriaService service) => _service = service;

    /// <summary>
    /// Consulta registros de auditoría (MD_PRO_AUDITORIA). Filtros opcionales: fechaDesde, fechaHasta, empresa, usuario, tabla, operacion.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MdProAuditoria>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MdProAuditoria>>> Consultar(
        [FromQuery] DateTime? fechaDesde,
        [FromQuery] DateTime? fechaHasta,
        [FromQuery] string? empresa,
        [FromQuery] string? usuario,
        [FromQuery] string? tabla,
        [FromQuery] string? operacion,
        CancellationToken ct)
    {
        var result = await _service.ConsultarAsync(fechaDesde, fechaHasta, empresa, usuario, tabla, operacion, ct);
        return Ok(result);
    }

    /// <summary>
    /// Registra un registro de auditoría en MD_PRO_AUDITORIA.
    /// FECHA es opcional; si no se envía, se usa la fecha/hora actual del servidor.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Registrar([FromBody] MdProAuditoria registro, CancellationToken ct)
    {
        await _service.RegistrarAsync(registro, ct);
        return StatusCode(201, new { message = "Registro de auditoría creado." });
    }
}
