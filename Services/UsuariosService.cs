using Microsoft.EntityFrameworkCore;
using VKS.Credimatic.API.Data;
using VKS.Credimatic.API.Models;

namespace VKS.Credimatic.API.Services;

public class UsuariosService : IUsuariosService
{
    private readonly AppDbContext _db;

    public UsuariosService(AppDbContext db) => _db = db;

    public async Task<UsuarioConsultaResponse?> GetByEmpresaAndUsuarioAsync(string empresa, string usuario, CancellationToken ct = default)
    {
        var entity = await _db.MdSysUsuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Usuario == usuario && u.Empresa == empresa, ct);

        if (entity == null) return null;

        return new UsuarioConsultaResponse
        {
            Empresa = entity.Empresa,
            Usuario = entity.Usuario,
            Nombres = entity.Nombres,
            Apellidos = entity.Apellidos,
            Identificacion = entity.Identificacion,
            Oficina = entity.Oficina,
            Departamento = entity.Departamento,
            Area = entity.Area,
            Cargo = entity.Cargo,
            Telefono = entity.Telefono,
            PerfilTransaccional = entity.PerfilTransaccional,
            PerfilDocumental = entity.PerfilDocumental,
            FechaCreacion = entity.FechaCreacion,
            FechaClave = entity.FechaClave,
            FechaExpiracion = entity.FechaExpiracion,
            Permanencia = entity.Permanencia,
            Motivo = entity.Motivo,
            Estado = entity.Estado,
            Reintentos = entity.Reintentos
        };
    }
}
