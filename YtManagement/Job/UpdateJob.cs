using Quartz;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YtManagement.Common.Model;
using YtManagement.Repository;
using YtManagement.Service;

namespace YtManagement.Job
{
    [DisallowConcurrentExecution]
    public class UpdateJob : IJob
    {
        private readonly IYoutubeService ytService;
        private readonly IRulesRepository rulesRepository;

        public UpdateJob(IYoutubeService ytService, IRulesRepository rulesRepository)
        {
            this.ytService = ytService;
            this.rulesRepository = rulesRepository;
        }
        public Task Execute(IJobExecutionContext context)
        {
            try
            {                
                Process();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return Task.FromResult(false);
            }
            return Task.CompletedTask;
        }

        private void Process()
        {
            var subscriptionResult = ytService.GetSubscriptions();
            if (subscriptionResult.Status != ActionStatus.Success)
            {
                return;
            }

            var rules = rulesRepository.GetAll().Data;
            foreach (var subscription in subscriptionResult.Data)
            {
                var uploads = ytService.GetUploads(subscription.Id);
                if (uploads.Status != ActionStatus.Success)
                {
                    continue;
                }
                var unprocessedVideos = uploads.Data.Where(o => !ytService.IsProcessed(o.Id)).ToList();
                foreach (var video in unprocessedVideos) // should only process new videos
                {

                    Console.WriteLine("Checking video {0}", video.Title);
                    ManagementRule matchedRule = null;
                    foreach (var rule in rules.OrderByDescending(o => o.Priority))
                    {
                        var success = false;
                        switch (rule.SearchPosition)
                        {
                            case SearchPosition.VideoTitle:
                                success = CheckRuleOnString(video.Title, rule, out matchedRule);
                                break;
                            case SearchPosition.ChannelTitle:
                                success = CheckRuleOnString(subscription.Title, rule, out matchedRule);
                                break;
                            case SearchPosition.All:
                                if(success = CheckRuleOnString(video.Title, rule, out matchedRule))
                                {
                                    break;
                                }
                                success |= CheckRuleOnString(subscription.Title, rule, out matchedRule);
                                break;
                            default:
                                Console.WriteLine("SearchPosition {0} not supported", rule.SearchPosition);
                                continue;
                        }
                        if(success)
                        {
                            break;
                        }
                    }
                    if (matchedRule == null)
                    {
                        Console.WriteLine("No rules found for '{0}'", video.Title);
                        continue;
                    }

                    matchedRule.LastMatch = DateTime.Now;
                    if (!matchedRule.IgnoreVideo)
                    {
                        Console.WriteLine("Moving video '{0}' to '{1}'", video.Title, matchedRule.Target);
                        var addResult = ytService.AddToPlaylist(video.Id, matchedRule.Target);
                        if(addResult.Status != ActionStatus.Success)
                        {
                            Console.WriteLine("{0} {1}",addResult.Status, addResult.Message);
                            continue;
                        }
                    }
                    ytService.SetProcessed(video.Id);

                }
            }
        }

        private bool CheckRuleOnString(string text, ManagementRule rule, out ManagementRule matchedRule)
        {
            if (rule.Regex)
            {
                if (!Regex.IsMatch(text, rule.RuleString, RegexOptions.Singleline | RegexOptions.IgnoreCase))
                {
                    matchedRule = null;
                    return false;
                }
                matchedRule = rule;
                return true;
            }
            else
            {
                if (text.IndexOf(rule.RuleString, StringComparison.OrdinalIgnoreCase) < 0)
                {
                    matchedRule = null;
                    return false;
                }

                matchedRule = rule;
                return true;
            }
        }
    }
}
