using VKS.Credimatic.API.Entities;
using VKS.Credimatic.API.Models;

namespace VKS.Credimatic.API.Services;

public interface ITransaccionesService
{
    Task<IEnumerable<MdSysTransaccion>> GetAllAsync(string? producto, string? menu, CancellationToken ct = default);
    Task<IEnumerable<OpcionMenuComboDto>> GetOpcionesMenuComboAsync(CancellationToken ct = default);
    Task<MdSysTransaccion?> GetByKeyAsync(string producto, string menu, string transaccion, CancellationToken ct = default);
    Task<(bool Success, string? Error)> CreateAsync(MdSysTransaccion entity, CancellationToken ct = default);
    Task<(bool Success, string? Error)> UpdateAsync(string producto, string menu, string transaccion, MdSysTransaccion entity, CancellationToken ct = default);
    Task<bool> DeleteAsync(string producto, string menu, string transaccion, CancellationToken ct = default);
}
