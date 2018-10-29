using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using YtManagement.Common.Model;
using YtManagement.Service;

namespace YtManagement.Controllers
{
    [Route("api/[controller]")]
    public class PlaylistsController : Controller
    {
        private IYoutubeService _youtubeService;

        public PlaylistsController(IYoutubeService youtubeService)
        {
            this._youtubeService = youtubeService;
        }

        [HttpGet]
        public ActionResult<List<YtPlaylist>> Get() => _youtubeService.GetPlaylists();
    }
}
