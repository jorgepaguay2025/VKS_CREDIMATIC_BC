using VKS.Credimatic.API.Models;

namespace VKS.Credimatic.API.Services;

public interface ISistemaService
{
    Task<IEnumerable<MdSysSistemaDto>> GetAllAsync(CancellationToken ct = default);
    Task<MdSysSistemaDto?> GetByKeyAsync(string producto, string empresa, CancellationToken ct = default);
    Task<(bool Success, string? Error)> CreateAsync(MdSysSistemaRequestDto dto, CancellationToken ct = default);
    Task<(bool Success, string? Error)> UpdateAsync(string producto, string empresa, MdSysSistemaRequestDto dto, CancellationToken ct = default);
    Task<bool> DeleteAsync(string producto, string empresa, CancellationToken ct = default);
}
