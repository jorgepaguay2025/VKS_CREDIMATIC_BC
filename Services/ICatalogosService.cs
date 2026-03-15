using VKS.Credimatic.API.Entities;

namespace VKS.Credimatic.API.Services;

public interface ICatalogosService
{
    Task<IEnumerable<MdSysCatalogoSistema>> GetByCatalogoAsync(string? catalogo, string? item, CancellationToken ct = default);
    Task<MdSysCatalogoSistema?> GetByKeyAsync(string catalogo, string item, CancellationToken ct = default);
    Task<(bool Success, string? Error)> CreateAsync(MdSysCatalogoSistema entity, CancellationToken ct = default);
    Task<(bool Success, string? Error)> UpdateAsync(string catalogo, string item, MdSysCatalogoSistema entity, CancellationToken ct = default);
    Task<bool> DeleteAsync(string catalogo, string item, CancellationToken ct = default);
}
