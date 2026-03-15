using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VKS.Credimatic.API.Entities;
using VKS.Credimatic.API.Services;

namespace VKS.Credimatic.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DescripcionEventosController : ControllerBase
{
    private readonly IDescripcionEventosService _service;

    public DescripcionEventosController(IDescripcionEventosService service) => _service = service;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MdSysDescripcionEvento>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MdSysDescripcionEvento>>> GetAll(CancellationToken ct) =>
        Ok(await _service.GetAllAsync(ct));

    [HttpGet("{codigoDeEvento}")]
    [ProducesResponseType(typeof(MdSysDescripcionEvento), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MdSysDescripcionEvento>> Get(string codigoDeEvento, CancellationToken ct)
    {
        var item = await _service.GetByKeyAsync(codigoDeEvento, ct);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    [ProducesResponseType(typeof(MdSysDescripcionEvento), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MdSysDescripcionEvento>> Create([FromBody] MdSysDescripcionEvento entity, CancellationToken ct)
    {
        var (success, error) = await _service.CreateAsync(entity, ct);
        if (!success) return BadRequest(new { message = error });
        return CreatedAtAction(nameof(Get), new { codigoDeEvento = entity.CodigoDeEvento }, entity);
    }

    [HttpPut("{codigoDeEvento}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(string codigoDeEvento, [FromBody] MdSysDescripcionEvento entity, CancellationToken ct)
    {
        var (success, error) = await _service.UpdateAsync(codigoDeEvento, entity, ct);
        if (error == "Registro no encontrado.") return NotFound();
        if (!success) return BadRequest(new { message = error });
        return NoContent();
    }

    [HttpDelete("{codigoDeEvento}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string codigoDeEvento, CancellationToken ct)
    {
        var deleted = await _service.DeleteAsync(codigoDeEvento, ct);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
