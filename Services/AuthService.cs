using System.Text;
using Microsoft.EntityFrameworkCore;
using VKS.Credimatic.API.Data;
using VKS.Credimatic.API.Models;

namespace VKS.Credimatic.API.Services;

public class AuthService : IAuthService
{
    private const int ClaveLength = 10;

    private readonly AppDbContext _db;
    private readonly IJwtService _jwtService;

    public AuthService(AppDbContext db, IJwtService jwtService)
    {
        _db = db;
        _jwtService = jwtService;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var usuario = await _db.MdSysUsuarios
            .FirstOrDefaultAsync(u => u.Usuario == request.Usuario, cancellationToken);

        if (usuario == null)
            return new LoginResponse { Success = false, Message = "Usuario o clave incorrectos." };

        if (usuario.Estado != "A")
            return new LoginResponse { Success = false, Message = "Usuario inactivo." };

        // [Clave] = texto en ASCII de la contraseña + relleno con bytes nulos (\0) hasta 10 bytes
        // Ej: "1234" -> 0x31323334000000000000
        var claveIngresada = ClaveToStoredFormat(request.Clave);
        if (usuario.Clave == null || usuario.Clave.Length < ClaveLength || !claveIngresada.AsSpan().SequenceEqual(usuario.Clave.AsSpan(0, ClaveLength)))
            return new LoginResponse { Success = false, Message = "Usuario o clave incorrectos." };

        var token = _jwtService.GenerateToken(usuario.Usuario, usuario.Empresa);
        return new LoginResponse { Success = true, Token = token };
    }

    /// <summary>
    /// Convierte la clave digitada al formato almacenado en [Clave]: bytes ASCII + relleno con \0 hasta 10 bytes.
    /// Ej: "1234" -> 0x31 0x32 0x33 0x34 0x00 0x00 0x00 0x00 0x00 0x00
    /// </summary>
    private static byte[] ClaveToStoredFormat(string clave)
    {
        var bytes = Encoding.ASCII.GetBytes(clave ?? string.Empty);
        var result = new byte[ClaveLength];
        var copyLen = Math.Min(bytes.Length, ClaveLength);
        Array.Copy(bytes, result, copyLen);
        // El resto ya son 0x00 por defecto
        return result;
    }
}
