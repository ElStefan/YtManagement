using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Mvc;
using YtManagement.Service;

namespace YtManagement.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IYoutubeService ytService;

        public AuthController(IYoutubeService ytService)
        {
            this.ytService = ytService;
        }

        [HttpGet("Authenticate")]
        public ActionResult Authenticate()
        {
            var uri = ytService.CreateAuthUri();
            
            return Redirect(uri.ToString());
        }

        [HttpGet("Exchange")]
        public string Exchange(string code)
        {
            ytService.ExchangeCode(code);
            return $"{DateTime.Now}: Auth OK";
        }
    }
}
