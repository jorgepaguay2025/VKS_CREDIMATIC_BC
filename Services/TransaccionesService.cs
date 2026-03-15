using Microsoft.EntityFrameworkCore;
using VKS.Credimatic.API.Data;
using VKS.Credimatic.API.Entities;
using VKS.Credimatic.API.Models;

namespace VKS.Credimatic.API.Services;

public class TransaccionesService : ITransaccionesService
{
    private readonly AppDbContext _db;

    public TransaccionesService(AppDbContext db) => _db = db;

    public async Task<IEnumerable<MdSysTransaccion>> GetAllAsync(string? producto, string? menu, CancellationToken ct = default)
    {
        var query = _db.MdSysTransacciones.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(producto))
            query = query.Where(x => x.Producto == producto);
        if (!string.IsNullOrWhiteSpace(menu))
            query = query.Where(x => x.Menu == menu);

        return await query.OrderBy(x => x.SecuenciaMenu).ToListAsync(ct);
    }

    /// <summary>
    /// Lista de menús para el combo (filtrar transacciones por Menu). Origen: MD_SYS_MENU.
    /// </summary>
    public async Task<IEnumerable<OpcionMenuComboDto>> GetOpcionesMenuComboAsync(CancellationToken ct = default)
    {
        return await _db.MdSysMenus
            .AsNoTracking()
            .Where(x => x.Estado == "A")
            .OrderBy(x => x.Secuencia)
            .Select(x => new OpcionMenuComboDto
            {
                Producto = x.Producto,
                Menu = x.Menu,
                Nombre = x.Nombre
            })
            .ToListAsync(ct);
    }

    public async Task<MdSysTransaccion?> GetByKeyAsync(string producto, string menu, string transaccion, CancellationToken ct = default) =>
        await _db.MdSysTransacciones.FindAsync(new object[] { producto, menu, transaccion }, ct);

    public async Task<(bool Success, string? Error)> CreateAsync(MdSysTransaccion entity, CancellationToken ct = default)
    {
        var exists = await _db.MdSysTransacciones.AnyAsync(
            x => x.Producto == entity.Producto && x.Menu == entity.Menu && x.Transaccion == entity.Transaccion, ct);
        if (exists)
            return (false, "Ya existe un registro con el mismo Producto, Menu y Transaccion.");

        _db.MdSysTransacciones.Add(entity);
        await _db.SaveChangesAsync(ct);
        return (true, null);
    }

    public async Task<(bool Success, string? Error)> UpdateAsync(string producto, string menu, string transaccion, MdSysTransaccion entity, CancellationToken ct = default)
    {
        var existing = await GetByKeyAsync(producto, menu, transaccion, ct);
        if (existing == null)
            return (false, "Registro no encontrado.");

        existing.Nombre = entity.Nombre;
        existing.Descripcion = entity.Descripcion;
        existing.Etiqueta = entity.Etiqueta;
        existing.SecuenciaMenu = entity.SecuenciaMenu;
        existing.Programa = entity.Programa;
        existing.Estado = entity.Estado;
        await _db.SaveChangesAsync(ct);
        return (true, null);
    }

    public async Task<bool> DeleteAsync(string producto, string menu, string transaccion, CancellationToken ct = default)
    {
        var existing = await GetByKeyAsync(producto, menu, transaccion, ct);
        if (existing == null) return false;
        _db.MdSysTransacciones.Remove(existing);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}
