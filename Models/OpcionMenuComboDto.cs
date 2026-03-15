namespace VKS.Credimatic.API.Models;

/// <summary>
/// Opción para el combo de menús (filtrar transacciones por Menu).
/// </summary>
public class OpcionMenuComboDto
{
    public string Producto { get; set; } = null!;
    public string Menu { get; set; } = null!;
    public string? Nombre { get; set; }
}
