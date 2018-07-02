using System;

namespace YtManagement.Model
{
    public class ManagementRule
    {
        public int Id { get; set; }
        public bool Regex { get; set; }
        public string Target { get; set; }
        public string RuleString { get; set; }
        public DateTime? LastMatch { get; set; }
        public bool IgnoreVideo { get; set; }
        public int Priority { get; set; }
        public SearchPosition SearchPosition { get; set; }
    }
}
