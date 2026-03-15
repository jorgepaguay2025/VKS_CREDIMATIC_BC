using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VKS.Credimatic.API.Entities;
using VKS.Credimatic.API.Services;

namespace VKS.Credimatic.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class IpAutorizadasController : ControllerBase
{
    private readonly IIpAutorizadasService _service;

    public IpAutorizadasController(IIpAutorizadasService service) => _service = service;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MdSysIpAutorizada>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MdSysIpAutorizada>>> GetAll(CancellationToken ct) =>
        Ok(await _service.GetAllAsync(ct));

    [HttpGet("{ipCliente}")]
    [ProducesResponseType(typeof(MdSysIpAutorizada), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MdSysIpAutorizada>> Get(string ipCliente, CancellationToken ct)
    {
        var item = await _service.GetByIpAsync(ipCliente, ct);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    [ProducesResponseType(typeof(MdSysIpAutorizada), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MdSysIpAutorizada>> Create([FromBody] MdSysIpAutorizada entity, CancellationToken ct)
    {
        var (success, error) = await _service.CreateAsync(entity, ct);
        if (!success) return BadRequest(new { message = error });
        return CreatedAtAction(nameof(Get), new { ipCliente = entity.IpCliente }, entity);
    }

    [HttpPut("{ipCliente}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(string ipCliente, [FromBody] MdSysIpAutorizada entity, CancellationToken ct)
    {
        var (success, error) = await _service.UpdateAsync(ipCliente, entity, ct);
        if (error == "Registro no encontrado.") return NotFound();
        if (!success) return BadRequest(new { message = error });
        return NoContent();
    }

    [HttpDelete("{ipCliente}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string ipCliente, CancellationToken ct)
    {
        var deleted = await _service.DeleteAsync(ipCliente, ct);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
