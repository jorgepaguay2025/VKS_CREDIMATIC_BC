using VKS.Credimatic.API.Entities;

namespace VKS.Credimatic.API.Services;

public interface IMenusService
{
    Task<IEnumerable<MdSysMenu>> GetAllAsync(CancellationToken ct = default);
    Task<MdSysMenu?> GetByKeyAsync(string producto, string menu, CancellationToken ct = default);
    Task<(bool Success, string? Error)> CreateAsync(MdSysMenu entity, CancellationToken ct = default);
    Task<(bool Success, string? Error)> UpdateAsync(string producto, string menu, MdSysMenu entity, CancellationToken ct = default);
    Task<bool> DeleteAsync(string producto, string menu, CancellationToken ct = default);
}
