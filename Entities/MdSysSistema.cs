using System.ComponentModel.DataAnnotations.Schema;

namespace VKS.Credimatic.API.Entities;

[Table("MD_SYS_SISTEMA")]
public class MdSysSistema
{
    [Column("Producto", TypeName = "varchar(6)")]
    public string Producto { get; set; } = null!;

    [Column("Empresa", TypeName = "varchar(6)")]
    public string Empresa { get; set; } = null!;

    [Column("DB_User", TypeName = "binary(20)")]
    public byte[]? DbUser { get; set; }

    [Column("DB_Password", TypeName = "binary(20)")]
    public byte[]? DbPassword { get; set; }

    [Column("DB_Server_Name", TypeName = "binary(20)")]
    public byte[]? DbServerName { get; set; }

    [Column("DB_Server_IP", TypeName = "binary(20)")]
    public byte[]? DbServerIp { get; set; }

    [Column("Db_Server_Port", TypeName = "binary(20)")]
    public byte[]? DbServerPort { get; set; }

    [Column("Clave_Reintentos")]
    public short? ClaveReintentos { get; set; }

    [Column("Clave_Vigencia")]
    public short? ClaveVigencia { get; set; }

    [Column("Unidad_Almacenamiento", TypeName = "nvarchar(50)")]
    public string? UnidadAlmacenamiento { get; set; }

    [Column("Tiempo_almacenamiento")]
    public short? TiempoAlmacenamiento { get; set; }

    [Column("Tiempo_BB_Act")]
    public short? TiempoBbAct { get; set; }

    [Column("Tiempo_BB_Hist")]
    public short? TiempoBbHist { get; set; }

    [Column("Tiempo_Depuracion")]
    public short? TiempoDepuracion { get; set; }

    [Column("Ruta", TypeName = "varchar(30)")]
    public string? Ruta { get; set; }
}
