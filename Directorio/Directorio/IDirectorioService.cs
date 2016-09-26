using DirectorioCore.ServiceContracts.Request;
using DirectorioCore.ServiceContracts.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Directorio
{
    [ServiceContract]
    public interface IDirectorioService
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "json/Login")]
        LoginResponse Login(LoginRequest Request);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "json/RecoverCredentials")]
        bool RecoverCredentials(string request);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "json/CatalogoEmpleados")]
        CatalogoResponse CatalogoEmpleados(CatalogoRequest Request);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "json/CatalogoUbicaciones")]
        CatalogoResponse CatalogoUbicaciones(CatalogoRequest Request);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "json/CatalogoAreas")]
        CatalogoResponse CatalogoAreas(CatalogoRequest Request);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "json/CatalogoDivisiones")]
        CatalogoResponse CatalogoDivisiones(CatalogoRequest Request);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "json/AgregarEmpleado")]
        AgregarEmpleadoResponse AgregarEmpleado(Stream BinaryRequest);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "json/ConsultaEmpleados")]
        ConsultaEmpleadosResponse ConsultaEmpleados();
    }
}
