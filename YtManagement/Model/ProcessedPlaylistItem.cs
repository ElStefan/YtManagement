using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YtManagement.Model
{
    public class ProcessedPlaylistItem
    {
        public string Key { get; set; }
        public PlaylistItem PlaylistItem { get; set; }
    }
}
