using System;
using System.Collections.Generic;
using YtManagement.Common.Model;
using YtManagement.Model;

namespace YtManagement.Service
{
    public interface IYoutubeService
    {
        ActionResult<List<YtPlaylist>> LoadPlaylists();
        ActionResult<List<YtChannel>> GetSubscriptions();
        ActionResult<List<YtVideo>> GetUploads(string channelId);
        ActionResult<List<YtVideo>> GetProcessed();

        ActionResult AddToPlaylist(string videoId, string targetPlaylist);
        void SetProcessed(string videoId, int matchRuleId);
        bool IsProcessed(string videoId);

        Uri CreateAuthUri();
        void ExchangeCode(string code);
    }
}