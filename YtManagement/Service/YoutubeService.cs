using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using YtManagement.Common.Model;
using YtManagement.Extension;
using YtManagement.Model;
using YtManagement.Storage;

namespace YtManagement.Service
{
    /// <summary>
    /// Handles youtube requests
    /// </summary>
    public class YoutubeService : IYoutubeService
    {
        private readonly GoogleAuthorizationCodeFlow _flow;
        private readonly IStorage _storage;
        private UserCredential _credential;

        private ConcurrentDictionary<string, ProcessedPlaylistItem> _processedPlaylistItems = new ConcurrentDictionary<string, ProcessedPlaylistItem>();
        private ConcurrentDictionary<string, Playlist> _playlists = new ConcurrentDictionary<string, Playlist>();
        private ConcurrentDictionary<string, PlaylistItem> _playlistItems = new ConcurrentDictionary<string, PlaylistItem>();
        private ConcurrentDictionary<string, Subscription> _subscriptions = new ConcurrentDictionary<string, Subscription>();
        private ConcurrentDictionary<string, DateTime> _lastRequestDates = new ConcurrentDictionary<string, DateTime>();

        /// <summary>
        /// Create a new youtube service with the given storage to save work
        /// </summary>
        /// <param name="storage">storage where data will be saved</param>
        public YoutubeService(IStorage storage)
        {
            var flowInit = new GoogleAuthorizationCodeFlow.Initializer();
            using (var stream = new FileStream("/data/client_id.json", FileMode.Open, FileAccess.Read))
            {
                flowInit.ClientSecrets = GoogleClientSecrets.Load(stream).Secrets;
            }
            flowInit.Scopes = new[] { YouTubeService.Scope.Youtube };
            flowInit.DataStore = new FileDataStore(storage);
            this._flow = new GoogleAuthorizationCodeFlow(flowInit);
            var token = this._flow.LoadTokenAsync("YtManagement", CancellationToken.None).Result;
            if (token != null)
            {
                _credential = new UserCredential(this._flow, "YtManagement", token);
            }

            this._storage = storage;

            var processedVideoResult = this._storage.Load<ProcessedPlaylistItem>();
            if (processedVideoResult.Status != ActionStatus.Success)
            {
                throw new TypeLoadException(processedVideoResult.Message);
            }
            foreach (var processedVideo in processedVideoResult.Data)
            {
                _processedPlaylistItems.TryAdd(processedVideo.Key, processedVideo);
            }
        }

        /// <summary>
        /// Load all playlists from the logged in user
        /// </summary>
        public ActionResult<List<YtPlaylist>> LoadPlaylists()
        {
            if (_lastRequestDates.TryGetValue(nameof(LoadPlaylists), out var requestTime))
            {
                if (requestTime > DateTime.Now.AddHours(-1))
                {
                    var data = _playlists.Values.Select(o => new YtPlaylist { Id = o.Id, Title = o.Snippet.Title }).ToList();
                    return new ActionResult<List<YtPlaylist>>(ActionStatus.Success, data);
                }
            }

            if (_credential == null)
            {
                return new ActionResult<List<YtPlaylist>>(ActionStatus.NotAuthorized, "Credentials missing");
            }

            var service = CreateService();

            var playlistRequest = service.Playlists.List("id, snippet");
            playlistRequest.Mine = true;
            playlistRequest.MaxResults = 50;
            var playlistResponse = playlistRequest.Execute();
            var list = playlistResponse.Items.Select(o => new YtPlaylist { Id = o.Id, Title = o.Snippet.Title }).ToList();

            _playlists.AddUpdateOrRemove(playlistResponse.Items, o => o.Id);

            _lastRequestDates.AddOrUpdate(nameof(LoadPlaylists), DateTime.Now, (key, oldValue) => DateTime.Now);
            return new ActionResult<List<YtPlaylist>>(ActionStatus.Success, list);

        }

        
        /// <summary>
        /// Create a new youtube service for requests
        /// </summary>
        private YouTubeService CreateService()
        {
            return new YouTubeService(new BaseClientService.Initializer
            {
                HttpClientInitializer = _credential,
                ApplicationName = "Playlist Management",
            });
        }

        /// <summary>
        /// Create the uri where the user should be redirected to
        /// </summary>
        public Uri CreateAuthUri()
        {
            return this._flow.CreateAuthorizationCodeRequest("http://localhost:50002/api/auth/Exchange").Build();
        }

        /// <summary>
        /// Create credentials with code from youtube
        /// </summary>
        /// <param name="code"></param>
        public void ExchangeCode(string code)
        {
            var result = this._flow.ExchangeCodeForTokenAsync("YtManagement", code, "http://localhost:50002/api/auth/Exchange", CancellationToken.None).Result;
            _credential = new UserCredential(this._flow, "YtManagement", result);
        }

        /// <summary>
        /// Get all subscriptions from the logged in user
        /// </summary>
        public ActionResult<List<YtChannel>> GetSubscriptions()
        {
            if (_lastRequestDates.TryGetValue(nameof(GetSubscriptions), out var requestTime))
            {
                if (requestTime > DateTime.Now.AddHours(-1))
                {
                    var data = _subscriptions.Values.Select(o => new YtChannel { Id = o.Snippet.ResourceId.ChannelId, Title = o.Snippet.Title }).ToList();
                    return new ActionResult<List<YtChannel>>(ActionStatus.Success, data);
                }
            }
            if (_credential == null)
            {
                return new ActionResult<List<YtChannel>>(ActionStatus.NotAuthorized, "Credentials missing");
            }
            var service = CreateService();
            var subscriptionsListRequest = service.Subscriptions.List("id, snippet");
            subscriptionsListRequest.Mine = true;
            subscriptionsListRequest.MaxResults = 50;

            var subscriptions = new List<Subscription>();
            SubscriptionListResponse subscriptionListResponse = null;
            string pageToken;

            do
            {
                subscriptionListResponse = subscriptionsListRequest.Execute();
                if (subscriptionListResponse == null)
                {
                    return new ActionResult<List<YtChannel>>(ActionStatus.Error, "Could not get subscriptions");
                }
                pageToken = subscriptionListResponse?.NextPageToken;
                subscriptionsListRequest.PageToken = pageToken;
                subscriptions.AddRange(subscriptionListResponse.Items);
            }
            while (subscriptionListResponse.Items.Count > 0 && pageToken != null);
            var list = subscriptions.Select(o => new YtChannel { Id = o.Snippet.ResourceId.ChannelId, Title = o.Snippet.Title }).ToList();

            _subscriptions.AddUpdateOrRemove(subscriptionListResponse.Items, o => o.Id);
            _lastRequestDates.AddOrUpdate(nameof(GetSubscriptions), DateTime.Now, (key, oldValue) => DateTime.Now);

            return new ActionResult<List<YtChannel>>(ActionStatus.Success, list);
        }

        /// <summary>
        /// Get all uploads from a specific channel
        /// </summary>
        /// <param name="channelId"></param>
        public ActionResult<List<YtVideo>> GetUploads(string channelId)
        {
            if (_credential == null)
            {
                return new ActionResult<List<YtVideo>>(ActionStatus.NotAuthorized, "Credentials missing");
            }
            var service = CreateService();
            var playlistItemsListRequest = service.PlaylistItems.List("id,snippet");

            var uploadListId = channelId.Remove(1, 1).Insert(1, "U");
            playlistItemsListRequest.PlaylistId = uploadListId;
            playlistItemsListRequest.MaxResults = 50;
            var playlistItemsResponse = playlistItemsListRequest.Execute();
            var list = playlistItemsResponse.Items
                .Where(o => o.Snippet.PublishedAt > DateTime.Now.AddDays(-7))
                .Where(o => !o.Snippet.Title.Equals("Private video"))
                .OrderBy(o => o.Snippet.PublishedAt)
                .Select(o => o.AsYtVideo())
                .ToList();

            foreach (var item in playlistItemsResponse.Items)
            {
                _playlistItems.AddOrUpdate(item.Id, item, (key, oldValue) => item);
            }

            return new ActionResult<List<YtVideo>>(ActionStatus.Success, list);
        }

        /// <summary>
        /// Add video to playlist
        /// </summary>
        /// <param name="playlistItemId"></param>
        /// <param name="targetPlaylistTitle"></param>
        public ActionResult AddToPlaylist(string playlistItemId, string targetPlaylistTitle)
        {
            if (_credential == null)
            {
                return new ActionResult(ActionStatus.NotAuthorized, "Credentials missing");
            }

            var playlistResult = GetPlaylistFromCache(targetPlaylistTitle);
            if (playlistResult.Status != ActionStatus.Success)
            {
                return playlistResult;
            }
            var targetPlaylist = playlistResult.Data;

            var playlistItemResult = GetPlaylistItemFromCache(playlistItemId);
            if (playlistItemResult.Status != ActionStatus.Success)
            {
                return playlistItemResult;
            }
            
            var service = CreateService();

            var playlistItemToInsert = new PlaylistItem();
            playlistItemToInsert.Snippet = new PlaylistItemSnippet();
            playlistItemToInsert.Snippet.ResourceId = new ResourceId();
            playlistItemToInsert.Snippet.ResourceId.Kind = playlistItemResult.Data.Snippet.ResourceId.Kind;
            playlistItemToInsert.Snippet.ResourceId.VideoId = playlistItemResult.Data.Snippet.ResourceId.VideoId;
            playlistItemToInsert.Snippet.PlaylistId = targetPlaylist.Id;

            var playlistItemsInsertRequest = service.PlaylistItems.Insert(playlistItemToInsert, "id,snippet");
            var playlistItemsInsertResult = playlistItemsInsertRequest.Execute();

            return new ActionResult(ActionStatus.Success);

        }

        /// <summary>
        /// Get playlist item from local cache, so we don't need to ask youtube again
        /// </summary>
        /// <param name="playlistItemId"></param>
        private ActionResult<PlaylistItem> GetPlaylistItemFromCache(string playlistItemId)
        {
            if (!_playlistItems.TryGetValue(playlistItemId, out var value))
            {
                return new ActionResult<PlaylistItem>(ActionStatus.NotFound, $"Playlist item {playlistItemId} not found");
            }
            return new ActionResult<PlaylistItem>(ActionStatus.Success, value);
        }

        /// <summary>
        /// Get playlist from local cache, so we don't need to ask youtube again
        /// </summary>
        /// <param name="targetPlaylistTitle"></param>
        private ActionResult<Playlist> GetPlaylistFromCache(string targetPlaylistTitle)
        {
            LoadPlaylists();

            var playlist = _playlists.Where(o => o.Value.Snippet.Title.Equals(targetPlaylistTitle, StringComparison.OrdinalIgnoreCase)).FirstOrDefault().Value;
            if (playlist == null)
            {
                return new ActionResult<Playlist>(ActionStatus.NotFound, $"Playlist {targetPlaylistTitle} not found");
            }
            return new ActionResult<Playlist>(ActionStatus.Success, playlist);
        }

        /// <summary>
        /// Marks the video id as processed
        /// </summary>
        /// <param name="videoId"></param>
        public void SetProcessed(string videoId, int matchRuleId)
        {
            var playlistItemResult = GetPlaylistItemFromCache(videoId);
            if (playlistItemResult.Status != ActionStatus.Success)
            {
                return;
            }
            this._processedPlaylistItems.GetOrAdd(videoId, new ProcessedPlaylistItem { Key = videoId, PlaylistItem = playlistItemResult.Data, MatchRuleId = matchRuleId });

            var removeKeys = this._processedPlaylistItems
                .Where(o => o.Value.PlaylistItem.Snippet.PublishedAt <= DateTime.Now.AddDays(-14))
                .Select(o => o.Key)
                .ToList();

            foreach (var item in removeKeys)
            {
                this._processedPlaylistItems.TryRemove(item, out _);
            }
            this._storage.Save(_processedPlaylistItems);
        }

        /// <summary>
        /// Tells if a video has already been processed
        /// </summary>
        /// <param name="videoId"></param>
        public bool IsProcessed(string videoId)
        {
            return this._processedPlaylistItems.ContainsKey(videoId);
        }

        /// <summary>
        /// Returns all processed videos
        /// </summary>
        public ActionResult<List<YtVideo>> GetProcessed()
        {
            var data = this._processedPlaylistItems.Values.Select(o => o.PlaylistItem.AsYtVideo().SetMatchRuleId(o.MatchRuleId)).ToList();
            return new ActionResult<List<YtVideo>>(ActionStatus.Success, data);
        }
    }
}
