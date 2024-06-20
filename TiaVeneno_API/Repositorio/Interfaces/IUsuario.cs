using GamarraPlus.Models;

namespace TiaVeneno_API.Repositorio.Interfaces
{
    public interface IUsuario
    {
        IEnumerable<Usuario> obtenerUsuarios();
        Usuario ObtenerUsuarioPorId(int id);
        string RegistrarUsuario(Usuario reg);
        string ActualizarUsuario(Usuario reg);
        bool EliminarUsuario(int id);
    }
}
