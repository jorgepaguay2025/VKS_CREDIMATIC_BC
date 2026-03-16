using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VKS.Credimatic.API.Entities;
using VKS.Credimatic.API.Services;

namespace VKS.Credimatic.API.Controllers
{
    public class EmpresasController : Controller
    {


        private readonly IEmpresasService _service;

        public EmpresasController(IEmpresasService service) => _service = service;

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MdSysArea>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MdSysArea>>> GetAll(CancellationToken ct) =>
            Ok(await _service.GetAllAsync(ct));

        [HttpGet("{empresa}")]
        [ProducesResponseType(typeof(MdSysArea), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MdSysArea>> Get(string empresa, CancellationToken ct)
        {
            var item = await _service.GetByKeyAsync(empresa, ct);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        [ProducesResponseType(typeof(MdSysArea), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MdSysArea>> Create([FromBody] MdSysEmpresa entity, CancellationToken ct)
        {
            var (success, error) = await _service.CreateAsync(entity, ct);
            if (!success) return BadRequest(new { message = error });
            return CreatedAtAction(nameof(Get), new { entity.Empresa }, entity);
        }

        [HttpPut("{empresa}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(string empresa,  [FromBody] MdSysEmpresa entity, CancellationToken ct)
        {
            var (success, error) = await _service.UpdateAsync(empresa,  entity, ct);
            if (error == "Registro no encontrado.") return NotFound();
            if (!success) return BadRequest(new { message = error });
            return NoContent();
        }

        [HttpDelete("{empresa}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string empresa, CancellationToken ct)
        {
            var deleted = await _service.DeleteAsync(empresa,  ct);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
