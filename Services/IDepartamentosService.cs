using VKS.Credimatic.API.Entities;

namespace VKS.Credimatic.API.Services;

public interface IDepartamentosService
{
    Task<IEnumerable<MdSysDepartamento>> GetAllAsync(CancellationToken ct = default);
    Task<MdSysDepartamento?> GetByKeyAsync(string empresa, string departamento, CancellationToken ct = default);
    Task<(bool Success, string? Error)> CreateAsync(MdSysDepartamento entity, CancellationToken ct = default);
    Task<(bool Success, string? Error)> UpdateAsync(string empresa, string departamento, MdSysDepartamento entity, CancellationToken ct = default);
    Task<bool> DeleteAsync(string empresa, string departamento, CancellationToken ct = default);
}
