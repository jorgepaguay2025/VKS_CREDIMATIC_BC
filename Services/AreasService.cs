using Microsoft.EntityFrameworkCore;
using VKS.Credimatic.API.Data;
using VKS.Credimatic.API.Entities;

namespace VKS.Credimatic.API.Services;

public class AreasService : IAreasService
{
    private readonly AppDbContext _db;

    public AreasService(AppDbContext db) => _db = db;

    public async Task<IEnumerable<MdSysArea>> GetAllAsync(CancellationToken ct = default) =>
        await _db.MdSysAreas.ToListAsync(ct);

    public async Task<MdSysArea?> GetByKeyAsync(string empresa, string area, CancellationToken ct = default) =>
        await _db.MdSysAreas.FindAsync(new object[] { empresa, area }, ct);

    public async Task<(bool Success, string? Error)> CreateAsync(MdSysArea entity, CancellationToken ct = default)
    {
        var exists = await _db.MdSysAreas.AnyAsync(
            x => x.Empresa == entity.Empresa && x.Area == entity.Area, ct);
        if (exists)
            return (false, "Ya existe un registro con la misma Empresa y Area.");

        _db.MdSysAreas.Add(entity);
        await _db.SaveChangesAsync(ct);
        return (true, null);
    }

    public async Task<(bool Success, string? Error)> UpdateAsync(string empresa, string area, MdSysArea entity, CancellationToken ct = default)
    {
        var existing = await GetByKeyAsync(empresa, area, ct);
        if (existing == null)
            return (false, "Registro no encontrado.");

        existing.Departamento = entity.Departamento;
        existing.Nombre = entity.Nombre;
        existing.Estado = entity.Estado;
        await _db.SaveChangesAsync(ct);
        return (true, null);
    }

    public async Task<bool> DeleteAsync(string empresa, string area, CancellationToken ct = default)
    {
        var existing = await GetByKeyAsync(empresa, area, ct);
        if (existing == null) return false;
        _db.MdSysAreas.Remove(existing);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}
