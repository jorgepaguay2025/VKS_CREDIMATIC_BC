using VKS.Credimatic.API.Entities;

namespace VKS.Credimatic.API.Services;

public interface IOficinasService
{
    Task<IEnumerable<MdSysOficina>> GetAllAsync(CancellationToken ct = default);
    Task<MdSysOficina?> GetByKeyAsync(string empresa, string tipoOficina, string oficina, CancellationToken ct = default);
    Task<(bool Success, string? Error)> CreateAsync(MdSysOficina entity, CancellationToken ct = default);
    Task<(bool Success, string? Error)> UpdateAsync(string empresa, string tipoOficina, string oficina, MdSysOficina entity, CancellationToken ct = default);
    Task<bool> DeleteAsync(string empresa, string tipoOficina, string oficina, CancellationToken ct = default);
}
