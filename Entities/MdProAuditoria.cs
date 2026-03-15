using System.ComponentModel.DataAnnotations.Schema;

namespace VKS.Credimatic.API.Entities;

[Table("MD_PRO_AUDITORIA")]
public class MdProAuditoria
{
    [Column("FECHA")]
    public DateTime Fecha { get; set; }

    [Column("EMPRESA", TypeName = "varchar(10)")]
    public string? Empresa { get; set; }

    [Column("OFICINA", TypeName = "varchar(6)")]
    public string? Oficina { get; set; }

    [Column("USUARIO", TypeName = "varchar(15)")]
    public string? Usuario { get; set; }

    [Column("TABLA", TypeName = "varchar(30)")]
    public string? Tabla { get; set; }

    [Column("OPERACION", TypeName = "varchar(20)")]
    public string? Operacion { get; set; }

    [Column("CLAVE", TypeName = "varchar(128)")]
    public string? Clave { get; set; }

    [Column("DATOS", TypeName = "varchar(max)")]
    public string? Datos { get; set; }

    [Column("IP_CLIENTE", TypeName = "varchar(45)")]
    public string? IpCliente { get; set; }
}
