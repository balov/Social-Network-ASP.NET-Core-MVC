using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;
using SocialNetwork.Data.Entities;
using SocialNetwork.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork.Services.Implementations
{
    public class EventService : IEventService
    {
        private readonly SocialNetworkDbContext db;

        public EventService(SocialNetworkDbContext db)
        {
            this.db = db;
        }

        public void AddUserToEvent(string userId, int eventId)
        {
            if (this.Exists(eventId))
            {
                var ev = this.db.Events
                    .Include(e => e.Participants)
                    .FirstOrDefault(e => e.Id == eventId);

                if (!ev.Participants.Any(p => p.UserId == userId))
                {
                    ev.Participants.Add(new EventUser
                    {
                        UserId = userId
                    });
                }

                this.db.SaveChanges();
            }
        }

        public void Create(string imageUrl, string title, string location, string description, DateTime dateStarts, DateTime dateEnds, string creatorId)
        {
            var ev = new Event
            {
                ImageUrl = imageUrl,
                Title = title,
                Location = location,
                Description = description,
                DateEnds = dateEnds,
                DateStarts = dateStarts
            };
            ev.Participants.Add(new EventUser { UserId = creatorId });

            this.db.Events.Add(ev);
            this.db.SaveChanges();
        }

        public EventModel Details(int id)
        {
            if (this.Exists(id))
            {
                var ev = this.db.Events
                    .Include(e => e.Participants)
                    .FirstOrDefault(e => e.Id == id);

                return Mapper.Map<EventModel>(ev);
            }

            return null;
        }

        public bool Exists(int id) => this.db.Events.Any(e => e.Id == id);

        public IEnumerable<EventModel> UpcomingThreeEvents()
        {
            return this.db.Events.Where(e => e.DateEnds < DateTime.UtcNow).OrderBy(e => e.DateStarts).Take(3).ProjectTo<EventModel>().ToList();
        }
    }
}