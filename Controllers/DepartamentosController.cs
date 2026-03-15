using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VKS.Credimatic.API.Entities;
using VKS.Credimatic.API.Services;

namespace VKS.Credimatic.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DepartamentosController : ControllerBase
{
    private readonly IDepartamentosService _service;

    public DepartamentosController(IDepartamentosService service) => _service = service;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MdSysDepartamento>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MdSysDepartamento>>> GetAll(CancellationToken ct) =>
        Ok(await _service.GetAllAsync(ct));

    [HttpGet("{empresa}/{departamento}")]
    [ProducesResponseType(typeof(MdSysDepartamento), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MdSysDepartamento>> Get(string empresa, string departamento, CancellationToken ct)
    {
        var item = await _service.GetByKeyAsync(empresa, departamento, ct);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    [ProducesResponseType(typeof(MdSysDepartamento), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MdSysDepartamento>> Create([FromBody] MdSysDepartamento entity, CancellationToken ct)
    {
        var (success, error) = await _service.CreateAsync(entity, ct);
        if (!success) return BadRequest(new { message = error });
        return CreatedAtAction(nameof(Get), new { entity.Empresa, entity.Departamento }, entity);
    }

    [HttpPut("{empresa}/{departamento}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(string empresa, string departamento, [FromBody] MdSysDepartamento entity, CancellationToken ct)
    {
        var (success, error) = await _service.UpdateAsync(empresa, departamento, entity, ct);
        if (error == "Registro no encontrado.") return NotFound();
        if (!success) return BadRequest(new { message = error });
        return NoContent();
    }

    [HttpDelete("{empresa}/{departamento}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string empresa, string departamento, CancellationToken ct)
    {
        var deleted = await _service.DeleteAsync(empresa, departamento, ct);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
