using System.ComponentModel.DataAnnotations.Schema;

namespace VKS.Credimatic.API.Entities;

[Table("MD_SYS_DESCRIPCION_EVENTOS")]
public class MdSysDescripcionEvento
{
    [Column("CODIGO_DE_EVENTO", TypeName = "varchar(4)")]
    public string CodigoDeEvento { get; set; } = null!;

    [Column("DESCRIPCION", TypeName = "varchar(60)")]
    public string? Descripcion { get; set; }

    [Column("DESC_INGLES", TypeName = "varchar(60)")]
    public string? DescIngles { get; set; }

    [Column("SEVERIDAD", TypeName = "varchar(2)")]
    public string? Severidad { get; set; }

    [Column("REPORTA_ALERTA", TypeName = "char(1)")]
    public string? ReportaAlerta { get; set; }

    [Column("EMAIL_1", TypeName = "varchar(64)")]
    public string? Email1 { get; set; }

    [Column("EMAIL_2", TypeName = "varchar(64)")]
    public string? Email2 { get; set; }

    [Column("EMAIL_3", TypeName = "varchar(64)")]
    public string? Email3 { get; set; }

    [Column("CAUSAL", TypeName = "varchar(512)")]
    public string? Causal { get; set; }

    [Column("LLAMAR_RESPONSABLE", TypeName = "varchar(6)")]
    public string? LlamarResponsable { get; set; }
}
