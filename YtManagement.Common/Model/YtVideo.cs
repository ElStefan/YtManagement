using System;

namespace YtManagement.Common.Model
{
    public class YtVideo
    {
        public YtVideo(string id, string title, DateTime? publishedAt)
        {
            this.Id = id;
            this.Title = title;
            this.PublishedAt = publishedAt;
        }
        protected YtVideo()
        {

        }
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime? PublishedAt { get; set; }
    }
}
