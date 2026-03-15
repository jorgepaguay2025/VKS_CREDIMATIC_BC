using Microsoft.EntityFrameworkCore;
using VKS.Credimatic.API.Data;
using VKS.Credimatic.API.Entities;

namespace VKS.Credimatic.API.Services;

public class CatalogosService : ICatalogosService
{
    /// <summary>Item del registro principal del catálogo (cabecera). Los detalles tienen Item distinto.</summary>
    private const string ItemPrincipal = "000000";
    private const string EstadoActivo = "A";
    private readonly AppDbContext _db;

    public CatalogosService(AppDbContext db) => _db = db;

    public async Task<IEnumerable<MdSysCatalogoSistema>> GetByCatalogoAsync(string? catalogo, string? item, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(catalogo))
        {
            var query = _db.MdSysCatalogosSistema
            .AsNoTracking()
            .Where(x => x.Item == item && x.Estado == EstadoActivo);

            return await query.ToListAsync(ct);
        }
        else
        {
            var query = _db.MdSysCatalogosSistema
                .AsNoTracking()
                .Where(x => x.Catalogo == catalogo && x.Item != item && x.Estado == EstadoActivo);

            if (!string.IsNullOrWhiteSpace(catalogo))
                query = query.Where(x => x.Catalogo == catalogo.Trim());

            return await query.ToListAsync(ct);
        }
    }

    public async Task<MdSysCatalogoSistema?> GetByKeyAsync(string catalogo, string item, CancellationToken ct = default) =>
        await _db.MdSysCatalogosSistema.FindAsync(new object[] { catalogo, item }, ct);

    public async Task<(bool Success, string? Error)> CreateAsync(MdSysCatalogoSistema entity, CancellationToken ct = default)
    {
        var exists = await _db.MdSysCatalogosSistema.AnyAsync(
            x => x.Catalogo == entity.Catalogo && x.Item == entity.Item, ct);
        if (exists)
            return (false, "Ya existe un registro con el mismo Catalogo e Item.");

        // Si es registro detalle (Item != '000000'), debe existir el registro principal (Catalogo, '000000')
        if (!string.Equals(entity.Item?.Trim(), ItemPrincipal, StringComparison.Ordinal))
        {
            var principalExiste = await _db.MdSysCatalogosSistema.AnyAsync(
                x => x.Catalogo == entity.Catalogo && x.Item == ItemPrincipal, ct);
            if (!principalExiste)
                return (false, "No existe el registro principal del catálogo (Catalogo, Item='000000'). Debe crearlo primero.");
        }

        _db.MdSysCatalogosSistema.Add(entity);
        await _db.SaveChangesAsync(ct);
        return (true, null);
    }

    public async Task<(bool Success, string? Error)> UpdateAsync(string catalogo, string item, MdSysCatalogoSistema entity, CancellationToken ct = default)
    {
        var existing = await GetByKeyAsync(catalogo, item, ct);
        if (existing == null)
            return (false, "Registro no encontrado.");

        existing.Descripcion = entity.Descripcion;
        existing.Estado = entity.Estado;
        await _db.SaveChangesAsync(ct);
        return (true, null);
    }

    public async Task<bool> DeleteAsync(string catalogo, string item, CancellationToken ct = default)
    {
        var existing = await GetByKeyAsync(catalogo, item, ct);
        if (existing == null) return false;
        _db.MdSysCatalogosSistema.Remove(existing);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}
