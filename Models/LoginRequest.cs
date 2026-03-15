using System.ComponentModel.DataAnnotations;

namespace VKS.Credimatic.API.Models;

public class LoginRequest
{
    [Required]
    public string Usuario { get; set; } = null!;

    [Required]
    public string Clave { get; set; } = null!;
}
