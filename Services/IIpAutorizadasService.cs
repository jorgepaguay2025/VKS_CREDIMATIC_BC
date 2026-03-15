using VKS.Credimatic.API.Entities;

namespace VKS.Credimatic.API.Services;

public interface IIpAutorizadasService
{
    Task<IEnumerable<MdSysIpAutorizada>> GetAllAsync(CancellationToken ct = default);
    Task<MdSysIpAutorizada?> GetByIpAsync(string ipCliente, CancellationToken ct = default);
    Task<(bool Success, string? Error)> CreateAsync(MdSysIpAutorizada entity, CancellationToken ct = default);
    Task<(bool Success, string? Error)> UpdateAsync(string ipCliente, MdSysIpAutorizada entity, CancellationToken ct = default);
    Task<bool> DeleteAsync(string ipCliente, CancellationToken ct = default);
}
