using System.ComponentModel.DataAnnotations.Schema;

namespace VKS.Credimatic.API.Entities;

[Table("MD_SYS_AREAS")]
public class MdSysArea
{
    [Column("Empresa", TypeName = "varchar(6)")]
    public string Empresa { get; set; } = null!;

    [Column("Area", TypeName = "varchar(6)")]
    public string Area { get; set; } = null!;

    [Column("Departamento", TypeName = "varchar(6)")]
    public string? Departamento { get; set; }

    [Column("Nombre", TypeName = "varchar(30)")]
    public string? Nombre { get; set; }

    [Column("Estado", TypeName = "char(1)")]
    public string? Estado { get; set; }
}
