using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VKS.Credimatic.API.Models;
using VKS.Credimatic.API.Services;

namespace VKS.Credimatic.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SistemaController : ControllerBase
{
    private readonly ISistemaService _service;

    public SistemaController(ISistemaService service) => _service = service;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MdSysSistemaDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MdSysSistemaDto>>> GetAll(CancellationToken ct) =>
        Ok(await _service.GetAllAsync(ct));

    [HttpGet("{producto}/{empresa}")]
    [ProducesResponseType(typeof(MdSysSistemaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MdSysSistemaDto>> Get(string producto, string empresa, CancellationToken ct)
    {
        var item = await _service.GetByKeyAsync(producto, empresa, ct);
        if (item == null) return NotFound();
        return Ok(item);
    }

    /// <summary>
    /// Crea registro. DB_User, DB_Password, DB_Server_Name, DB_Server_IP, Db_Server_Port en texto (ej: "sa", "1235"); se convierten a binary en BD.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(MdSysSistemaDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MdSysSistemaDto>> Create([FromBody] MdSysSistemaRequestDto dto, CancellationToken ct)
    {
        var (success, error) = await _service.CreateAsync(dto, ct);
        if (!success) return BadRequest(new { message = error });
        var created = await _service.GetByKeyAsync(dto.Producto, dto.Empresa, ct);
        return CreatedAtAction(nameof(Get), new { producto = dto.Producto, empresa = dto.Empresa }, created);
    }

    /// <summary>
    /// Actualiza. Los campos binarios (DB_User, DB_Password, etc.) en texto ASCII; se convierten a binary en BD.
    /// </summary>
    [HttpPut("{producto}/{empresa}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(string producto, string empresa, [FromBody] MdSysSistemaRequestDto dto, CancellationToken ct)
    {
        var (success, error) = await _service.UpdateAsync(producto, empresa, dto, ct);
        if (error == "Registro no encontrado.") return NotFound();
        if (!success) return BadRequest(new { message = error });
        return NoContent();
    }

    [HttpDelete("{producto}/{empresa}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string producto, string empresa, CancellationToken ct)
    {
        var deleted = await _service.DeleteAsync(producto, empresa, ct);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
