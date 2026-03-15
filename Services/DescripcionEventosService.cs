using Microsoft.EntityFrameworkCore;
using VKS.Credimatic.API.Data;
using VKS.Credimatic.API.Entities;

namespace VKS.Credimatic.API.Services;

public class DescripcionEventosService : IDescripcionEventosService
{
    private readonly AppDbContext _db;

    public DescripcionEventosService(AppDbContext db) => _db = db;

    public async Task<IEnumerable<MdSysDescripcionEvento>> GetAllAsync(CancellationToken ct = default) =>
        await _db.MdSysDescripcionEventos.AsNoTracking().ToListAsync(ct);

    public async Task<MdSysDescripcionEvento?> GetByKeyAsync(string codigoDeEvento, CancellationToken ct = default) =>
        await _db.MdSysDescripcionEventos.FindAsync(new object[] { codigoDeEvento }, ct);

    public async Task<(bool Success, string? Error)> CreateAsync(MdSysDescripcionEvento entity, CancellationToken ct = default)
    {
        var exists = await _db.MdSysDescripcionEventos.AnyAsync(
            x => x.CodigoDeEvento == entity.CodigoDeEvento, ct);
        if (exists)
            return (false, "Ya existe un registro con el mismo CODIGO_DE_EVENTO.");

        _db.MdSysDescripcionEventos.Add(entity);
        await _db.SaveChangesAsync(ct);
        return (true, null);
    }

    public async Task<(bool Success, string? Error)> UpdateAsync(string codigoDeEvento, MdSysDescripcionEvento entity, CancellationToken ct = default)
    {
        var existing = await GetByKeyAsync(codigoDeEvento, ct);
        if (existing == null)
            return (false, "Registro no encontrado.");

        existing.Descripcion = entity.Descripcion;
        existing.DescIngles = entity.DescIngles;
        existing.Severidad = entity.Severidad;
        existing.ReportaAlerta = entity.ReportaAlerta;
        existing.Email1 = entity.Email1;
        existing.Email2 = entity.Email2;
        existing.Email3 = entity.Email3;
        existing.Causal = entity.Causal;
        existing.LlamarResponsable = entity.LlamarResponsable;
        await _db.SaveChangesAsync(ct);
        return (true, null);
    }

    public async Task<bool> DeleteAsync(string codigoDeEvento, CancellationToken ct = default)
    {
        var existing = await GetByKeyAsync(codigoDeEvento, ct);
        if (existing == null) return false;
        _db.MdSysDescripcionEventos.Remove(existing);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}
