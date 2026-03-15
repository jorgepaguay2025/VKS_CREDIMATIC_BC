using VKS.Credimatic.API.Entities;

namespace VKS.Credimatic.API.Services;

public interface IAuditoriaService
{
    Task<IEnumerable<MdProAuditoria>> ConsultarAsync(DateTime? fechaDesde, DateTime? fechaHasta, string? empresa, string? usuario, string? tabla, string? operacion, CancellationToken ct = default);
    Task<bool> RegistrarAsync(MdProAuditoria registro, CancellationToken ct = default);
}
