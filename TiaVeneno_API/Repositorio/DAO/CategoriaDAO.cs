using GamarraPlus.Datos;
using GamarraPlus.Models;
using TiaVeneno_API.Repositorio.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace TiaVeneno_API.Repositorio.DAO
{
    public class CategoriaDAO : ICategoria
    {
        public IEnumerable<Categoria> ObtenerCategorias()
        {
            var oLista = new List<Categoria>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                string connectionString = cn.getCadenaSQL();
                conexion.ConnectionString = connectionString;

                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_listar_categoria", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new Categoria()
                        {
                            IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                            Descripcion = dr["Descripcion"].ToString()
                        });
                    }
                }
            }

            return oLista;
        }

        public bool RegistrarCategoria(Categoria obj)
        {
            bool respuesta;
            var cn = new Conexion();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    oconexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_guardar_categoria", oconexion);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    respuesta = true;
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
            }
            return respuesta;
        }

        public bool ActualizarCategoria(Categoria obj)
        {
            bool respuesta;
            var cn = new Conexion();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    oconexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_editar_categoria", oconexion);
                    cmd.Parameters.AddWithValue("IdCategoria", obj.IdCategoria);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    respuesta = true;
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
            }
            return respuesta;
        }

        public bool EliminarCategoria(int idCategoria)
        {
            bool respuesta;
            var cn = new Conexion();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    oconexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_eliminar_categoria", oconexion);
                    cmd.Parameters.AddWithValue("@IdCategoria", idCategoria);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    respuesta = true;
                }
            }
            catch (Exception ex)
            {
                if (ex is SqlException sqlEx && sqlEx.Number == 547)
                {
                    Console.WriteLine("No se puede eliminar la categoría porque está siendo utilizada por uno o más productos.");
                }
                else
                {
                    Console.WriteLine("Error al eliminar la categoría: " + ex.Message);
                }
                respuesta = false;
            }
            return respuesta;
        }


        public Categoria ObtenerCategoriaPorId(int id)
        {
            Categoria categoria = null;
            var cn = new Conexion();

            try
            {
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_obtener_categoria_por_id", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCategoria", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            categoria = new Categoria
                            {
                                IdCategoria = Convert.ToInt32(reader["IdCategoria"]),
                                Descripcion = reader["Descripcion"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la categoría por ID: " + ex.Message);
            }

            return categoria;
        }
    }
}
