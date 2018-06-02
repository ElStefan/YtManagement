using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YtManagement.Core;

namespace YtManagement.Job
{
    public class UpdateJob : IJob
    {
        private readonly ISystemCore _core;

        public UpdateJob(ISystemCore core)
        {
            this._core = core;
        }
        public Task Execute(IJobExecutionContext context)
        {
            //Console.WriteLine("Core value: " + _core.Value);
            return Task.FromResult(true);
        }
    }
}
