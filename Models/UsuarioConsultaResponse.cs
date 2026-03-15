namespace VKS.Credimatic.API.Models;

/// <summary>
/// Datos del usuario para consulta (sin Clave).
/// </summary>
public class UsuarioConsultaResponse
{
    public string Empresa { get; set; } = null!;
    public string Usuario { get; set; } = null!;
    public string? Nombres { get; set; }
    public string? Apellidos { get; set; }
    public string? Identificacion { get; set; }
    public string? Oficina { get; set; }
    public string? Departamento { get; set; }
    public string? Area { get; set; }
    public string? Cargo { get; set; }
    public string? Telefono { get; set; }
    public string? PerfilTransaccional { get; set; }
    public string? PerfilDocumental { get; set; }
    public string? FechaCreacion { get; set; }
    public string? FechaClave { get; set; }
    public string? FechaExpiracion { get; set; }
    public string? Permanencia { get; set; }
    public string? Motivo { get; set; }
    public string? Estado { get; set; }
    public short? Reintentos { get; set; }
}
