using VKS.Credimatic.API.Entities;

namespace VKS.Credimatic.API.Services;

public interface IDescripcionEventosService
{
    Task<IEnumerable<MdSysDescripcionEvento>> GetAllAsync(CancellationToken ct = default);
    Task<MdSysDescripcionEvento?> GetByKeyAsync(string codigoDeEvento, CancellationToken ct = default);
    Task<(bool Success, string? Error)> CreateAsync(MdSysDescripcionEvento entity, CancellationToken ct = default);
    Task<(bool Success, string? Error)> UpdateAsync(string codigoDeEvento, MdSysDescripcionEvento entity, CancellationToken ct = default);
    Task<bool> DeleteAsync(string codigoDeEvento, CancellationToken ct = default);
}
