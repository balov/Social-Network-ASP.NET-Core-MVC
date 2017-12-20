using SocialNetwork.Services.Models;
using System;
using System.Collections.Generic;

namespace SocialNetwork.Services
{
    public interface IEventService : IService
    {
        void Create(string imageUrl, string title, string location, string description, DateTime dateStarts, DateTime dateEnds, string creatorId);

        bool Exists(int id);

        EventModel Details(int id);

        IEnumerable<EventModel> UpcomingThreeEvents();

        void AddUserToEvent(string userId, int eventId);
    }
}