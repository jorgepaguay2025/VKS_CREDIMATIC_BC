using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VKS.Credimatic.API.Entities;
using VKS.Credimatic.API.Services;

namespace VKS.Credimatic.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AreasController : ControllerBase
{
    private readonly IAreasService _service;

    public AreasController(IAreasService service) => _service = service;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MdSysArea>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MdSysArea>>> GetAll(CancellationToken ct) =>
        Ok(await _service.GetAllAsync(ct));

    [HttpGet("{empresa}/{area}")]
    [ProducesResponseType(typeof(MdSysArea), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MdSysArea>> Get(string empresa, string area, CancellationToken ct)
    {
        var item = await _service.GetByKeyAsync(empresa, area, ct);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    [ProducesResponseType(typeof(MdSysArea), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MdSysArea>> Create([FromBody] MdSysArea entity, CancellationToken ct)
    {
        var (success, error) = await _service.CreateAsync(entity, ct);
        if (!success) return BadRequest(new { message = error });
        return CreatedAtAction(nameof(Get), new { entity.Empresa, entity.Area }, entity);
    }

    [HttpPut("{empresa}/{area}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(string empresa, string area, [FromBody] MdSysArea entity, CancellationToken ct)
    {
        var (success, error) = await _service.UpdateAsync(empresa, area, entity, ct);
        if (error == "Registro no encontrado.") return NotFound();
        if (!success) return BadRequest(new { message = error });
        return NoContent();
    }

    [HttpDelete("{empresa}/{area}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string empresa, string area, CancellationToken ct)
    {
        var deleted = await _service.DeleteAsync(empresa, area, ct);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
