using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using YtManagement.Model;

namespace YtManagement.Service
{
    public interface IYoutubeService
    {
        ActionResult<List<YtPlaylist>> LoadPlaylists();
        ActionResult<List<YtChannel>> GetSubscriptions();
        ActionResult<List<YtVideo>> GetUploads(string channelId);

        ActionResult AddToPlaylist(string videoId, string targetPlaylist);
        void SetProcessed(string videoId);
        bool IsProcessed(string videoId);

        Uri CreateAuthUri();
        void ExchangeCode(string code);
    }
}