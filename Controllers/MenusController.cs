using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VKS.Credimatic.API.Entities;
using VKS.Credimatic.API.Services;

namespace VKS.Credimatic.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MenusController : ControllerBase
{
    private readonly IMenusService _service;

    public MenusController(IMenusService service) => _service = service;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MdSysMenu>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MdSysMenu>>> GetAll(CancellationToken ct) =>
        Ok(await _service.GetAllAsync(ct));

    [HttpGet("{producto}/{menu}")]
    [ProducesResponseType(typeof(MdSysMenu), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MdSysMenu>> Get(string producto, string menu, CancellationToken ct)
    {
        var item = await _service.GetByKeyAsync(producto, menu, ct);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    [ProducesResponseType(typeof(MdSysMenu), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MdSysMenu>> Create([FromBody] MdSysMenu entity, CancellationToken ct)
    {
        var (success, error) = await _service.CreateAsync(entity, ct);
        if (!success) return BadRequest(new { message = error });
        return CreatedAtAction(nameof(Get), new { entity.Producto, entity.Menu }, entity);
    }

    [HttpPut("{producto}/{menu}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(string producto, string menu, [FromBody] MdSysMenu entity, CancellationToken ct)
    {
        var (success, error) = await _service.UpdateAsync(producto, menu, entity, ct);
        if (error == "Registro no encontrado.") return NotFound();
        if (!success) return BadRequest(new { message = error });
        return NoContent();
    }

    [HttpDelete("{producto}/{menu}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string producto, string menu, CancellationToken ct)
    {
        var deleted = await _service.DeleteAsync(producto, menu, ct);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
