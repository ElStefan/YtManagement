namespace YtManagement.Common.Model
{
    public class YtPlaylist
    {
        public YtPlaylist(string id, string title)
        {
            this.Id = id;
            this.Title = title;
        }

        protected YtPlaylist()
        {
        }

        public string Id { get; set; }
        public string Title { get; set; }
    }
}
