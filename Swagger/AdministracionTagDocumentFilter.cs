using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace VKS.Credimatic.API.Swagger;

/// <summary>
/// Define el tag "Administración" en el documento OpenAPI (Oficinas, Departamentos, Areas, Transacciones, Menus, Catalogos, IP Autorizadas, DescripcionEventos).
/// </summary>
public class AdministracionTagDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        swaggerDoc.Tags ??= new List<OpenApiTag>();

        if (swaggerDoc.Tags.All(t => t.Name != "Administración"))
        {
            swaggerDoc.Tags.Insert(0, new OpenApiTag
            {
                Name = "Administración",
                Description = "Oficinas, Departamentos, Areas, Transacciones, Menus, Catalogos, IP Autorizadas, DescripcionEventos, Sistema"
            });
        }
    }
}
