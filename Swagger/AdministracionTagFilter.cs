using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace VKS.Credimatic.API.Swagger;

/// <summary>
/// Agrupa bajo el tag "Administración" los controladores: Oficinas, Departamentos, Areas,
/// Transacciones, Menus, Catalogos, IpAutorizadas, DescripcionEventos.
/// </summary>
public class AdministracionTagFilter : IOperationFilter
{
    private static readonly HashSet<string> AdministracionControllers = new(StringComparer.OrdinalIgnoreCase)
    {
        "Oficinas",
        "Departamentos",
        "Areas",
        "Transacciones",
        "Menus",
        "Catalogos",
        "IpAutorizadas",
        "DescripcionEventos",
        "Sistema"
    };

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.ApiDescription.RelativePath == null) return;

        var controllerName = context.ApiDescription.ActionDescriptor.RouteValues["controller"];
        if (controllerName != null && AdministracionControllers.Contains(controllerName))
        {
            operation.Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Administración" } };
        }
    }
}
