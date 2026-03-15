using System.ComponentModel.DataAnnotations.Schema;

namespace VKS.Credimatic.API.Entities;

[Table("MD_SYS_USUARIOS")]
public class MdSysUsuario
{
    [Column("Empresa", TypeName = "varchar(6)")]
    public string Empresa { get; set; } = null!;

    [Column("Usuario", TypeName = "varchar(15)")]
    public string Usuario { get; set; } = null!;

    [Column("Nombres", TypeName = "varchar(30)")]
    public string? Nombres { get; set; }

    [Column("Apellidos", TypeName = "varchar(30)")]
    public string? Apellidos { get; set; }

    [Column("Identificacion", TypeName = "varchar(15)")]
    public string? Identificacion { get; set; }

    [Column("Oficina", TypeName = "varchar(6)")]
    public string? Oficina { get; set; }

    [Column("Departamento", TypeName = "varchar(6)")]
    public string? Departamento { get; set; }

    [Column("Area", TypeName = "varchar(6)")]
    public string? Area { get; set; }

    [Column("Cargo", TypeName = "varchar(6)")]
    public string? Cargo { get; set; }

    [Column("Telefono", TypeName = "varchar(15)")]
    public string? Telefono { get; set; }

    [Column("Clave", TypeName = "binary(10)")]
    public byte[]? Clave { get; set; }

    [Column("Perfil_Transaccional", TypeName = "varchar(6)")]
    public string? PerfilTransaccional { get; set; }

    [Column("Perfil_Documental", TypeName = "varchar(6)")]
    public string? PerfilDocumental { get; set; }

    [Column("Fecha_Creacion", TypeName = "varchar(8)")]
    public string? FechaCreacion { get; set; }

    [Column("Fecha_Clave", TypeName = "varchar(8)")]
    public string? FechaClave { get; set; }

    [Column("Fecha_Expiracion", TypeName = "varchar(8)")]
    public string? FechaExpiracion { get; set; }

    [Column("Permanencia", TypeName = "char(1)")]
    public string? Permanencia { get; set; }

    [Column("Motivo", TypeName = "char(2)")]
    public string? Motivo { get; set; }

    [Column("Estado", TypeName = "char(1)")]
    public string? Estado { get; set; }

    [Column("Reintentos")]
    public short? Reintentos { get; set; }
}
