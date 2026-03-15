using Microsoft.EntityFrameworkCore;
using VKS.Credimatic.API.Data;
using VKS.Credimatic.API.Entities;

namespace VKS.Credimatic.API.Services;

public class MenusService : IMenusService
{
    private readonly AppDbContext _db;

    public MenusService(AppDbContext db) => _db = db;

    public async Task<IEnumerable<MdSysMenu>> GetAllAsync(CancellationToken ct = default) =>
        await _db.MdSysMenus.ToListAsync(ct);

    public async Task<MdSysMenu?> GetByKeyAsync(string producto, string menu, CancellationToken ct = default) =>
        await _db.MdSysMenus.FindAsync(new object[] { producto, menu }, ct);

    public async Task<(bool Success, string? Error)> CreateAsync(MdSysMenu entity, CancellationToken ct = default)
    {
        var exists = await _db.MdSysMenus.AnyAsync(
            x => x.Producto == entity.Producto && x.Menu == entity.Menu, ct);
        if (exists)
            return (false, "Ya existe un registro con el mismo Producto y Menu.");

        _db.MdSysMenus.Add(entity);
        await _db.SaveChangesAsync(ct);
        return (true, null);
    }

    public async Task<(bool Success, string? Error)> UpdateAsync(string producto, string menu, MdSysMenu entity, CancellationToken ct = default)
    {
        var existing = await GetByKeyAsync(producto, menu, ct);
        if (existing == null)
            return (false, "Registro no encontrado.");

        existing.Nombre = entity.Nombre;
        existing.Descripcion = entity.Descripcion;
        existing.Etiqueta = entity.Etiqueta;
        existing.Estado = entity.Estado;
        existing.Secuencia = entity.Secuencia;
        await _db.SaveChangesAsync(ct);
        return (true, null);
    }

    public async Task<bool> DeleteAsync(string producto, string menu, CancellationToken ct = default)
    {
        var existing = await GetByKeyAsync(producto, menu, ct);
        if (existing == null) return false;
        _db.MdSysMenus.Remove(existing);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}
