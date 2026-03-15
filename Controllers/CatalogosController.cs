using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VKS.Credimatic.API.Entities;
using VKS.Credimatic.API.Services;

namespace VKS.Credimatic.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CatalogosController : ControllerBase
{
    private readonly ICatalogosService _service;

    public CatalogosController(ICatalogosService service) => _service = service;

    /// <summary>
    /// Lista ítems del catálogo. Filtro: Catalogo (opcional), Item (opcional).
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MdSysCatalogoSistema>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MdSysCatalogoSistema>>> Get(
        [FromQuery] string? catalogo,
        [FromQuery] string? item,
        CancellationToken ct)
    {
        var result = await _service.GetByCatalogoAsync(catalogo?.Trim(), item?.Trim(), ct);
        return Ok(result);
    }

    /// <summary>
    /// Obtiene un ítem por clave (Catalogo, Item).
    /// </summary>
    [HttpGet("{catalogo}/{item}")]
    [ProducesResponseType(typeof(MdSysCatalogoSistema), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MdSysCatalogoSistema>> GetByKey(string catalogo, string item, CancellationToken ct)
    {
        var result = await _service.GetByKeyAsync(catalogo, item, ct);
        if (result == null) return NotFound();
        return Ok(result);
    }

    /// <summary>
    /// Crea registro. Si Item != '000000' (detalle), debe existir el principal (Catalogo, Item='000000').
    /// Clave: Catalogo, Item.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(MdSysCatalogoSistema), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MdSysCatalogoSistema>> Create([FromBody] MdSysCatalogoSistema entity, CancellationToken ct)
    {
        var (success, error) = await _service.CreateAsync(entity, ct);
        if (!success) return BadRequest(new { message = error });
        return CreatedAtAction(nameof(GetByKey), new { catalogo = entity.Catalogo, item = entity.Item }, entity);
    }

    /// <summary>
    /// Actualiza Descripcion y Estado. La clave (Catalogo, Item) no se modifica.
    /// </summary>
    [HttpPut("{catalogo}/{item}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(string catalogo, string item, [FromBody] MdSysCatalogoSistema entity, CancellationToken ct)
    {
        var (success, error) = await _service.UpdateAsync(catalogo, item, entity, ct);
        if (error == "Registro no encontrado.") return NotFound();
        if (!success) return BadRequest(new { message = error });
        return NoContent();
    }

    /// <summary>
    /// Elimina el registro por (Catalogo, Item).
    /// </summary>
    [HttpDelete("{catalogo}/{item}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string catalogo, string item, CancellationToken ct)
    {
        var deleted = await _service.DeleteAsync(catalogo, item, ct);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
