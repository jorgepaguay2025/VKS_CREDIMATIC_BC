using System.ComponentModel.DataAnnotations.Schema;

namespace VKS.Credimatic.API.Entities;

[Table("MD_SYS_TRANSACCIONES")]
public class MdSysTransaccion
{
    [Column("Producto", TypeName = "varchar(6)")]
    public string Producto { get; set; } = null!;

    [Column("Menu", TypeName = "varchar(6)")]
    public string Menu { get; set; } = null!;

    [Column("Transaccion", TypeName = "varchar(6)")]
    public string Transaccion { get; set; } = null!;

    [Column("Nombre", TypeName = "nvarchar(30)")]
    public string? Nombre { get; set; }

    [Column("Descripcion", TypeName = "varchar(50)")]
    public string? Descripcion { get; set; }

    [Column("Etiqueta", TypeName = "varchar(20)")]
    public string? Etiqueta { get; set; }

    [Column("Secuencia_Menu")]
    public short? SecuenciaMenu { get; set; }

    [Column("Programa", TypeName = "varchar(30)")]
    public string? Programa { get; set; }

    [Column("Estado", TypeName = "char(1)")]
    public string? Estado { get; set; }
}
