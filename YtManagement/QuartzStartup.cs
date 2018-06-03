using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YtManagement.Job;

namespace YtManagement
{
    public class QuartzStartup
    {
        private IScheduler _scheduler; // after Start, and until shutdown completes, references the scheduler object
        private readonly IServiceProvider container;

        public QuartzStartup(IServiceProvider container)
        {
            this.container = container;
        }

        // starts the scheduler, defines the jobs and the triggers
        public void Start()
        {
            if (_scheduler != null)
            {
                throw new InvalidOperationException("Already started.");
            }

            var schedulerFactory = new StdSchedulerFactory();
            _scheduler = schedulerFactory.GetScheduler().Result;
            _scheduler.JobFactory = new JobFactory(container);
            _scheduler.Start().Wait();



            var updateJob = JobBuilder.Create<UpdateJob>()
                .WithIdentity(nameof(UpdateJob))
                .Build();
            var updateJobTrigger = TriggerBuilder.Create()
                .ForJob(nameof(UpdateJob))
                .StartNow()
                .WithSimpleSchedule(o => o.WithIntervalInMinutes(5).RepeatForever())
                .WithIdentity($"{nameof(UpdateJob)}Trigger")
                .Build();

            _scheduler.ScheduleJob(updateJob, updateJobTrigger).Wait();
        }

        // initiates shutdown of the scheduler, and waits until jobs exit gracefully (within allotted timeout)
        public void Stop()
        {
            if (_scheduler == null)
            {
                return;
            }

            // give running jobs 30 sec (for example) to stop gracefully
            if (_scheduler.Shutdown(waitForJobsToComplete: true).Wait(30000))
            {
                _scheduler = null;
            }
            else
            {
                // jobs didn't exit in timely fashion - log a warning...
            }
        }
    }
}
