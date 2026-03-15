namespace VKS.Credimatic.API.Configuration;

/// <summary>
/// Configuración de conexión a base de datos. Modifique aquí cuando necesite cambiar servidor, usuario o base de datos.
/// </summary>
public class DatabaseSettings
{
    public const string SectionName = "ConnectionStrings";
    
    public string DefaultConnection { get; set; } = string.Empty;
}
