using System.ComponentModel.DataAnnotations.Schema;

namespace VKS.Credimatic.API.Entities;

[Table("MD_SYS_DEPARTAMENTOS")]
public class MdSysDepartamento
{
    [Column("Empresa", TypeName = "varchar(6)")]
    public string Empresa { get; set; } = null!;

    [Column("Departamento", TypeName = "varchar(6)")]
    public string Departamento { get; set; } = null!;

    [Column("Nombre", TypeName = "varchar(30)")]
    public string? Nombre { get; set; }

    [Column("Estado", TypeName = "char(1)")]
    public string? Estado { get; set; }
}
