using VKS.Credimatic.API.Models;

namespace VKS.Credimatic.API.Services;

public interface IUsuariosService
{
    Task<UsuarioConsultaResponse?> GetByEmpresaAndUsuarioAsync(string empresa, string usuario, CancellationToken ct = default);
}
