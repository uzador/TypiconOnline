﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Schedule;

namespace TypiconOnline.AppServices.Messaging.Schedule
{
    public class GetScheduleWeekResponse
    {
        public string WeekName { get; set; }
        public IEnumerable<ScheduleDay> Days { get; set; }
    }
}
