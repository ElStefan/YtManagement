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

        public string Id { get; private set; }
        public string Title { get; private set; }
        public DateTime? PublishedAt { get; private set; }
        public int MatchRuleId { get; private set; }

        public YtVideo SetMatchRuleId(int matchRuleId)
        {
            return new YtVideo(Id, Title, PublishedAt) { MatchRuleId = matchRuleId };
        }
    }
}
