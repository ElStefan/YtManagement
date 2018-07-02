using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YtManagement.Common.Model;
using YtManagement.Model;
using YtManagement.Service;

namespace YtManagement.Controllers
{
    [Route("api/[controller]")]
    public class VideosController : Controller
    {
        private IYoutubeService _youtubeService;

        public VideosController(IYoutubeService youtubeService)
        {
            this._youtubeService = youtubeService;
        }

        [HttpGet]
        public ActionResult<List<YtVideo>> Get() => _youtubeService.GetProcessed();
    }
}
