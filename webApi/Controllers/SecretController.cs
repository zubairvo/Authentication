using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace webApi.Controllers
{
    public class SecretController : Controller
    {
        [Route("/Secret")]
        [Authorize]
        public string Index()
        {
            return "Secret Message from Authorized User";
        }
    }
}
