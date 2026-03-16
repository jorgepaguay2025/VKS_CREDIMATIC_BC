using Microsoft.EntityFrameworkCore;
using VKS.Credimatic.API.Data;
using VKS.Credimatic.API.Entities;

namespace VKS.Credimatic.API.Services
{
    public class EmpresasService: IEmpresasService
    {
        private readonly AppDbContext _db;

        public EmpresasService(AppDbContext db) => _db = db;
        public async Task<IEnumerable<MdSysEmpresa>> GetAllAsync(CancellationToken ct = default) =>
            await _db.MdSysEmpresas.ToListAsync(ct);

        public async Task<MdSysEmpresa?> GetByKeyAsync(string empresa, CancellationToken ct = default) =>
            await _db.MdSysEmpresas.FindAsync(new object[] { empresa }, ct);

        public async Task<(bool Success, string? Error)> CreateAsync(MdSysEmpresa entity, CancellationToken ct = default)
        {
            var exists = await _db.MdSysEmpresas.AnyAsync(
                x => x.Empresa == entity.Empresa , ct);
            if (exists)
                return (false, "Ya existe un registro con la misma Empresa y Area.");

            _db.MdSysEmpresas.Add(entity);
            await _db.SaveChangesAsync(ct);
            return (true, null);
        }

        public async Task<(bool Success, string? Error)> UpdateAsync(string empresa, MdSysEmpresa entity, CancellationToken ct = default)
        {
            var existing = await GetByKeyAsync(empresa,  ct);
            if (existing == null)
                return (false, "Registro no encontrado.");

            existing.Ruc = entity.Ruc;
            existing.RazonSocial = entity.RazonSocial;
            existing.RazonComercial = entity.RazonComercial;
            existing.Ciudad = entity.Ciudad;
            existing.Pais = entity.Pais;
            existing.Direccion = entity.Direccion;
            existing.Telefono1 = entity.Telefono1;
            existing.Telefono2 = entity.Telefono2;
            existing.Estado = entity.Estado;
            await _db.SaveChangesAsync(ct);
            return (true, null);
        }

        public async Task<bool> DeleteAsync(string empresa,  CancellationToken ct = default)
        {
            var existing = await GetByKeyAsync(empresa,  ct);
            if (existing == null) return false;
            _db.MdSysEmpresas.Remove(existing);
            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
