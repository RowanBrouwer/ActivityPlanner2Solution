using ActivityPlanner2.Data.ServerModels;
using ActivityPlanner2.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityPlanner2.Data
{
    public class ActivityLogic : IActivityLogic
    {
        IPersonInviteRepository invitePersonContext;
        ApplicationDbContext db;
        IPersonOrganizedActivityRepository organizeContext;
        public ActivityLogic(IPersonInviteRepository invitePersonContext, IPersonOrganizedActivityRepository organizeContext, ApplicationDbContext db)
        {
            this.invitePersonContext = invitePersonContext;
            this.organizeContext = organizeContext;
            this.db = db;
        }

        public async Task<IEnumerable<PersonOrganizedActivity>> OrganizersDtoListToOrganizerList(ActivityDTO value, int Id)
        {
            var Organizers = new List<PersonOrganizedActivity>();

            PersonOrganizedActivity invite;

            foreach (var personOrganizer in value.Organizers)
            {
                if (await invitePersonContext.GetInviteByActivityId(personOrganizer.ActivityId) != null)
                {
                    invite = (PersonOrganizedActivity)value.InvitedGuests;
                    await organizeContext.UpdateInvite(invite);
                }   

                Organizers.Add((PersonOrganizedActivity)personOrganizer);
            }

            db.PersonOrginizers.AddRange(Organizers);

            return Organizers;
        }

        public async Task<IEnumerable<PersonInvites>> PersonInviteDtoListToPersonInviteList(ActivityDTO value, int Id)
        {
            var InvitedPeople = new List<PersonInvites>();
            PersonInvites invite;

            foreach (var personInvite in value.InvitedGuests)
            {
                if (await invitePersonContext.GetInviteByActivityId(personInvite.ActivityId) != null)
                {
                    invite = (PersonInvites)value.InvitedGuests;
                    await invitePersonContext.UpdateInvite(invite);
                }

                InvitedPeople.Add((PersonInvites)personInvite);
            }

            db.PersonActivities.AddRange(InvitedPeople);

            return InvitedPeople;
        }
    }
}
