﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebAPI.OutputCache.Demo
{
    public class TeamsController : ApiController
    {
        private static readonly List<Team> Teams = new List<Team>
            {
                new Team {Id = 1, League = "NHL", Name = "Leafs"},
                new Team {Id = 2, League = "NHL", Name = "Habs"},
            };

        [CacheOutput(ClientTimeSpan = 50, ServerTimeSpan = 50)]
        public IEnumerable<Team> Get()
        {
            return Teams;
        }

        [CacheOutputUntil(2013, 7, 20)]
        public Team Get(int id)
        {
            var team = Teams.FirstOrDefault(i => i.Id == id);
            if (team == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            return team;
        }

        public void Post(Team value)
        {
            if (!ModelState.IsValid) throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));
            Teams.Add(value);
        }

        public void Put(int id, Team value)
        {
            if (!ModelState.IsValid) throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));

            var team = Teams.FirstOrDefault(i => i.Id == id);
            if (team == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            team.League = value.League;
            team.Name = value.Name;
        }

        public void Delete(int id)
        {
            var team = Teams.FirstOrDefault(i => i.Id == id);
            if (team == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            Teams.Remove(team);
        }
    }
}
