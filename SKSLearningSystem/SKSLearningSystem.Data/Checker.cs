﻿using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKSLearningSystem.Data
{
   public class Checker
    {
        public static void Start()
        {

            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();
            IJobDetail job = JobBuilder.Create<OverdueCheckJob>().Build();

            ITrigger trigger = TriggerBuilder.Create().WithDailyTimeIntervalSchedule(
                s => s.WithIntervalInMinutes(1).OnEveryDay().StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(6, 7)).InTimeZone(TimeZoneInfo.Local)).Build();

            scheduler.ScheduleJob(job, trigger);           
        }
    }
}
