using System.Text;

namespace VKS.Credimatic.API.Services;

/// <summary>
/// Convierte entre texto ASCII y byte[] para los campos binarios de MD_SYS_SISTEMA.
/// Al consultar: byte[] → ASCII. Al registrar/actualizar: ASCII (string) → byte[] (se guarda como binary en BD).
/// </summary>
public static class SistemaHexHelper
{
    private const int BinaryFieldLength = 20;

    /// <summary>
    /// Convierte texto ASCII en byte[] de 20 bytes (relleno con 0). Para registrar/actualizar: el cliente envía "sa", se guarda como binary.
    /// </summary>
    public static byte[]? AsciiStringToBytes(string? value)
    {
        if (value == null) return null;
        var bytes = Encoding.ASCII.GetBytes(value);
        var result = new byte[BinaryFieldLength];
        var copyLen = Math.Min(bytes.Length, BinaryFieldLength);
        Array.Copy(bytes, result, copyLen);
        return result;
    }

    /// <summary>
    /// Convierte una cadena hexadecimal (ej: "0x7361..." o "73610000...") en byte[] de máximo 20 bytes.
    /// </summary>
    public static byte[]? HexStringToBytes(string? hex)
    {
        if (string.IsNullOrWhiteSpace(hex)) return null;

        hex = hex.Trim();
        if (hex.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            hex = hex[2..];

        if (hex.Length == 0) return null;

        var bytes = new List<byte>();
        for (var i = 0; i < hex.Length; i += 2)
        {
            if (i + 1 >= hex.Length) break;
            if (byte.TryParse(hex.AsSpan(i, 2), System.Globalization.NumberStyles.HexNumber, null, out var b))
                bytes.Add(b);
        }

        if (bytes.Count == 0) return null;

        var result = new byte[BinaryFieldLength];
        var copyLen = Math.Min(bytes.Count, BinaryFieldLength);
        for (var i = 0; i < copyLen; i++)
            result[i] = bytes[i];
        return result;
    }

    /// <summary>
    /// Convierte byte[] a string ASCII (eliminando bytes nulos al final).
    /// </summary>
    public static string? BytesToAscii(byte[]? bytes)
    {
        if (bytes == null || bytes.Length == 0) return null;

        var len = bytes.Length;
        while (len > 0 && bytes[len - 1] == 0)
            len--;
        if (len == 0) return string.Empty;

        return Encoding.ASCII.GetString(bytes, 0, len);
    }
}
