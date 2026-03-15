# VKS Credimatic API

API REST en .NET 7 con Swagger, JWT y CRUD para tablas de VKS_CREDIMATIC_MEJORAS.

## Configuración

### Base de datos (un solo lugar)

Edite **`appsettings.json`** (o `appsettings.Development.json`) para cambiar servidor, usuario, contraseña o base de datos:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=VKS_CREDIMATIC_MEJORAS;User Id=sa;Password=123456;TrustServerCertificate=True;Encrypt=False;"
}
```

### JWT (clave y tiempo de vida)

En la misma sección de configuración:

```json
"Jwt": {
  "SecretKey": "VKS_Credimatic_ClaveSecreta_JWT_2024_Minimo32Caracteres",
  "Issuer": "VKS.Credimatic.API",
  "Audience": "VKS.Credimatic.Client",
  "ExpirationHours": 8
}
```

- **SecretKey**: Clave secreta para firmar el JWT (mínimo 32 caracteres). Cámbiela en producción.
- **ExpirationHours**: Horas de validez del token (por defecto 8).

## Ejecución

```bash
dotnet run
```

Swagger: `https://localhost:5xxx/swagger` (puerto según `launchSettings.json`).

## Autenticación

1. **Login** (no requiere JWT): `POST /api/Auth/login`  
   Cuerpo JSON:
   ```json
   { "usuario": "NOMBRE_USUARIO", "clave": "CONTRASEÑA" }
   ```
   Respuesta: `{ "success": true, "token": "eyJ..." }`

2. **Resto de endpoints**: envíe el JWT en el header:
   ```
   Authorization: Bearer <token>
   ```

## Endpoints protegidos con JWT

- **Oficinas**: `GET/POST/PUT/DELETE /api/Oficinas` — CRUD MD_SYS_OFICINAS (clave única: Empresa, Tipo_Oficina, Oficina).
- **Departamentos**: `GET/POST/PUT/DELETE /api/Departamentos` — CRUD MD_SYS_DEPARTAMENTOS (clave única: Empresa, Departamento).
- **IP Autorizadas**: `GET/POST/PUT/DELETE /api/IpAutorizadas` — CRUD MD_SYS_IP_AUTORIZADAS (IPCLIENTE única).

## Clave de usuarios (MD_SYS_USUARIOS)

El campo [Clave] (binary(10)) guarda la contraseña en **ASCII + relleno con bytes nulos** hasta 10 bytes. Ejemplo: si el usuario digita `1234`, en BD se almacena `0x31323334000000000000` (ASCII "1234" + 6 bytes `\0`). El login convierte la clave ingresada a ese formato y la compara con el valor de la tabla.
