using DirectorioCore.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorioCore.DataAccess
{
    public class DirectorioDA
    {
        private static MySqlConnection Conectar()
        {
            MySqlConnection connection = new MySqlConnection();
            try
            {
                connection.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                connection.Open();
            }
            catch (Exception exc)
            {
                throw new Exception("Error al intentar conectar con la base de datos: " + exc.Message);
            }
            return connection;
        }
        public static Usuario GetUser(string user, string password)
        {
            Usuario modelo = new Usuario();
            try
            {
                using (MySqlConnection connection = Conectar())
                {
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = @" SELECT U.UsuarioID,
				                                    U.vchNombreEmpleado,
				                                    U.vchNombreUsuario,
				                                    U.vchPassword,
                                                    U.vchCorreoElectronico,
				                                    U.datFechaRegistro,
				                                    U.bitActivo AS UsuarioActivo
		                                       FROM USUARIOS U 
                                              WHERE U.vchNombreUsuario = @user AND U.vchPassword = @password";
                    command.Parameters.AddWithValue("@user", user);
                    command.Parameters.AddWithValue("@password", password);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            modelo.Id = reader.GetInt32("UsuarioID");
                            modelo.NombreEmpleado = reader.GetString("vchNombreEmpleado");
                            modelo.NombreUsuario = reader.GetString("vchNombreUsuario");
                            modelo.Password = reader.GetString("vchPassword");
                            modelo.CorreoElectronico = reader.GetString("vchCorreoElectronico");
                            modelo.FechaRegistro = reader.GetDateTime("datFechaRegistro");
                            modelo.Activo = reader.GetInt32("UsuarioActivo") == 1 ? true : false;
                        }
                        reader.Close();
                    }
                    connection.Close();
                }
            }
            catch (Exception exc)
            {
                BusinessLogic.Log.EscribeLog("Error al ejectuar el metodo DirectorioDA.GetUser : " + exc.Message);
            }
            return modelo;
        }
    }
}
