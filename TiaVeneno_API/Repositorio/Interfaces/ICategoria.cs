using GamarraPlus.Models;
using System.Collections.Generic;

namespace TiaVeneno_API.Repositorio.Interfaces
{
    public interface ICategoria
    {
        IEnumerable<Categoria> ObtenerCategorias();
        Categoria ObtenerCategoriaPorId(int id);
        bool RegistrarCategoria(Categoria obj);
        bool ActualizarCategoria(Categoria obj);
        bool EliminarCategoria(int idCategoria);
    }
}
