using GamarraPlus.Models;
using System.Collections.Generic;

namespace TiaVeneno_API.Repositorio.Interfaces
{
    public interface IProducto
    {
        IEnumerable<Producto> ObtenerProductos();
        Producto ObtenerProductoPorId(int id);
        string RegistrarProducto(Producto reg);
        string ActualizarProducto(Producto reg);
        bool EliminarProducto(int id);
    }
}
