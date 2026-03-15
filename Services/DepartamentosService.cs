using Microsoft.EntityFrameworkCore;
using VKS.Credimatic.API.Data;
using VKS.Credimatic.API.Entities;

namespace VKS.Credimatic.API.Services;

public class DepartamentosService : IDepartamentosService
{
    private readonly AppDbContext _db;

    public DepartamentosService(AppDbContext db) => _db = db;

    public async Task<IEnumerable<MdSysDepartamento>> GetAllAsync(CancellationToken ct = default) =>
        await _db.MdSysDepartamentos.ToListAsync(ct);

    public async Task<MdSysDepartamento?> GetByKeyAsync(string empresa, string departamento, CancellationToken ct = default) =>
        await _db.MdSysDepartamentos.FindAsync(new object[] { empresa, departamento }, ct);

    public async Task<(bool Success, string? Error)> CreateAsync(MdSysDepartamento entity, CancellationToken ct = default)
    {
        var exists = await _db.MdSysDepartamentos.AnyAsync(
            x => x.Empresa == entity.Empresa && x.Departamento == entity.Departamento, ct);
        if (exists)
            return (false, "Ya existe un registro con la misma Empresa y Departamento.");

        _db.MdSysDepartamentos.Add(entity);
        await _db.SaveChangesAsync(ct);
        return (true, null);
    }

    public async Task<(bool Success, string? Error)> UpdateAsync(string empresa, string departamento, MdSysDepartamento entity, CancellationToken ct = default)
    {
        var existing = await GetByKeyAsync(empresa, departamento, ct);
        if (existing == null)
            return (false, "Registro no encontrado.");

        if (existing.Empresa != entity.Empresa || existing.Departamento != entity.Departamento)
        {
            var duplicate = await _db.MdSysDepartamentos.AnyAsync(
                x => x.Empresa == entity.Empresa && x.Departamento == entity.Departamento, ct);
            if (duplicate)
                return (false, "Ya existe un registro con la misma Empresa y Departamento.");
        }

        existing.Nombre = entity.Nombre;
        existing.Estado = entity.Estado;
        await _db.SaveChangesAsync(ct);
        return (true, null);
    }

    public async Task<bool> DeleteAsync(string empresa, string departamento, CancellationToken ct = default)
    {
        var existing = await GetByKeyAsync(empresa, departamento, ct);
        if (existing == null) return false;
        _db.MdSysDepartamentos.Remove(existing);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}
