using Microsoft.EntityFrameworkCore;
using VKS.Credimatic.API.Data;
using VKS.Credimatic.API.Entities;
using VKS.Credimatic.API.Models;

namespace VKS.Credimatic.API.Services;

public class SistemaService : ISistemaService
{
    private readonly AppDbContext _db;

    public SistemaService(AppDbContext db) => _db = db;

    public async Task<IEnumerable<MdSysSistemaDto>> GetAllAsync(CancellationToken ct = default)
    {
        var list = await _db.MdSysSistemas.AsNoTracking().ToListAsync(ct);
        return list.Select(ToDto);
    }

    public async Task<MdSysSistemaDto?> GetByKeyAsync(string producto, string empresa, CancellationToken ct = default)
    {
        var entity = await _db.MdSysSistemas.FindAsync(new object[] { producto, empresa }, ct);
        return entity == null ? null : ToDto(entity);
    }

    public async Task<(bool Success, string? Error)> CreateAsync(MdSysSistemaRequestDto dto, CancellationToken ct = default)
    {
        var exists = await _db.MdSysSistemas.AnyAsync(
            x => x.Producto == dto.Producto && x.Empresa == dto.Empresa, ct);
        if (exists)
            return (false, "Ya existe un registro con el mismo Producto y Empresa.");

        var (entity, error) = RequestToEntity(dto);
        if (error != null) return (false, error);

        _db.MdSysSistemas.Add(entity);
        await _db.SaveChangesAsync(ct);
        return (true, null);
    }

    public async Task<(bool Success, string? Error)> UpdateAsync(string producto, string empresa, MdSysSistemaRequestDto dto, CancellationToken ct = default)
    {
        var existing = await _db.MdSysSistemas.FindAsync(new object[] { producto, empresa }, ct);
        if (existing == null)
            return (false, "Registro no encontrado.");

        var (entity, error) = RequestToEntity(dto);
        if (error != null) return (false, error);

        existing.DbUser = entity.DbUser;
        existing.DbPassword = entity.DbPassword;
        existing.DbServerName = entity.DbServerName;
        existing.DbServerIp = entity.DbServerIp;
        existing.DbServerPort = entity.DbServerPort;
        existing.ClaveReintentos = entity.ClaveReintentos;
        existing.ClaveVigencia = entity.ClaveVigencia;
        existing.UnidadAlmacenamiento = entity.UnidadAlmacenamiento;
        existing.TiempoAlmacenamiento = entity.TiempoAlmacenamiento;
        existing.TiempoBbAct = entity.TiempoBbAct;
        existing.TiempoBbHist = entity.TiempoBbHist;
        existing.TiempoDepuracion = entity.TiempoDepuracion;
        existing.Ruta = entity.Ruta;
        await _db.SaveChangesAsync(ct);
        return (true, null);
    }

    public async Task<bool> DeleteAsync(string producto, string empresa, CancellationToken ct = default)
    {
        var existing = await _db.MdSysSistemas.FindAsync(new object[] { producto, empresa }, ct);
        if (existing == null) return false;
        _db.MdSysSistemas.Remove(existing);
        await _db.SaveChangesAsync(ct);
        return true;
    }

    private static MdSysSistemaDto ToDto(MdSysSistema e)
    {
        return new MdSysSistemaDto
        {
            Producto = e.Producto,
            Empresa = e.Empresa,
            DbUser = SistemaHexHelper.BytesToAscii(e.DbUser),
            DbPassword = SistemaHexHelper.BytesToAscii(e.DbPassword),
            DbServerName = SistemaHexHelper.BytesToAscii(e.DbServerName),
            DbServerIp = SistemaHexHelper.BytesToAscii(e.DbServerIp),
            DbServerPort = SistemaHexHelper.BytesToAscii(e.DbServerPort),
            ClaveReintentos = e.ClaveReintentos,
            ClaveVigencia = e.ClaveVigencia,
            UnidadAlmacenamiento = e.UnidadAlmacenamiento,
            TiempoAlmacenamiento = e.TiempoAlmacenamiento,
            TiempoBbAct = e.TiempoBbAct,
            TiempoBbHist = e.TiempoBbHist,
            TiempoDepuracion = e.TiempoDepuracion,
            Ruta = e.Ruta
        };
    }

    private static (MdSysSistema Entity, string? Error) RequestToEntity(MdSysSistemaRequestDto dto)
    {
        // Al registrar/actualizar el cliente envía texto ASCII (ej: "sa", "1235"); se convierte a binary para la BD.
        var dbUser = SistemaHexHelper.AsciiStringToBytes(dto.DbUser);
        var dbPassword = SistemaHexHelper.AsciiStringToBytes(dto.DbPassword);
        var dbServerName = SistemaHexHelper.AsciiStringToBytes(dto.DbServerName);
        var dbServerIp = SistemaHexHelper.AsciiStringToBytes(dto.DbServerIp);
        var dbServerPort = SistemaHexHelper.AsciiStringToBytes(dto.DbServerPort);

        return (new MdSysSistema
        {
            Producto = dto.Producto,
            Empresa = dto.Empresa,
            DbUser = dbUser,
            DbPassword = dbPassword,
            DbServerName = dbServerName,
            DbServerIp = dbServerIp,
            DbServerPort = dbServerPort,
            ClaveReintentos = dto.ClaveReintentos,
            ClaveVigencia = dto.ClaveVigencia,
            UnidadAlmacenamiento = dto.UnidadAlmacenamiento,
            TiempoAlmacenamiento = dto.TiempoAlmacenamiento,
            TiempoBbAct = dto.TiempoBbAct,
            TiempoBbHist = dto.TiempoBbHist,
            TiempoDepuracion = dto.TiempoDepuracion,
            Ruta = dto.Ruta
        }, null);
    }
}
