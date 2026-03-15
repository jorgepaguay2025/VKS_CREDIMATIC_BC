namespace VKS.Credimatic.API.Models;

/// <summary>
/// Respuesta/consulta de MD_SYS_SISTEMA. Los campos binarios (DB_User, DB_Password, etc.) se devuelven en ASCII.
/// </summary>
public class MdSysSistemaDto
{
    public string Producto { get; set; } = null!;
    public string Empresa { get; set; } = null!;
    public string? DbUser { get; set; }
    public string? DbPassword { get; set; }
    public string? DbServerName { get; set; }
    public string? DbServerIp { get; set; }
    public string? DbServerPort { get; set; }
    public short? ClaveReintentos { get; set; }
    public short? ClaveVigencia { get; set; }
    public string? UnidadAlmacenamiento { get; set; }
    public short? TiempoAlmacenamiento { get; set; }
    public short? TiempoBbAct { get; set; }
    public short? TiempoBbHist { get; set; }
    public short? TiempoDepuracion { get; set; }
    public string? Ruta { get; set; }
}

/// <summary>
/// Request para crear/actualizar. DB_User, DB_Password, DB_Server_Name, DB_Server_IP, Db_Server_Port se envían en texto (ASCII) y se convierten a binary en BD.
/// </summary>
public class MdSysSistemaRequestDto
{
    public string Producto { get; set; } = null!;
    public string Empresa { get; set; } = null!;
    public string? DbUser { get; set; }
    public string? DbPassword { get; set; }
    public string? DbServerName { get; set; }
    public string? DbServerIp { get; set; }
    public string? DbServerPort { get; set; }
    public short? ClaveReintentos { get; set; }
    public short? ClaveVigencia { get; set; }
    public string? UnidadAlmacenamiento { get; set; }
    public short? TiempoAlmacenamiento { get; set; }
    public short? TiempoBbAct { get; set; }
    public short? TiempoBbHist { get; set; }
    public short? TiempoDepuracion { get; set; }
    public string? Ruta { get; set; }
}
