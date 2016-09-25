using DirectorioCore.ServiceContracts.Request;
using DirectorioCore.ServiceContracts.Response;
using System;
using System.Collections.Generic;
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
    }
}
