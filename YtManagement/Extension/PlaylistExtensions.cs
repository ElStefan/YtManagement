using Google.Apis.YouTube.v3.Data;
using System;
using YtManagement.Common.Model;

namespace YtManagement.Extension
{
    public static class PlaylistExtensions
    {
        public static YtPlaylist AsYtPlaylist(this Playlist item)
        {
            if(item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return new YtPlaylist(item.Id, item.Snippet.Title);
        }
    }
}