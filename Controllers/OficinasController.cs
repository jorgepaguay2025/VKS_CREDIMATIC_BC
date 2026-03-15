using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VKS.Credimatic.API.Entities;
using VKS.Credimatic.API.Services;

namespace VKS.Credimatic.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OficinasController : ControllerBase
{
    private readonly IOficinasService _service;

    public OficinasController(IOficinasService service) => _service = service;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MdSysOficina>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MdSysOficina>>> GetAll(CancellationToken ct) =>
        Ok(await _service.GetAllAsync(ct));

    [HttpGet("{empresa}/{tipoOficina}/{oficina}")]
    [ProducesResponseType(typeof(MdSysOficina), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MdSysOficina>> Get(string empresa, string tipoOficina, string oficina, CancellationToken ct)
    {
        var item = await _service.GetByKeyAsync(empresa, tipoOficina, oficina, ct);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    [ProducesResponseType(typeof(MdSysOficina), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MdSysOficina>> Create([FromBody] MdSysOficina entity, CancellationToken ct)
    {
        var (success, error) = await _service.CreateAsync(entity, ct);
        if (!success) return BadRequest(new { message = error });
        return CreatedAtAction(nameof(Get), new { entity.Empresa, entity.TipoOficina, entity.Oficina }, entity);
    }

    [HttpPut("{empresa}/{tipoOficina}/{oficina}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(string empresa, string tipoOficina, string oficina, [FromBody] MdSysOficina entity, CancellationToken ct)
    {
        var (success, error) = await _service.UpdateAsync(empresa, tipoOficina, oficina, entity, ct);
        if (error == "Registro no encontrado.") return NotFound();
        if (!success) return BadRequest(new { message = error });
        return NoContent();
    }

    [HttpDelete("{empresa}/{tipoOficina}/{oficina}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string empresa, string tipoOficina, string oficina, CancellationToken ct)
    {
        var deleted = await _service.DeleteAsync(empresa, tipoOficina, oficina, ct);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
