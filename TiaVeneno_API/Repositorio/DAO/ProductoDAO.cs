using GamarraPlus.Datos;
using GamarraPlus.Models;
using TiaVeneno_API.Repositorio.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace TiaVeneno_API.Repositorio.DAO
{
    public class ProductoDAO : IProducto
    {
        public IEnumerable<Producto> ObtenerProductos()
        {
            var oLista = new List<Producto>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_listar_producto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new Producto()
                        {
                            IdProducto = Convert.ToInt32(dr["IdProducto"]),
                            Codigo = dr["Codigo"].ToString(),
                            oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(dr["IdCategoria"]), Descripcion = dr["DesCategoria"].ToString() },
                            Descripcion = dr["Descripcion"].ToString(),
                            PrecioCompra = Convert.ToDecimal(dr["PrecioCompra"], new CultureInfo("es-PE")),
                            PrecioVenta = Convert.ToDecimal(dr["PrecioVenta"], new CultureInfo("es-PE")),
                            Stock = Convert.ToInt32(dr["Stock"]),
                            RutaImagen = dr["RutaImagen"].ToString()
                        });
                    }
                }
            }

            return oLista;
        }

        public Producto ObtenerProductoPorId(int id)
        {
            Producto producto = null;
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_obtener_producto_por_id", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProducto", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        producto = new Producto()
                        {
                            IdProducto = Convert.ToInt32(reader["IdProducto"]),
                            Codigo = reader["Codigo"].ToString(),
                            oCategoria = new Categoria()
                            {
                                IdCategoria = Convert.ToInt32(reader["IdCategoria"]),
                                Descripcion = reader["DesCategoria"].ToString()
                            },
                            Descripcion = reader["Descripcion"].ToString(),
                            PrecioCompra = Convert.ToDecimal(reader["PrecioCompra"], new CultureInfo("es-PE")),
                            PrecioVenta = Convert.ToDecimal(reader["PrecioVenta"], new CultureInfo("es-PE")),
                            Stock = Convert.ToInt32(reader["Stock"]),
                            RutaImagen = reader["RutaImagen"].ToString()
                        };
                    }
                }
            }

            return producto;
        }

        public string RegistrarProducto(Producto reg)
        {
            string mensaje;
            var cn = new Conexion();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    oconexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_guardar_producto", oconexion);
                    cmd.Parameters.AddWithValue("Codigo", reg.Codigo);
                    cmd.Parameters.AddWithValue("IdCategoria", reg.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Descripcion", reg.Descripcion);
                    cmd.Parameters.AddWithValue("PrecioCompra", reg.PrecioCompra);
                    cmd.Parameters.AddWithValue("PrecioVenta", reg.PrecioVenta);
                    cmd.Parameters.AddWithValue("Stock", reg.Stock);
                    cmd.Parameters.AddWithValue("RutaImagen", reg.RutaImagen);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                    mensaje = "Registro exitoso";
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return mensaje;
        }


        public string ActualizarProducto(Producto reg)
        {
            string mensaje;
            var cn = new Conexion();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    oconexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_editar_producto", oconexion);
                    cmd.Parameters.AddWithValue("@IdProducto", reg.IdProducto);
                    cmd.Parameters.AddWithValue("@Codigo", reg.Codigo);
                    cmd.Parameters.AddWithValue("@IdCategoria", reg.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("@Descripcion", reg.Descripcion);
                    cmd.Parameters.AddWithValue("@PrecioCompra", reg.PrecioCompra);
                    cmd.Parameters.AddWithValue("@PrecioVenta", reg.PrecioVenta);
                    cmd.Parameters.AddWithValue("@Stock", reg.Stock);
                    cmd.Parameters.AddWithValue("@RutaImagen", reg.RutaImagen);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                    mensaje = "Actualización exitosa";
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return mensaje;
        }



        public bool EliminarProducto(int idProducto)
        {
            bool exito = false;
            var cn = new Conexion();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    oconexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_eliminar_producto", oconexion);
                    cmd.Parameters.AddWithValue("IdProducto", idProducto);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    exito = true;
                }
            }
            catch (Exception ex)
            {
                exito = false;
            }
            return exito;
        }

    }
}
