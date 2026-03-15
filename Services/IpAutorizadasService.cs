using Microsoft.EntityFrameworkCore;
using VKS.Credimatic.API.Data;
using VKS.Credimatic.API.Entities;

namespace VKS.Credimatic.API.Services;

public class IpAutorizadasService : IIpAutorizadasService
{
    private readonly AppDbContext _db;

    public IpAutorizadasService(AppDbContext db) => _db = db;

    public async Task<IEnumerable<MdSysIpAutorizada>> GetAllAsync(CancellationToken ct = default) =>
        await _db.MdSysIpAutorizadas.ToListAsync(ct);

    public async Task<MdSysIpAutorizada?> GetByIpAsync(string ipCliente, CancellationToken ct = default) =>
        await _db.MdSysIpAutorizadas.FindAsync(new object[] { ipCliente }, ct);

    public async Task<(bool Success, string? Error)> CreateAsync(MdSysIpAutorizada entity, CancellationToken ct = default)
    {
        var exists = await _db.MdSysIpAutorizadas.AnyAsync(x => x.IpCliente == entity.IpCliente, ct);
        if (exists)
            return (false, "La IPCLIENTE ya está registrada.");

        _db.MdSysIpAutorizadas.Add(entity);
        await _db.SaveChangesAsync(ct);
        return (true, null);
    }

    public async Task<(bool Success, string? Error)> UpdateAsync(string ipCliente, MdSysIpAutorizada entity, CancellationToken ct = default)
    {
        var existing = await GetByIpAsync(ipCliente, ct);
        if (existing == null)
            return (false, "Registro no encontrado.");

        existing.Descripcion = entity.Descripcion;
        existing.Estado = entity.Estado;
        await _db.SaveChangesAsync(ct);
        return (true, null);
    }

    public async Task<bool> DeleteAsync(string ipCliente, CancellationToken ct = default)
    {
        var existing = await GetByIpAsync(ipCliente, ct);
        if (existing == null) return false;
        _db.MdSysIpAutorizadas.Remove(existing);
        await _db.SaveChangesAsync(ct);
        return true;
    }
}
