using DirectorioCore.Entities;
using DirectorioCore.ServiceContracts.Request;
using DirectorioCore.ServiceContracts.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DirectorioCore.BusinessLogic
{
    public class DirectorioController
    {
        /// <summary>
        /// Método que permite logear a un usuario, se devuelve la variable de login (LoginResponse)
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public LoginResponse Login(LoginRequest Request)
        {
            LoginResponse Response = new LoginResponse();
            try
            {
                Usuario user = DataAccess.DirectorioDA.GetUser(Request.usuario, Request.password);
                if (user.Id > 0)
                {
                    Response.Id = user.Id;
                    Response.Nombre = user.NombreEmpleado;
                    Response.Token = Security.Encrypt(user.Id + "|" + user.NombreUsuario + "|" + user.Password);
                }
            }
            catch(Exception exc)
            {
                Log.EscribeLog("Error en el método DirectorioController.Login: " + exc.Message);
            }
            return Response;
        }
        /// <summary>
        /// Método que permite obtener el catálogo de los empleados desde el sistema de nómina
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public CatalogoResponse CatalogoEmpleados(CatalogoRequest Request)
        {
            CatalogoResponse Response = new CatalogoResponse();
            Response.Lista = DataAccess.EslabonDA.CatalogoEmpleados();
            return Response;
        }
        /// <summary>
        /// Método que permite obtener el catálogo de ubicaciones
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public CatalogoResponse CatalogoUbicaciones(CatalogoRequest Request)
        {
            CatalogoResponse Response = new CatalogoResponse();
            Response.Lista = DataAccess.DirectorioDA.CatalogoUbicaciones();
            return Response;
        }
        /// <summary>
        /// Método que permite obtener el catálogo de áreas
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public CatalogoResponse CatalogoAreas(CatalogoRequest Request)
        {
            CatalogoResponse Response = new CatalogoResponse();
            Response.Lista = DataAccess.DirectorioDA.CatalogoAreas();
            return Response;
        }
        /// <summary>
        /// Método que permite obtener el catálogo de divisiones
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public CatalogoResponse CatalogoDivisiones(CatalogoRequest Request)
        {
            CatalogoResponse Response = new CatalogoResponse();
            Response.Lista = DataAccess.DirectorioDA.CatalogoDivisones();
            return Response;
        }
        /// <summary>
        /// Método de la capa de control que permite agregar un empleado al directorio
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public AgregarEmpleadoResponse AgregarEmpleado(Empleado Request)
        {
            AgregarEmpleadoResponse Response = new AgregarEmpleadoResponse();
            try 
            {
                Request.Id = DataAccess.DirectorioDA.InsertaEmpleado(Request);
                if(Request.Id > 0)
                {
                    Response.Message = "Empleado agregado correctamente";
                    Response.Success = true;
                }
                else
                {
                    Response.Message = "Ocurrió un problema al intentar insertar el empleado en la base de datos";
                    Response.Success = false;
                }
                
            }
            catch (Exception exc)
            {
                Log.EscribeLog("Error al ejecutar el método BusinessLogic.AgregarEmpleado: " + exc.Message);
            }
            return Response;
        }
        /// <summary>
        /// Método que permite extraer los empleados contenidos en el directorio
        /// </summary>
        /// <returns></returns>
        public ConsultaEmpleadosResponse ConsultaEmpleados()
        {
            ConsultaEmpleadosResponse Response = new ConsultaEmpleadosResponse();
            try
            {
                Response.Empleados = DataAccess.DirectorioDA.ConsultaEmpleados();
            }
            catch(Exception exc)
            {
                Log.EscribeLog("Error al ejecutar el método BusinessLogic.ConsultaEmpleados: " + exc.Message);
            }
            return Response;
        }
        /// <summary>
        /// Método que permite enviar por correo las credenciales del usuario con el correo electrónico proporcionado
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public bool EnviarCredenciales(string mail)
        {
            bool Enviado = false;
            Entities.Usuario user = new Entities.Usuario();
            try
            {
                user.Id = DataAccess.DirectorioDA.EmailExists(mail);
                if (user.Id > 0)
                {
                    user = DataAccess.DirectorioDA.GetUser(user.Id);
                    string Mensaje = @"<p>Hola " + user.NombreEmpleado + @", hemos recibido una solicitud de recuperación de credenciales para acceso al sistema de directorio</p>
                                       <p>Tus credenciales son las siguientes:</p>
                                       <p><strong>Usuario: </strong>" + user.NombreUsuario + @"</p>
                                       <p><strong>Contraseña: </strong>" + user.Password + @"</p>
                                       <p>Si usted no solicitó esta recuperación, favor de hacer caso omiso";
                    Enviado = SendMail(mail, "Recuperación de credenciales", Mensaje);
                }
            }
            catch (Exception exc)
            {
                Log.EscribeLog("Error en el método Controller.EnviarCredenciales: " + exc.Message);
            }
            return Enviado;
        }
        /// <summary>
        /// Permite enviar un correo electrónico desde la cuenta de correo configurada en la aplicación
        /// </summary>
        /// <param name="Destinatario">Pueden ser varios separados por ','</param>
        /// <param name="Asunto">Corresponde al subject del correo</param>
        /// <param name="Mensaje">Este puede ser en formato html para darle edición</param>
        /// <returns></returns>
        private bool SendMail(string Destinatario, string Asunto, string Mensaje)
        {
            bool Enviado = false;
            try
            {
                string MailParameters = ConfigurationManager.AppSettings["CorreoString"].ToString();
                SmtpClient client = new SmtpClient();
                client.Port = Convert.ToInt32(MailParameters.Split(',')[3]);
                client.Host = MailParameters.Split(',')[2];
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(MailParameters.Split(',')[0], MailParameters.Split(',')[1]);
                MailAddress From = new MailAddress("noreply@fujifilm.mx");
                MailMessage message = new MailMessage();
                message.From = From;
                message.Sender = new MailAddress("noreply@fujifilm.mx", "No Reply");
                message.ReplyToList.Add(new MailAddress("noreply@fujifilm.mx", "No Reply"));
                string[] Destinatarios = Destinatario.Split(',');
                foreach (string destinatario in Destinatarios)
                    message.To.Add(new MailAddress(destinatario.Trim()));
                message.Subject = Asunto;
                string FilePath = string.Empty;
                List<string> ConjuntoCertificados = new List<string>();
                message.Body = Mensaje;
                message.IsBodyHtml = true;
                client.Send(message);
                Enviado = true;
            }
            catch (Exception exc)
            {
                Log.EscribeLog("Error al intentar enviar el correo con asunto: " + Asunto + ": " + exc.Message);
            }
            return Enviado;
        }
    }
}
