using VKS.Credimatic.API.Entities;

namespace VKS.Credimatic.API.Services
{
    public interface IEmpresasService
    {
        Task<IEnumerable<MdSysEmpresa>> GetAllAsync(CancellationToken ct = default);
        Task<MdSysEmpresa?> GetByKeyAsync(string empresa,CancellationToken ct = default);
        Task<(bool Success, string? Error)> CreateAsync(MdSysEmpresa entity, CancellationToken ct = default);
        Task<(bool Success, string? Error)> UpdateAsync(string empresa,  MdSysEmpresa entity, CancellationToken ct = default);
        Task<bool> DeleteAsync(string empresa,  CancellationToken ct = default);
    }
}
