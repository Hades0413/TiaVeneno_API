

using GamarraPlus.Models;

namespace TiaVeneno_API.Repositorio.Interfaces
{
    public interface IVenta
    {
        string RegistrarVenta(Venta ventaXml);

        string VerDetalleVenta(string numeroDocumento);

    }
}
