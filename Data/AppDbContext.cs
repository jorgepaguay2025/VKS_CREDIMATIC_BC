using Microsoft.EntityFrameworkCore;
using VKS.Credimatic.API.Entities;

namespace VKS.Credimatic.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<MdSysOficina> MdSysOficinas => Set<MdSysOficina>();
    public DbSet<MdSysUsuario> MdSysUsuarios => Set<MdSysUsuario>();
    public DbSet<MdSysDepartamento> MdSysDepartamentos => Set<MdSysDepartamento>();
    public DbSet<MdSysIpAutorizada> MdSysIpAutorizadas => Set<MdSysIpAutorizada>();
    public DbSet<MdSysCatalogoSistema> MdSysCatalogosSistema => Set<MdSysCatalogoSistema>();
    public DbSet<MdSysArea> MdSysAreas => Set<MdSysArea>();
    public DbSet<MdSysMenu> MdSysMenus => Set<MdSysMenu>();
    public DbSet<MdSysTransaccion> MdSysTransacciones => Set<MdSysTransaccion>();
    public DbSet<MdSysDescripcionEvento> MdSysDescripcionEventos => Set<MdSysDescripcionEvento>();
    public DbSet<MdProAuditoria> MdProAuditorias => Set<MdProAuditoria>();
    public DbSet<MdSysSistema> MdSysSistemas => Set<MdSysSistema>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MdSysOficina>(e =>
        {
            e.HasKey(x => new { x.Empresa, x.TipoOficina, x.Oficina });
        });

        modelBuilder.Entity<MdSysUsuario>(e =>
        {
            e.HasKey(x => new { x.Empresa, x.Usuario });
        });

        modelBuilder.Entity<MdSysDepartamento>(e =>
        {
            e.HasKey(x => new { x.Empresa, x.Departamento });
        });

        modelBuilder.Entity<MdSysIpAutorizada>(e =>
        {
            e.HasKey(x => x.IpCliente);
        });

        modelBuilder.Entity<MdSysCatalogoSistema>(e =>
        {
            e.HasKey(x => new { x.Catalogo, x.Item });
        });

        modelBuilder.Entity<MdSysArea>(e =>
        {
            e.HasKey(x => new { x.Empresa, x.Area });
        });

        modelBuilder.Entity<MdSysMenu>(e =>
        {
            e.HasKey(x => new { x.Producto, x.Menu });
        });

        modelBuilder.Entity<MdSysTransaccion>(e =>
        {
            e.HasKey(x => new { x.Producto, x.Menu, x.Transaccion });
        });

        modelBuilder.Entity<MdSysDescripcionEvento>(e =>
        {
            e.HasKey(x => x.CodigoDeEvento);
        });

        modelBuilder.Entity<MdProAuditoria>(e =>
        {
            e.HasNoKey();
        });

        modelBuilder.Entity<MdSysSistema>(e =>
        {
            e.HasKey(x => new { x.Producto, x.Empresa });
        });
    }
}
