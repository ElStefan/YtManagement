using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YtManagement.Model
{
    public class ManagementRule
    {
        public int Id { get; set; }
        public bool Regex { get; set; }
        public string Target { get; set; }
        public string RuleString { get; set; }
        public DateTime? LastMatch { get; set; }
    }
}
