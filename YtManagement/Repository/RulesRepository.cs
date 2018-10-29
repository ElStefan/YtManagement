using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using YtManagement.Common.Model;
using YtManagement.Storage;

namespace YtManagement.Repository
{
    public class RulesRepository : IRulesRepository
    {
        private readonly ConcurrentDictionary<int, ManagementRule> _cache = new ConcurrentDictionary<int, ManagementRule>();
        private readonly IStorage _storage;

        public RulesRepository(IStorage storage)
        {
            this._storage = storage ?? throw new ArgumentNullException(nameof(storage));

            var ruleResult = this._storage.Load<ManagementRule>();
            if(ruleResult.Status != ActionStatus.Success)
            {
                throw new TypeLoadException(ruleResult.Message);
            }

            foreach (var rule in ruleResult.Data)
            {
                _cache.TryAdd(rule.Id, rule);
            }
        }

        public ActionResult<int> Add(ManagementRule item)
        {
            var maxId = 0;
            if (this._cache.Values.Count > 0)
            {
                maxId = this._cache.Keys.Max();
            }
            item.Id = maxId + 1;
            if(this._cache.Values.Any(o => o.RuleString.Equals(item.RuleString, StringComparison.OrdinalIgnoreCase) && o.SearchPosition == item.SearchPosition))
            {
                return new ActionResult<int>(ActionStatus.Error, "Rule already exists");
            }
            if(!this._cache.TryAdd(item.Id, item))
            {
                return new ActionResult<int>(ActionStatus.Error, "Id already exists, try again");
            }
            if (item.IgnoreVideo)
            {
                item.Target = null;
            }
            var saveResult = this._storage.Save(this._cache);
            if(saveResult.Status != ActionStatus.Success)
            {
                return new ActionResult<int>(saveResult);
            }
            return new ActionResult<int>(ActionStatus.Success, item.Id);
        }

        public ActionResult Delete(int id)
        {
            if(!this._cache.TryRemove(id, out var trash))
            {
                return new ActionResult(ActionStatus.NotFound, $"Rule {id} not found");
            }
            this._storage.Save(this._cache);
            return new ActionResult(ActionStatus.Success);
        }

        public ActionResult<ManagementRule> Get(int id)
        {
            if (!_cache.TryGetValue(id, out ManagementRule value))
            {
                return new ActionResult<ManagementRule>(ActionStatus.NotFound, $"Rule {id} not found");
            }
            return new ActionResult<ManagementRule>(ActionStatus.Success, value);

        }

        public ActionResult<List<ManagementRule>> GetAll()
        {
            return new ActionResult<List<ManagementRule>>(ActionStatus.Success, this._cache.Values.ToList());
        }

        public ActionResult Update(ManagementRule item)
        {
            var cacheItemResult = Get(item.Id);
            if(cacheItemResult.Status != ActionStatus.Success)
            {
                return cacheItemResult;
            }
            var oldRule = cacheItemResult.Data;
            oldRule.Regex = item.Regex;
            oldRule.RuleString = item.RuleString;
            oldRule.Target = item.IgnoreVideo ? null : item.Target;
            oldRule.IgnoreVideo = item.IgnoreVideo;
            oldRule.Priority = item.Priority;
            oldRule.SearchPosition = item.SearchPosition;

            this._storage.Save(this._cache);

            return new ActionResult(ActionStatus.Success);
        }
    }
}
