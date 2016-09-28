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
using System.Drawing.Drawing2D;

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
                if (!Contents.ElementAt(1).ToLower().Contains("undefined"))
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
                    json = json.Replace("Ã¡", "á");
                    json = json.Replace("Ã©", "é");
                    json = json.Replace("Ã*", "í");
                    json = json.Replace("Ã³", "ó");
                    json = json.Replace("Ãº", "ú");
                    json = json.Replace("Ã", "Á");
                    json = json.Replace("Ã‰", "É");
                    json = json.Replace("Ã", "Í");
                    json = json.Replace("Ã“", "Ó");
                    json = json.Replace("Ãš", "Ú");
                    json = json.Replace("Ã±", "ñ");
                    json = json.Replace("Ã‘", "Ñ");
                    json = json.Replace("Ã¼", "ü");
                    json = json.Replace("Ãœ", "Ü");
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
                                var Thumbnail = ResizeImage(bmp, new Size(100, 100));
                                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                                System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                                EncoderParameters myEncoderParameters = new EncoderParameters(1);
                                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 90L);
                                myEncoderParameters.Param[0] = myEncoderParameter;
                                byte[] byteArray;
                                using (var foto = new MemoryStream())
                                {
                                    Thumbnail.Save(foto, jpgEncoder, myEncoderParameters);
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
                    json = json.Replace("Ã¡", "á");
                    json = json.Replace("Ã©", "é");
                    json = json.Replace("Ã*", "í");
                    json = json.Replace("Ã³", "ó");
                    json = json.Replace("Ãº", "ú");
                    json = json.Replace("Ã", "Á");
                    json = json.Replace("Ã‰", "É");
                    json = json.Replace("Ã", "Í");
                    json = json.Replace("Ã“", "Ó");
                    json = json.Replace("Ãš", "Ú");
                    json = json.Replace("Ã±", "ñ");
                    json = json.Replace("Ã‘", "Ñ");
                    json = json.Replace("Ã¼", "ü");
                    json = json.Replace("Ãœ", "Ü");
                    AgregarEmpleadoRequest Request = new AgregarEmpleadoRequest();
                    Request = JsonConvert.DeserializeObject<AgregarEmpleadoRequest>(json);
                    if (Security.ValidateToken(Request.Token))
                    {
                        DirectorioController controlador = new DirectorioController();
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

        public AgregarEmpleadoResponse ActualizarEmpleado(Stream BinaryRequest)
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
                if (!Contents.ElementAt(1).ToLower().Contains("undefined"))
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
                    json = json.Replace("Ã¡", "á");
                    json = json.Replace("Ã©", "é");
                    json = json.Replace("Ã*", "í");
                    json = json.Replace("Ã³", "ó");
                    json = json.Replace("Ãº", "ú");
                    json = json.Replace("Ã", "Á");
                    json = json.Replace("Ã‰", "É");
                    json = json.Replace("Ã", "Í");
                    json = json.Replace("Ã“", "Ó");
                    json = json.Replace("Ãš", "Ú");
                    json = json.Replace("Ã±", "ñ");
                    json = json.Replace("Ã‘", "Ñ");
                    json = json.Replace("Ã¼", "ü");
                    json = json.Replace("Ãœ", "Ü");
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
                                var Thumbnail = ResizeImage(bmp, new Size(100, 100));
                                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                                System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                                EncoderParameters myEncoderParameters = new EncoderParameters(1);
                                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 90L);
                                myEncoderParameters.Param[0] = myEncoderParameter;
                                byte[] byteArray;
                                using (var foto = new MemoryStream())
                                {
                                    Thumbnail.Save(foto, jpgEncoder, myEncoderParameters);
                                    byteArray = foto.ToArray();
                                    Request.Entidad.Foto = Convert.ToBase64String(byteArray);
                                }
                            }
                            Response = controlador.ActualizarEmpleado(Request.Entidad);
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
                    json = json.Replace("Ã¡", "á");
                    json = json.Replace("Ã©", "é");
                    json = json.Replace("Ã*", "í");
                    json = json.Replace("Ã³", "ó");
                    json = json.Replace("Ãº", "ú");
                    json = json.Replace("Ã", "Á");
                    json = json.Replace("Ã‰", "É");
                    json = json.Replace("Ã", "Í");
                    json = json.Replace("Ã“", "Ó");
                    json = json.Replace("Ãš", "Ú");
                    json = json.Replace("Ã±", "ñ");
                    json = json.Replace("Ã‘", "Ñ");
                    json = json.Replace("Ã¼", "ü");
                    json = json.Replace("Ãœ", "Ü");
                    AgregarEmpleadoRequest Request = new AgregarEmpleadoRequest();
                    Request = JsonConvert.DeserializeObject<AgregarEmpleadoRequest>(json);
                    if (Security.ValidateToken(Request.Token))
                    {
                        DirectorioController controlador = new DirectorioController();
                        Response = controlador.ActualizarEmpleado(Request.Entidad);
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
        public static Image ResizeImage(Image imgToResize, Size destinationSize)
        {
            var originalWidth = imgToResize.Width;
            var originalHeight = imgToResize.Height;

            //how many units are there to make the original length
            var hRatio = (float)originalHeight / destinationSize.Height;
            var wRatio = (float)originalWidth / destinationSize.Width;

            //get the shorter side
            var ratio = Math.Min(hRatio, wRatio);

            var hScale = Convert.ToInt32(destinationSize.Height * ratio);
            var wScale = Convert.ToInt32(destinationSize.Width * ratio);

            //start cropping from the center
            var startX = (originalWidth - wScale) / 2;
            var startY = (originalHeight - hScale) / 2;

            //crop the image from the specified location and size
            var sourceRectangle = new Rectangle(startX, startY, wScale, hScale);

            //the future size of the image
            var bitmap = new Bitmap(destinationSize.Width, destinationSize.Height);

            //fill-in the whole bitmap
            var destinationRectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            //generate the new image
            using (var g = Graphics.FromImage(bitmap))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(imgToResize, destinationRectangle, sourceRectangle, GraphicsUnit.Pixel);
            }
            return bitmap;
        }
        public EliminarEmpleadoResponse EliminarEmpleado(EliminarEmpleadoRequest Request)
        {
            EliminarEmpleadoResponse Response = new EliminarEmpleadoResponse();
            if (Security.ValidateToken(Request.Token))
            {
                DirectorioController Controlador = new DirectorioController();
                Response.Success = Controlador.EliminarEmpleado(Request.EmpleadoId);
                Response.Message = Response.Success ? "Empleado eliminado correctamente" : "No fue posible eliminar al empleado";
            }
            return Response;
        }
    }
}
