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
        /// <summary>
        /// Obtener la información del usuario a partir de los parámetros ingresados
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Obtener informaicón de usuario a partir de los parámetros ingresados
        /// </summary>
        /// <param name="UsuarioId"></param>
        /// <returns></returns>
        public static Usuario GetUser(int UsuarioId)
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
                                              WHERE U.UsuarioID = @user";
                    command.Parameters.AddWithValue("@user", UsuarioId);
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
        /// <summary>
        /// Permite saber si existe el correo ingresado como parámetro, si es así se regresa el identificador del usuario al que se ha registrado
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public static int EmailExists(string mail)
        {
            int UsuarioId = 0;
            try
            {
                using (MySqlConnection connection = Conectar())
                {
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = @" SELECT U.UsuarioID
		                                       FROM USUARIOS U 
                                              WHERE U.vchCorreoElectronico = @mail";
                    command.Parameters.AddWithValue("@mail", mail);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            UsuarioId = reader.GetInt32("UsuarioID");
                        }
                        reader.Close();
                    }
                    connection.Close();
                }
            }
            catch(Exception exc)
            {
                BusinessLogic.Log.EscribeLog("Error al ejecutar el método DirectorioDA.EmailExists : " + exc.Message);
            }
            return UsuarioId;
        }
        /// <summary>
        /// Método que permite extraer el catálogo de ubicaciones
        /// </summary>
        /// <param name="UsuarioId"></param>
        /// <returns></returns>
        public static List<Catalogo> CatalogoUbicaciones()
        {
            List<Catalogo> Ubicaciones = new List<Catalogo>();
            try
            {
                using (MySqlConnection connection = Conectar())
                {
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = @" SELECT UbicacionID,
                                                    vchDescripcion
		                                       FROM Ubicacion
                                              WHERE bitActivo = 1";
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        Catalogo ubicacion;
                        while (reader.Read())
                        {
                            ubicacion = new Catalogo();
                            ubicacion.id = reader.GetInt32("UbicacionID");
                            ubicacion.descripcion = reader.GetString("vchDescripcion");
                            Ubicaciones.Add(ubicacion);
                        }
                        reader.Close();
                    }
                    connection.Close();
                }
            }
            catch (Exception exc)
            {
                BusinessLogic.Log.EscribeLog("Error al ejectuar el metodo DirectorioDA.CatalogoUbicaciones : " + exc.Message);
            }
            return Ubicaciones;
        }
        /// <summary>
        /// Método que permite extraer el catálogo de los departamentos o áreas
        /// </summary>
        /// <returns></returns>
        public static List<Catalogo> CatalogoAreas()
        {
            List<Catalogo> Areas = new List<Catalogo>();
            try
            {
                using (MySqlConnection connection = Conectar())
                {
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = @" SELECT DepartamentoID,
                                                    vchDescripcion
		                                       FROM Departamento
                                              WHERE bitActivo = 1";
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        Catalogo area;
                        while (reader.Read())
                        {
                            area = new Catalogo();
                            area.id = reader.GetInt32("DepartamentoID");
                            area.descripcion = reader.GetString("vchDescripcion");
                            Areas.Add(area);
                        }
                        reader.Close();
                    }
                    connection.Close();
                }
            }
            catch (Exception exc)
            {
                BusinessLogic.Log.EscribeLog("Error al ejectuar el metodo DirectorioDA.CatalogoAreas : " + exc.Message);
            }
            return Areas;
        }
        /// <summary>
        /// Método que permite obtener el catálogo de divisiones
        /// </summary>
        /// <returns></returns>
        public static List<Catalogo> CatalogoDivisones()
        {
            List<Catalogo> Divisiones = new List<Catalogo>();
            try
            {
                using (MySqlConnection connection = Conectar())
                {
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = @" SELECT DivisionID,
                                                    DepartamentoID,
                                                    vchDescripcion
		                                       FROM Division
                                              WHERE bitActivo = 1";
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        Catalogo division;
                        while (reader.Read())
                        {
                            division = new Catalogo();
                            division.id = reader.GetInt32("DivisionID");
                            division.codigo = reader.GetInt32("DepartamentoID").ToString();
                            division.descripcion = reader.GetString("vchDescripcion");
                            Divisiones.Add(division);
                        }
                        reader.Close();
                    }
                    connection.Close();
                }
            }
            catch (Exception exc)
            {
                BusinessLogic.Log.EscribeLog("Error al ejectuar el metodo DirectorioDA.CatalogoDivisiones : " + exc.Message);
            }
            return Divisiones;
        }
        /// <summary>
        /// Permite insertar un empleado a la base de datos
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static int InsertaEmpleado(Empleado request)
        {
            int Id = 0;
            try 
            {
                using (MySqlConnection connection = Conectar())
                {
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = @"INSERT INTO Empleado (
				                                vchNombre,
                                                vchExtension,
                                                vchPuesto,
                                                vchCorreo,
                                                vchCelular,
                                                DepartamentoID,
                                                DivisionID,
                                                UbicacionID,
                                                vchNumeroDirecto,
                                                txtFoto,
                                                bitActivo,
                                                AUDUSUARIO) VALUES (
                                                @vchNombre,
                                                @vchExtension,
                                                @vchPuesto,
                                                @vchCorreo,
                                                @vchCelular,
                                                @DepartamentoID,
                                                @DivisionID,
                                                @UbicacionID,
                                                @vchNumeroDirecto,
                                                @txtFoto,
                                                @bitActivo,
                                                @AUDUSUARIO
                                            )";
                    command.Parameters.AddWithValue("@vchNombre", request.Nombre);
                    command.Parameters.AddWithValue("@vchExtension", request.Extension);
                    command.Parameters.AddWithValue("@vchPuesto", request.Puesto);
                    command.Parameters.AddWithValue("@vchCorreo", request.Correo);
                    command.Parameters.AddWithValue("@vchCelular", request.Celular);
                    command.Parameters.AddWithValue("@DepartamentoID", request.Departamento.id);
                    command.Parameters.AddWithValue("@DivisionID", request.Division.id);
                    command.Parameters.AddWithValue("@UbicacionID", request.Ubicacion.id);
                    command.Parameters.AddWithValue("@vchNumeroDirecto", request.NumeroDirecto);
                    command.Parameters.AddWithValue("@txtFoto", request.Foto);
                    command.Parameters.AddWithValue("@bitActivo", request.Activo);
                    command.Parameters.AddWithValue("@AUDUSUARIO", request.AUDUSUARIO);
                    command.ExecuteNonQuery();
                    Id = Convert.ToInt32(command.LastInsertedId);
                    command.Dispose();
                    connection.Close();
                }
            }
            catch (Exception exc)
            {
                BusinessLogic.Log.EscribeLog("Error al ejectuar el metodo DirectorioDA.InsertaEmpleado : " + exc.Message);
            }
            return Id;
        }
        /// <summary>
        /// Permite actualizar la información de un empleado en la base de datos
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool ActualizaEmpleado(Empleado request)
        {
            bool Actualizado = false;
            try
            {
                using (MySqlConnection connection = Conectar())
                {
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = @"Update Empleado SET
				                                vchNombre = @vchNombre,
                                                vchExtension = @vchExtension,
                                                vchPuesto = @vchPuesto,
                                                vchCorreo = @vchCorreo,
                                                vchCelular = @vchCelular,
                                                DepartamentoID = @DepartamentoID,
                                                DivisionID = @DivisionID,
                                                UbicacionID = @UbicacionID,
                                                vchNumeroDirecto = @vchNumeroDirecto,
                                                txtFoto = @txtFoto,
                                                bitActivo = @bitActivo,
                                                AUDUSUARIO = @AUDUSUARIO WHERE EmpleadoID = @EmpleadoID";
                    command.Parameters.AddWithValue("@vchNombre", request.Nombre);
                    command.Parameters.AddWithValue("@vchExtension", request.Extension);
                    command.Parameters.AddWithValue("@vchPuesto", request.Puesto);
                    command.Parameters.AddWithValue("@vchCorreo", request.Correo);
                    command.Parameters.AddWithValue("@vchCelular", request.Celular);
                    command.Parameters.AddWithValue("@DepartamentoID", request.Departamento);
                    command.Parameters.AddWithValue("@DivisionID", request.Division);
                    command.Parameters.AddWithValue("@UbicacionID", request.Ubicacion);
                    command.Parameters.AddWithValue("@vchNumeroDirecto", request.NumeroDirecto);
                    command.Parameters.AddWithValue("@txtFoto", request.Foto);
                    command.Parameters.AddWithValue("@bitActivo", request.Activo);
                    command.Parameters.AddWithValue("@AUDUSUARIO", request.AUDUSUARIO);
                    command.Parameters.AddWithValue("@EmpleadoID", request.Id);
                    if (command.ExecuteNonQuery() > 0)
                        Actualizado = true;
                    command.Dispose();
                    connection.Close();
                }
            }
            catch (Exception exc)
            {
                BusinessLogic.Log.EscribeLog("Error al ejectuar el metodo DirectorioDA.ActualizaEmpleado : " + exc.Message);
            }
            return Actualizado;
        }
        /// <summary>
        /// Permite hacer el borrado lógico del empleado en la base de datos
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool EliminaEmpleado(int EmpleadoId)
        {
            bool Eliminado = false;
            try
            {
                using (MySqlConnection connection = Conectar())
                {
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = @"Update Empleado SET bitActivo = 0 WHERE EmpleadoID = @EmpleadoID";
                    command.Parameters.AddWithValue("@EmpleadoID", EmpleadoId);
                    if (command.ExecuteNonQuery() > 0)
                        Eliminado = true;
                    command.Dispose();
                    connection.Close();
                }
            }
            catch (Exception exc)
            {
                BusinessLogic.Log.EscribeLog("Error al ejectuar el metodo DirectorioDA.ActualizaEmpleado : " + exc.Message);
            }
            return Eliminado;
        }
        /// <summary>
        /// Permite hacer la consulta de los empleados activos en la base de datos
        /// </summary>
        /// <returns></returns>
        public static List<Empleado> ConsultaEmpleados()
        {
            List<Empleado> Empleados = new List<Empleado>();
            try
            {
                using (MySqlConnection connection = Conectar())
                {
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = @" SELECT EM.EmpleadoID,
                                                    EM.vchNombre,
                                                    EM.vchExtension,
                                                    EM.vchPuesto,
                                                    EM.vchCorreo,
                                                    EM.vchCelular,
                                                    EM.DepartamentoID,
                                                    D.vchDescripcion AS Departamento,
                                                    EM.DivisionID,
                                                    DI.vchDescripcion AS Division,
                                                    EM.UbicacionID,
                                                    U.vchDescripcion AS Ubicacion,
                                                    EM.vchNumeroDirecto,
                                                    EM.txtFoto,
                                                    EM.bitActivo,
                                                    EM.AUDUSUARIO
		                                       FROM Empleado EM
										 INNER JOIN Departamento D ON D.DepartamentoID = EM.DepartamentoID
                                         INNER JOIN Division DI ON DI.DivisionID = EM.DivisionID
                                         INNER JOIN Ubicacion U ON U.UbicacionID = EM.UbicacionID
                                              WHERE EM.bitActivo = 1;";
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        Empleado entidad;
                        while (reader.Read())
                        {
                            entidad = new Empleado();
                            entidad.Id = reader.GetInt32("EmpleadoID");
                            entidad.Nombre = reader.GetString("vchNombre");
                            entidad.Extension = reader.GetString("vchExtension");
                            entidad.Puesto = reader.GetString("vchPuesto");
                            entidad.Correo = reader.GetString("vchCorreo");
                            entidad.Celular = reader.GetString("vchCelular");
                            entidad.Departamento.id = reader.GetInt32("DepartamentoID");
                            entidad.Departamento.descripcion = reader.GetString("Departamento");
                            entidad.Division.id = reader.GetInt32("DivisionID");
                            entidad.Division.descripcion = reader.GetString("Division");
                            entidad.Ubicacion.id = reader.GetInt32("UbicacionID");
                            entidad.Ubicacion.descripcion = reader.GetString("Ubicacion");
                            entidad.NumeroDirecto = reader.GetString("vchNumeroDirecto");
                            entidad.Foto = reader.GetString("txtFoto");
                            entidad.Activo = true;
                            entidad.AUDUSUARIO = reader.GetInt32("AUDUSUARIO");
                            Empleados.Add(entidad);
                        }
                        reader.Close();
                    }
                    connection.Close();
                }
            }
            catch (Exception exc)
            {
                BusinessLogic.Log.EscribeLog("Error al ejectuar el metodo DirectorioDA.CatalogoDivisiones : " + exc.Message);
            }
            return Empleados;
        }
    }
}
