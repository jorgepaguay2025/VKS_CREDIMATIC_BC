using System.ComponentModel.DataAnnotations.Schema;

namespace VKS.Credimatic.API.Entities;

[Table("MD_SYS_CATALOGOS_SISTEMA")]
public class MdSysCatalogoSistema
{
    [Column("Catalogo", TypeName = "nvarchar(6)")]
    public string Catalogo { get; set; } = null!;

    [Column("Item", TypeName = "nvarchar(6)")]
    public string Item { get; set; } = null!;

    [Column("Descripcion", TypeName = "nvarchar(50)")]
    public string? Descripcion { get; set; }

    [Column("Estado", TypeName = "char(1)")]
    public string? Estado { get; set; }
}
