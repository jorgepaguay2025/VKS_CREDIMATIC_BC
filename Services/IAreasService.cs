using VKS.Credimatic.API.Entities;

namespace VKS.Credimatic.API.Services;

public interface IAreasService
{
    Task<IEnumerable<MdSysArea>> GetAllAsync(CancellationToken ct = default);
    Task<MdSysArea?> GetByKeyAsync(string empresa, string area, CancellationToken ct = default);
    Task<(bool Success, string? Error)> CreateAsync(MdSysArea entity, CancellationToken ct = default);
    Task<(bool Success, string? Error)> UpdateAsync(string empresa, string area, MdSysArea entity, CancellationToken ct = default);
    Task<bool> DeleteAsync(string empresa, string area, CancellationToken ct = default);
}
