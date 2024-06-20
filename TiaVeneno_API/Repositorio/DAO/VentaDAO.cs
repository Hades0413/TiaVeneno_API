using GamarraPlus.Datos;
using GamarraPlus.Models;
using TiaVeneno_API.Repositorio.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using System.Xml.Serialization;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using System.Text;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using System.Globalization;

namespace TiaVeneno_API.Repositorio.DAO
{
    public class VentaDAO : IVenta
    {

        public string RegistrarVenta(Venta venta)
        {
            var cn = new Conexion();
            string mensaje;
            string nroDocumento = string.Empty;

            using (SqlConnection connection = new SqlConnection(cn.getCadenaSQL()))
            {
                using (SqlCommand command = new SqlCommand("sp_registrar_venta", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    
                    XElement ventaXml = new XElement("Venta",
                        new XElement("TipoPago", venta.TipoPago),
                        new XElement("DocumentoCliente", venta.DocumentoCliente),
                        new XElement("NombreCliente", venta.NombreCliente),
                        new XElement("MontoPagoCon", venta.MontoPagoCon),
                        new XElement("MontoCambio", venta.MontoCambio),
                        new XElement("MontoSubTotal", venta.MontoSubTotal),
                        new XElement("MontoIGV", venta.MontoIGV),
                        new XElement("MontoTotal", venta.MontoTotal),
                        new XElement("Detalle_Venta",
                            from d in venta.oDetalleVenta
                            select new XElement("Item",
                                new XElement("IdProducto", d.oProducto.IdProducto),
                                new XElement("PrecioVenta", d.PrecioVenta),
                                new XElement("Cantidad", d.Cantidad),
                                new XElement("Total", d.Total)
                            )
                        )
                    );

                    SqlParameter xmlParameter = new SqlParameter("@Venta_xml", SqlDbType.Xml);
                    xmlParameter.Value = ventaXml.ToString();

                    SqlParameter nroDocParameter = new SqlParameter("@NroDocumento", SqlDbType.VarChar, 6);
                    nroDocParameter.Direction = ParameterDirection.Output;

                    command.Parameters.Add(xmlParameter);
                    command.Parameters.Add(nroDocParameter);

                    connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                        nroDocumento = nroDocParameter.Value.ToString();
                        mensaje = "Venta registrada exitosamente : " + nroDocumento;

                        return mensaje;
                    }
                    catch (Exception ex)
                    {
                        mensaje = ex.Message;
                    }
                }
            }

            return mensaje;
        }

        public string VerDetalleVenta(string numeroDocumento)
        {

            var cn = new Conexion();
            string jsonResult = "";


            using (SqlConnection connection = new SqlConnection(cn.getCadenaSQL()))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("sp_detalle_venta", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@nrodocumento", numeroDocumento));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            jsonResult = reader.GetString(0); 
                        }
                    }
                }
            }

            return jsonResult;
        }


    }
}
