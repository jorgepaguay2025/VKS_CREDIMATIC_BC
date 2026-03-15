using Microsoft.EntityFrameworkCore;
using VKS.Credimatic.API.Data;
using VKS.Credimatic.API.Entities;

namespace VKS.Credimatic.API.Services;

public class AuditoriaService : IAuditoriaService
{
    private readonly AppDbContext _db;

    public AuditoriaService(AppDbContext db) => _db = db;

    public async Task<IEnumerable<MdProAuditoria>> ConsultarAsync(DateTime? fechaDesde, DateTime? fechaHasta, string? empresa, string? usuario, string? tabla, string? operacion, CancellationToken ct = default)
    {
        var sql = "SELECT FECHA, EMPRESA, OFICINA, USUARIO, TABLA, OPERACION, CLAVE, DATOS, IP_CLIENTE FROM MD_PRO_AUDITORIA WHERE 1=1";
        var parametros = new List<object>();
        var idx = 0;

        if (fechaDesde.HasValue)
        {
            sql += $" AND FECHA >= {{{idx}}}";
            parametros.Add(fechaDesde.Value);
            idx++;
        }
        if (fechaHasta.HasValue)
        {
            sql += $" AND FECHA <= {{{idx}}}";
            parametros.Add(fechaHasta.Value);
            idx++;
        }
        if (!string.IsNullOrWhiteSpace(empresa))
        {
            sql += $" AND EMPRESA = {{{idx}}}";
            parametros.Add(empresa.Trim());
            idx++;
        }
        if (!string.IsNullOrWhiteSpace(usuario))
        {
            sql += $" AND USUARIO = {{{idx}}}";
            parametros.Add(usuario.Trim());
            idx++;
        }
        if (!string.IsNullOrWhiteSpace(tabla))
        {
            sql += $" AND TABLA = {{{idx}}}";
            parametros.Add(tabla.Trim());
            idx++;
        }
        if (!string.IsNullOrWhiteSpace(operacion))
        {
            sql += $" AND OPERACION = {{{idx}}}";
            parametros.Add(operacion.Trim());
        }

        sql += " ORDER BY FECHA ASC";

        if (parametros.Count == 0)
            return await _db.MdProAuditorias.FromSqlRaw(sql).ToListAsync(ct);

        return await _db.MdProAuditorias.FromSqlRaw(sql, parametros.ToArray()).ToListAsync(ct);
    }

    public async Task<bool> RegistrarAsync(MdProAuditoria registro, CancellationToken ct = default)
    {
        var fecha = registro.Fecha == default ? DateTime.Now : registro.Fecha;

        await _db.Database.ExecuteSqlInterpolatedAsync(
            $"INSERT INTO MD_PRO_AUDITORIA (FECHA, EMPRESA, OFICINA, USUARIO, TABLA, OPERACION, CLAVE, DATOS, IP_CLIENTE) VALUES ({fecha}, {registro.Empresa}, {registro.Oficina}, {registro.Usuario}, {registro.Tabla}, {registro.Operacion}, {registro.Clave}, {registro.Datos}, {registro.IpCliente})",
            ct);

        return true;
    }
}
