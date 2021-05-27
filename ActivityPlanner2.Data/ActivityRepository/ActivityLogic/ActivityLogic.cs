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

            foreach (var personOrganizer in value.Organizers)
            {
                personOrganizer.ActivityId = Id;

                if (await organizeContext.GetOrganizedActivitiesByPersonIdAndActivityId(personOrganizer.PersonId, personOrganizer.ActivityId) != null)
                {
                    var OrganizerToUpdate = (PersonOrganizedActivity)personOrganizer;
                    await organizeContext.UpdateOrganizedActivities(OrganizerToUpdate);
                }
                else
                {
                    var OrganizerToAdd = (PersonOrganizedActivity)personOrganizer;
                    db.PersonOrginizers.Add(OrganizerToAdd);
                }

                Organizers.Add((PersonOrganizedActivity)personOrganizer);
            }

            return Organizers;
        }

        public async Task<IEnumerable<PersonInvites>> PersonInviteDtoListToPersonInviteList(ActivityDTO value, int Id)
        {
            var InvitedPeople = new List<PersonInvites>();

            foreach (var personInvite in value.InvitedGuests)
            {
                personInvite.ActivityId = Id;

                if (await invitePersonContext.GetInviteByPersonIdAndActivityId(personInvite.PersonId, personInvite.ActivityId) != null)
                {
                    var InviteToUpdate = (PersonInvites)personInvite;
                    await invitePersonContext.UpdateInvite(InviteToUpdate);
                }
                else
                {
                    var invite = (PersonInvites)personInvite;
                    db.PersonActivities.Add(invite);
                }

                InvitedPeople.Add((PersonInvites)personInvite);

            }

            return InvitedPeople;
        }
    }
}
