using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YtManagement.Common.Model;

namespace YtManagement.Extension
{
    public static class PlaylistItemExtensions
    {
        public static YtVideo AsYtVideo(this PlaylistItem item)
        {
            if(item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return new YtVideo(item.Id, item.Snippet.Title, item.Snippet.PublishedAt);
        }
    }
}