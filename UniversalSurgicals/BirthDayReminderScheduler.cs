﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using Quartz.Impl;

namespace UniversalSurgicals
{
    public class BirthDayReminderScheduler
    {
        public static void Start()
        {

            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();
            IJobDetail job = JobBuilder.Create<BirthDayReminderJob>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                //.StartNow()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInHours(24)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(23, 59))
                  )
                .Build();




    //        ITrigger trigger = TriggerBuilder.Create()
    //.WithIdentity("trigger1", "group1")
    //.StartNow()
    //.WithSimpleSchedule(x => x
    //    .WithIntervalInHours(24)
    //    .RepeatForever())
    //.Build();
            scheduler.ScheduleJob(job, trigger);
        }
    }
}