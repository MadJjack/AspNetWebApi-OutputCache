﻿using System;

namespace WebAPI.OutputCache.Time
{
    internal class ThisDay : IModelQuery<DateTime, CacheTime>
    {
        private readonly int hour;
        private readonly int minute;
        private readonly int second;

        public ThisDay(int hour, int minute, int second)
        {
            this.hour = hour;
            this.minute = minute;
            this.second = second;
        }

        public CacheTime Execute(DateTime model)
        {
            var cacheTime = new CacheTime
            {
                AbsoluteExpiration = new DateTime(model.Year,
                                                  model.Month,
                                                  model.Day,
                                                  hour,
                                                  minute,
                                                  second),
            };

            if (cacheTime.AbsoluteExpiration <= model)
                cacheTime.AbsoluteExpiration = cacheTime.AbsoluteExpiration.AddDays(1);

            cacheTime.ServerTimespan = cacheTime.AbsoluteExpiration.Subtract(model);
            cacheTime.ClientTimeSpan = cacheTime.ServerTimespan;

            return cacheTime;
        }
    }
}
