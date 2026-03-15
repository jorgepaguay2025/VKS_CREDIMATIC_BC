using System.ComponentModel.DataAnnotations.Schema;

namespace VKS.Credimatic.API.Entities;

[Table("MD_SYS_IP_AUTORIZADAS")]
public class MdSysIpAutorizada
{
    [Column("IPCLIENTE", TypeName = "varchar(20)")]
    public string IpCliente { get; set; } = null!;

    [Column("DESCRIPCION", TypeName = "varchar(40)")]
    public string? Descripcion { get; set; }

    [Column("ESTADO", TypeName = "varchar(1)")]
    public string? Estado { get; set; }
}
