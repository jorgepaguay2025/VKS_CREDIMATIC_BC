using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using VKS.Credimatic.API.Configuration;
using VKS.Credimatic.API.Data;
using VKS.Credimatic.API.Middleware;
using VKS.Credimatic.API.Services;
using VKS.Credimatic.API.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Conexión a base de datos: cambiar en appsettings.json (ConnectionStrings:DefaultConnection) cuando se requiera
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("No se configuró 'ConnectionStrings:DefaultConnection'.");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// JWT: clave secreta y horas de expiración en appsettings.json (sección Jwt)
var jwtSection = builder.Configuration.GetSection(JwtSettings.SectionName);
builder.Services.Configure<JwtSettings>(jwtSection);
var jwtSettings = jwtSection.Get<JwtSettings>() ?? new JwtSettings();
builder.Services.AddSingleton(jwtSettings);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
            ClockSkew = TimeSpan.Zero
        };
        // Aceptar JWT con "Bearer <token>" o solo "<token>" en el header Authorization
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                if (!string.IsNullOrEmpty(authHeader))
                {
                    if (authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                        context.Token = authHeader["Bearer ".Length..].Trim();
                    else
                        context.Token = authHeader.Trim();
                }
                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IOficinasService, OficinasService>();
builder.Services.AddScoped<IDepartamentosService, DepartamentosService>();
builder.Services.AddScoped<IAreasService, AreasService>();
builder.Services.AddScoped<IMenusService, MenusService>();
builder.Services.AddScoped<ITransaccionesService, TransaccionesService>();
builder.Services.AddScoped<IDescripcionEventosService, DescripcionEventosService>();
builder.Services.AddScoped<IIpAutorizadasService, IpAutorizadasService>();
builder.Services.AddScoped<IUsuariosService, UsuariosService>();
builder.Services.AddScoped<ICatalogosService, CatalogosService>();
builder.Services.AddScoped<ISistemaService, SistemaService>();
builder.Services.AddScoped<IAuditoriaService, AuditoriaService>();
builder.Services.AddScoped<IEmpresasService, EmpresasService>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "VKS Credimatic API", Version = "v1" });
    c.OperationFilter<AdministracionTagFilter>();
    c.DocumentFilter<AdministracionTagDocumentFilter>();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT. Ejemplo: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Manejo global de excepciones (SqlException, etc.): respuestas controladas sin exponer detalles
app.UseMiddleware<ExceptionHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
