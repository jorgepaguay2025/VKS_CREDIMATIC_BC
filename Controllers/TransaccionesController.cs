using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VKS.Credimatic.API.Entities;
using VKS.Credimatic.API.Models;
using VKS.Credimatic.API.Services;

namespace VKS.Credimatic.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TransaccionesController : ControllerBase
{
    private readonly ITransaccionesService _service;

    public TransaccionesController(ITransaccionesService service) => _service = service;

    /// <summary>
    /// Lista transacciones. Filtro opcional: Producto, Menu (combo de menús).
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MdSysTransaccion>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MdSysTransaccion>>> Get(
        [FromQuery] string? producto,
        [FromQuery] string? menu,
        CancellationToken ct)
    {
        var result = await _service.GetAllAsync(producto?.Trim(), menu?.Trim(), ct);
        return Ok(result);
    }

    /// <summary>
    /// Opciones para el combo de menús (filtrar transacciones por Menu). Origen: MD_SYS_MENU.
    /// </summary>
    [HttpGet("opciones-menu")]
    [ProducesResponseType(typeof(IEnumerable<OpcionMenuComboDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<OpcionMenuComboDto>>> GetOpcionesMenuCombo(CancellationToken ct) =>
        Ok(await _service.GetOpcionesMenuComboAsync(ct));

    /// <summary>
    /// Obtiene una transacción por clave (Producto, Menu, Transaccion).
    /// </summary>
    [HttpGet("{producto}/{menu}/{transaccion}")]
    [ProducesResponseType(typeof(MdSysTransaccion), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MdSysTransaccion>> GetByKey(string producto, string menu, string transaccion, CancellationToken ct)
    {
        var result = await _service.GetByKeyAsync(producto, menu, transaccion, ct);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(MdSysTransaccion), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MdSysTransaccion>> Create([FromBody] MdSysTransaccion entity, CancellationToken ct)
    {
        var (success, error) = await _service.CreateAsync(entity, ct);
        if (!success) return BadRequest(new { message = error });
        return CreatedAtAction(nameof(GetByKey), new { entity.Producto, entity.Menu, entity.Transaccion }, entity);
    }

    [HttpPut("{producto}/{menu}/{transaccion}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(string producto, string menu, string transaccion, [FromBody] MdSysTransaccion entity, CancellationToken ct)
    {
        var (success, error) = await _service.UpdateAsync(producto, menu, transaccion, entity, ct);
        if (error == "Registro no encontrado.") return NotFound();
        if (!success) return BadRequest(new { message = error });
        return NoContent();
    }

    [HttpDelete("{producto}/{menu}/{transaccion}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string producto, string menu, string transaccion, CancellationToken ct)
    {
        var deleted = await _service.DeleteAsync(producto, menu, transaccion, ct);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
