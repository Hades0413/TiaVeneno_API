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
    public class UsuarioDAO : IUsuario
    {
        public IEnumerable<Usuario> obtenerUsuarios()
        {
            var oLista = new List<Usuario>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_listar_usuario", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new Usuario()
                        {
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            NombreCompleto = dr["NombreCompleto"].ToString(),
                            Correo = dr["Correo"].ToString(),
                            Clave = dr["Clave"].ToString()
                        });
                    }
                }
            }

            return oLista;
        }


        public Usuario ObtenerUsuarioPorId(int id)
        {
            Usuario usuario = null;
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_obtener_usuario_por_id", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUsuario", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        usuario = new Usuario()
                        {
                            IdUsuario = Convert.ToInt32(reader["IdUsuario"]),
                            NombreCompleto = reader["NombreCompleto"].ToString(),
                            Correo = reader["Correo"].ToString(),
                            Clave = reader["Clave"].ToString()
                        };
                    }
                }
            }

            return usuario;
        }


        public string RegistrarUsuario(Usuario reg)
        {
            string mensaje;
            var cn = new Conexion();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    oconexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_guardar_usuario", oconexion);
                    cmd.Parameters.AddWithValue("NombreCompleto", reg.NombreCompleto);
                    cmd.Parameters.AddWithValue("Correo", reg.Correo);
                    cmd.Parameters.AddWithValue("Clave", reg.Clave);
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


        public string ActualizarUsuario(Usuario reg)
        {
            string mensaje;
            var cn = new Conexion();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    oconexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_editar_usuario", oconexion);
                    cmd.Parameters.AddWithValue("@IdUsuario", reg.IdUsuario);
                    cmd.Parameters.AddWithValue("@NombreCompleto", reg.NombreCompleto);
                    cmd.Parameters.AddWithValue("@Correo", reg.Correo);
                    cmd.Parameters.AddWithValue("@Clave", reg.Clave);
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


        public bool EliminarUsuario(int idUsuario)
        {
            bool exito = false;
            var cn = new Conexion();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    oconexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_eliminar_usuario", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario", idUsuario);
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
