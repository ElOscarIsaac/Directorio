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

namespace Directorio
{
    public class DirectorioService : IDirectorioService
    {
        public LoginResponse Login(LoginRequest Request)
        {
            DirectorioController Controller = new DirectorioController();
            return Controller.Login(Request);
        }
    }
}
