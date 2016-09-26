using DirectorioCore.ServiceContracts.Request;
using DirectorioCore.ServiceContracts.Response;
using DirectorioCore.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Drawing;
using System.Drawing.Imaging;

namespace Directorio
{
    public class DirectorioService : IDirectorioService
    {
        public LoginResponse Login(LoginRequest Request)
        {
            DirectorioController Controller = new DirectorioController();
            return Controller.Login(Request);
        }
        public bool RecoverCredentials(string request)
        {
            bool Sent = false;
            DirectorioController controlador = new DirectorioController();
            Sent = controlador.EnviarCredenciales(request);
            return Sent;
        }
        public CatalogoResponse CatalogoEmpleados(CatalogoRequest Request)
        {
            CatalogoResponse Response = new CatalogoResponse();
            if (Security.ValidateToken(Request.Token))
            {
                DirectorioController controlador = new DirectorioController();
                Response = controlador.CatalogoEmpleados(Request);
            }
            return Response;
        }
        public CatalogoResponse CatalogoUbicaciones(CatalogoRequest Request)
        {
            CatalogoResponse Response = new CatalogoResponse();
            if (Security.ValidateToken(Request.Token))
            {
                DirectorioController controlador = new DirectorioController();
                Response = controlador.CatalogoUbicaciones(Request);
            }
            return Response;
        }
        public CatalogoResponse CatalogoAreas(CatalogoRequest Request)
        {
            CatalogoResponse Response = new CatalogoResponse();
            if (Security.ValidateToken(Request.Token))
            {
                DirectorioController controlador = new DirectorioController();
                Response = controlador.CatalogoAreas(Request);
            }
            return Response;
        }
        public CatalogoResponse CatalogoDivisiones(CatalogoRequest Request)
        {
            CatalogoResponse Response = new CatalogoResponse();
            if (Security.ValidateToken(Request.Token))
            {
                DirectorioController controlador = new DirectorioController();
                Response = controlador.CatalogoDivisiones(Request);
            }
            return Response;
        }
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
        public AgregarEmpleadoResponse AgregarEmpleado(Stream BinaryRequest)
        {
            AgregarEmpleadoResponse Response = new AgregarEmpleadoResponse();
            try
            {
                Encoding encoding = Encoding.Default;
                byte[] data = MultipartParser.ToByteArray(BinaryRequest);
                string content = encoding.GetString(data);
                List<string> Contents = new List<string>();
                string entidad = string.Empty;
                string line = string.Empty;
                string[] contentSplited = content.Split('\n');
                for (int i = 0; i < contentSplited.Length; i++)
                {
                    if (contentSplited[i].StartsWith("------") && i != 0)
                    {
                        entidad += contentSplited[i] + "\n";
                        Contents.Add(entidad);
                        entidad = contentSplited[i] + "\n";
                    }
                    else
                    {
                        entidad += contentSplited[i] + "\n";
                    }
                }
                if (Contents.Count > 1)
                {
                    contentSplited = Contents.ElementAt(0).Split('\n');
                    string json = "";
                    foreach (string linea in contentSplited)
                    {
                        if (linea.StartsWith("{"))
                        {
                            json = linea.Substring(0, linea.Length - 1).Replace("\\", "");
                            break;
                        }
                    }
                    AgregarEmpleadoRequest Request = new AgregarEmpleadoRequest();
                    Request = JsonConvert.DeserializeObject<AgregarEmpleadoRequest>(json);
                    if (Security.ValidateToken(Request.Token))
                    {
                        DirectorioController controlador = new DirectorioController();
                        byte[] bytes = encoding.GetBytes(Contents.ElementAt(1));
                        MultipartParser parserOfFileOne = new MultipartParser(bytes, encoding);
                        if (parserOfFileOne.Success)
                        {
                            Bitmap bmp;
                            using (var ms = new MemoryStream(parserOfFileOne.FileContents))
                            {
                                bmp = new Bitmap(ms);
                                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                                System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                                EncoderParameters myEncoderParameters = new EncoderParameters(1);
                                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 10L);
                                myEncoderParameters.Param[0] = myEncoderParameter;
                                byte[] byteArray;
                                using (var foto = new MemoryStream())
                                {
                                    bmp.Save(foto, jpgEncoder, myEncoderParameters);
                                    byteArray = foto.ToArray();
                                    Request.Entidad.Foto = Convert.ToBase64String(byteArray);
                                }
                            }
                            Response = controlador.AgregarEmpleado(Request.Entidad);
                        }
                        else
                        {
                            throw new Exception("Error al intentar decodificar imagen");
                        }
                    }
                    else
                    {
                        Response.Message = "La información no se subió correctamente";
                        Response.Success = false;
                    }
                }
                else
                {
                    contentSplited = Contents.ElementAt(0).Split('\n');
                    string json = "";
                    foreach (string linea in contentSplited)
                    {
                        if (linea.StartsWith("{"))
                        {
                            json = linea.Substring(0, linea.Length - 1).Replace("\\", "");
                            break;
                        }
                    }
                    AgregarEmpleadoRequest Request = new AgregarEmpleadoRequest();
                    Request = JsonConvert.DeserializeObject<AgregarEmpleadoRequest>(json);
                    if (Security.ValidateToken(Request.Token))
                    {
                        DirectorioController controlador = new DirectorioController();
                        Request.Entidad.Foto = "";
                        Response = controlador.AgregarEmpleado(Request.Entidad);
                    }
                }
            }
            catch (Exception e)
            {
                Response.Message = e.Message;
                Response.Success = false;
            }
            return Response;
        }
        public ConsultaEmpleadosResponse ConsultaEmpleados()
        {
            DirectorioController Controller = new DirectorioController();
            return Controller.ConsultaEmpleados();
        }
    }
}
