using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VKS.Credimatic.API.Entities;

[Table("MD_SYS_OFICINAS")]
public class MdSysOficina
{
    [Column("Empresa", TypeName = "varchar(6)")]
    [MaxLength(6)]
    public string Empresa { get; set; } = null!;

    [Column("Tipo_Oficina", TypeName = "char(2)")]
    [MaxLength(2)]
    public string TipoOficina { get; set; } = null!;

    [Column("Oficina", TypeName = "varchar(6)")]
    [MaxLength(6)]
    public string Oficina { get; set; } = null!;

    [Column("Nombre", TypeName = "varchar(30)")]
    [MaxLength(30)]
    public string? Nombre { get; set; }

    [Column("Direccion", TypeName = "varchar(50)")]
    [MaxLength(50)]
    public string? Direccion { get; set; }

    [Column("Estado", TypeName = "char(1)")]
    [MaxLength(1)]
    public string? Estado { get; set; }

    [Column("Tiempo_Depuracion")]
    public short? TiempoDepuracion { get; set; }

    [Column("Ruta_Almacenamiento", TypeName = "varchar(50)")]
    [MaxLength(50)]
    public string? RutaAlmacenamiento { get; set; }

    [Column("Capacidad_unidad")]
    public short? CapacidadUnidad { get; set; }
}
