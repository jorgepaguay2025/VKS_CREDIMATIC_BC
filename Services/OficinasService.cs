using Microsoft.EntityFrameworkCore;
using VKS.Credimatic.API.Data;
using VKS.Credimatic.API.Entities;

namespace VKS.Credimatic.API.Services;

public class OficinasService : IOficinasService
{
    private readonly AppDbContext _db;

    public OficinasService(AppDbContext db) => _db = db;

    public async Task<IEnumerable<MdSysOficina>> GetAllAsync(CancellationToken ct = default) =>
        await _db.MdSysOficinas.ToListAsync(ct);

    public async Task<MdSysOficina?> GetByKeyAsync(string empresa, string tipoOficina, string oficina, CancellationToken ct = default) =>
        await _db.MdSysOficinas.FindAsync(new object[] { empresa, tipoOficina, oficina }, ct);

    public async Task<(bool Success, string? Error)> CreateAsync(MdSysOficina entity, CancellationToken ct = default)
    {
        var exists = await _db.MdSysOficinas.AnyAsync(
            x => x.Empresa == entity.Empresa && x.TipoOficina == entity.TipoOficina && x.Oficina == entity.Oficina, ct);
        if (exists)
            return (false, "Ya existe un registro con la misma Empresa, Tipo_Oficina y Oficina.");

        _db.MdSysOficinas.Add(entity);
        await _db.SaveChangesAsync(ct);
        return (true, null);
    }

    public async Task<(bool Success, string? Error)> UpdateAsync(string empresa, string tipoOficina, string oficina, MdSysOficina entity, CancellationToken ct = default)
    {
        var existing = await GetByKeyAsync(empresa, tipoOficina, oficina, ct);
        if (existing == null)
            return (false, "Registro no encontrado.");

        // Actualización: no se valida duplicado, se actualizan solo los campos editables
        existing.Nombre = entity.Nombre;
        existing.Direccion = entity.Direccion;
        existing.Estado = entity.Estado;
        existing.TiempoDepuracion = entity.TiempoDepuracion;
        existing.RutaAlmacenamiento = entity.RutaAlmacenamiento;
        existing.CapacidadUnidad = entity.CapacidadUnidad;
        await _db.SaveChangesAsync(ct);
        return (true, null);
    }

    public async Task<bool> DeleteAsync(string empresa, string tipoOficina, string oficina, CancellationToken ct = default)
    {
        var existing = await GetByKeyAsync(empresa, tipoOficina, oficina, ct);
        if (existing == null) return false;
        _db.MdSysOficinas.Remove(existing);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}
