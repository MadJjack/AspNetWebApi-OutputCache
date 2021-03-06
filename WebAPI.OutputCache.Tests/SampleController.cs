﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebAPI.OutputCache.Tests
{
    public class SampleController : ApiController
    {
        [CacheOutput(ClientTimeSpan = 100, ServerTimeSpan = 100)]
        public string Get_c100_s100()
        {
            return "test";
        }

        [CacheOutput(ClientTimeSpan = 50, MustRevalidate = true)]
        public string Get_c50_mustR()
        {
            return "test";
        }

        [CacheOutput(ServerTimeSpan = 50, ExcludeQueryStringFromCacheKey = false)]
        public string Get_s50_exclude_false(int id)
        {
            return "test"+id;
        }

        [CacheOutput(ServerTimeSpan = 50, ExcludeQueryStringFromCacheKey = true)]
        public string Get_s50_exclude_true(int id)
        {
            return "test" + id;
        }

        [CacheOutputUntil(2013,01,25,17,00)]
        public string Get_until25012013_1700()
        {
            return "test";
        }

        [CacheOutputUntilToday(23,55)]
        public string Get_until2355_today()
        {
            return "value";
        }

        [CacheOutputUntilThisMonth(31)]
        public string Get_until31_thismonth()
        {
            return "value";
        }

        [CacheOutputUntilThisYear(7,31)]
        public string Get_until731_thisyear()
        {
            return "value";
        }

        [CacheOutputUntilThisYear(7, 31, MustRevalidate = true)]
        public string Get_until731_thisyear_mustrevalidate()
        {
            return "value";
        }

        [CacheOutput(AnonymousOnly = true, ClientTimeSpan = 50, ServerTimeSpan = 50)]
        public string Get_s50_c50_anonymousonly()
        {
            return "value";
        }

        [HttpGet]
        [CacheOutput(AnonymousOnly = true, ClientTimeSpan = 50, ServerTimeSpan = 50)]
        public string etag_match_304()
        {
            return "value";
        }
    }
}
