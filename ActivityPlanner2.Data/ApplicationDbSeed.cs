using ActivityPlanner2.Data;
using ActivityPlanner2.Data.ServerModels;
using ActivityPlanner2.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityPlanner2.Data
{
public class ApplicationDbSeed
{
        public static void Seed(UserManager<Person> userManager, ApplicationDbContext db, bool testing)
        {
            if (db.People.Count() == 0)
            {
                List<Person> people = new()
                {
                    new Person()
                    {
                        Email = "Admin1@Admin1",
                        NormalizedEmail = "ADMIN1@ADMIN1",
                        EmailConfirmed = true,
                        UserName = "Admin1@Admin1",
                        NormalizedUserName = "ADMIN1@ADMIN1",
                        FirstName = "Rowan",
                        LastName = "Brouwer"
                    },
                    new Person()
                    {
                        Email = "Admin2@Admin2",
                        NormalizedEmail = "ADMIN2@ADMIN2",
                        EmailConfirmed = true,
                        UserName = "Admin2@Admin2",
                        NormalizedUserName = "ADMIN2@ADMIN2",
                        FirstName = "Rowan",
                        MiddleName = "de",
                        LastName = "Drouwer"
                    },
                    new Person()
                    {
                        Email = "Admin3@Admin3",
                        NormalizedEmail = "ADMIN3@ADMIN3",
                        EmailConfirmed = true,
                        UserName = "Admin3@Admin3",
                        NormalizedUserName = "ADMIN3@ADMIN3",
                        FirstName = "Test",
                        LastName = "Person"
                    }
                };
                if (!testing)
                {
                    foreach (var user in people)
                    {
                        IdentityResult result = userManager.CreateAsync(user, "!Admin123").Result;
                    }
                }
                db.AddRange(people);         

                db.SaveChanges();
            }

            if (db.Activities.Count() == 0)
            {
                List<Activity> activities = new List<Activity>()
                {
                    new Activity()
                    {
                        ActivityName = "Scating",
                        DateOfDeadline = DateTime.Now,
                        DateOfEvent = DateTime.Now.AddHours(1),
                        Describtion = "well duhh Scating"
                    },
                    new Activity()
                    {
                        ActivityName = "Bowling",
                        DateOfDeadline = DateTime.Now,
                        DateOfEvent = DateTime.Now.AddHours(8),
                        Describtion = "well duhh Bowling"
                    },
                    new Activity()
                    {
                        ActivityName = "Football",
                        DateOfDeadline = DateTime.Now,
                        DateOfEvent = DateTime.Now.AddHours(6),
                        Describtion = "well duhh Football"
                    },
                    new Activity()
                    {
                        ActivityName = "Darting",
                        DateOfDeadline = DateTime.Now,
                        DateOfEvent = DateTime.Now.AddHours(2),
                        Describtion = "well duhh Darting"
                    }
                };
                db.AddRange(activities);
                db.SaveChanges();
            }

            if (db.PersonActivities.Count() == 0)
            {
                List<PersonInvites> invites = new List<PersonInvites>()
                {
                    //------------------------------------------Person 1-------------------------------------------//
                    new PersonInvites()
                    {
                        Person = db.People.Where(p => p.FirstName == "Rowan" && p.LastName == "Brouwer").FirstOrDefault(),
                        PersonId = db.People.Where(p => p.FirstName == "Rowan" && p.LastName == "Brouwer").FirstOrDefault().Id,
                        Activity = db.Activities.Where(a => a.ActivityName == "Scating").FirstOrDefault(),
                        ActivityId = db.Activities.Where(a => a.ActivityName == "Scating").FirstOrDefault().Id,
                        Accepted = false
                    },
                    new PersonInvites()
                    {
                        Person = db.People.Where(p => p.FirstName == "Rowan" && p.LastName == "Brouwer").FirstOrDefault(),
                        PersonId = db.People.Where(p => p.FirstName == "Rowan" && p.LastName == "Brouwer").FirstOrDefault().Id,
                        Activity = db.Activities.Where(a => a.ActivityName == "Bowling").FirstOrDefault(),
                        ActivityId = db.Activities.Where(a => a.ActivityName == "Bowling").FirstOrDefault().Id,
                        Accepted = true
                    },
                    new PersonInvites()
                    {
                        Person = db.People.Where(p => p.FirstName == "Rowan" && p.LastName == "Brouwer").FirstOrDefault(),
                        PersonId = db.People.Where(p => p.FirstName == "Rowan" && p.LastName == "Brouwer").FirstOrDefault().Id,
                        Activity = db.Activities.Where(a => a.ActivityName == "Football").FirstOrDefault(),
                        ActivityId = db.Activities.Where(a => a.ActivityName == "Football").FirstOrDefault().Id,
                        Accepted = false
                    },
                    new PersonInvites()
                    {
                        Person = db.People.Where(p => p.FirstName == "Rowan" && p.LastName == "Brouwer").FirstOrDefault(),
                        PersonId = db.People.Where(p => p.FirstName == "Rowan" && p.LastName == "Brouwer").FirstOrDefault().Id,
                        Activity = db.Activities.Where(a => a.ActivityName == "Darting").FirstOrDefault(),
                        ActivityId = db.Activities.Where(a => a.ActivityName == "Darting").FirstOrDefault().Id,
                        Accepted = true
                    },
                    //------------------------------------------Person 2-------------------------------------------//
                    new PersonInvites()
                    {
                        Person = db.People.Where(p => p.FirstName == "Rowan" && p.MiddleName == "de" && p.LastName == "Drouwer").FirstOrDefault(),
                        PersonId = db.People.Where(p => p.FirstName == "Rowan" && p.MiddleName == "de" && p.LastName == "Drouwer").FirstOrDefault().Id,
                        Activity = db.Activities.Where(a => a.ActivityName == "Scating").FirstOrDefault(),
                        ActivityId = db.Activities.Where(a => a.ActivityName == "Scating").FirstOrDefault().Id,
                        Accepted = true
                    },
                    new PersonInvites()
                    {
                        Person = db.People.Where(p => p.FirstName == "Rowan" && p.MiddleName == "de" && p.LastName == "Drouwer").FirstOrDefault(),
                        PersonId = db.People.Where(p => p.FirstName == "Rowan" && p.MiddleName == "de" && p.LastName == "Drouwer").FirstOrDefault().Id,
                        Activity = db.Activities.Where(a => a.ActivityName == "Bowling").FirstOrDefault(),
                        ActivityId = db.Activities.Where(a => a.ActivityName == "Bowling").FirstOrDefault().Id,
                        Accepted = false
                    },
                    new PersonInvites()
                    {
                        Person = db.People.Where(p => p.FirstName == "Rowan" && p.MiddleName == "de" && p.LastName == "Drouwer").FirstOrDefault(),
                        PersonId = db.People.Where(p => p.FirstName == "Rowan" && p.MiddleName == "de" && p.LastName == "Drouwer").FirstOrDefault().Id,
                        Activity = db.Activities.Where(a => a.ActivityName == "Football").FirstOrDefault(),
                        ActivityId = db.Activities.Where(a => a.ActivityName == "Football").FirstOrDefault().Id,
                        Accepted = true
                    },
                    new PersonInvites()
                    {
                        Person = db.People.Where(p => p.FirstName == "Rowan" && p.MiddleName == "de" && p.LastName == "Drouwer").FirstOrDefault(),
                        PersonId = db.People.Where(p => p.FirstName == "Rowan" && p.MiddleName == "de" && p.LastName == "Drouwer").FirstOrDefault().Id,
                        Activity = db.Activities.Where(a => a.ActivityName == "Darting").FirstOrDefault(),
                        ActivityId = db.Activities.Where(a => a.ActivityName == "Darting").FirstOrDefault().Id,
                        Accepted = false
                    },
                    //------------------------------------------Person 3-------------------------------------------//
                    new PersonInvites()
                    {
                        Person = db.People.Where(p => p.FirstName == "Test" && p.LastName == "Person").FirstOrDefault(),
                        PersonId = db.People.Where(p => p.FirstName == "Test" && p.LastName == "Person").FirstOrDefault().Id,
                        Activity = db.Activities.Where(a => a.ActivityName == "Scating").FirstOrDefault(),
                        ActivityId = db.Activities.Where(a => a.ActivityName == "Scating").FirstOrDefault().Id,
                        Accepted = true
                    },
                    new PersonInvites()
                    {
                        Person = db.People.Where(p => p.FirstName == "Test" && p.LastName == "Person").FirstOrDefault(),
                        PersonId = db.People.Where(p => p.FirstName == "Test" && p.LastName == "Person").FirstOrDefault().Id,
                        Activity = db.Activities.Where(a => a.ActivityName == "Bowling").FirstOrDefault(),
                        ActivityId = db.Activities.Where(a => a.ActivityName == "Bowling").FirstOrDefault().Id,
                        Accepted = true
                    },
                    new PersonInvites()
                    {
                        Person = db.People.Where(p => p.FirstName == "Test" && p.LastName == "Person").FirstOrDefault(),
                        PersonId = db.People.Where(p => p.FirstName == "Test" && p.LastName == "Person").FirstOrDefault().Id,
                        Activity = db.Activities.Where(a => a.ActivityName == "Football").FirstOrDefault(),
                        ActivityId = db.Activities.Where(a => a.ActivityName == "Football").FirstOrDefault().Id,
                        Accepted = false
                    },
                    new PersonInvites()
                    {
                        Person = db.People.Where(p => p.FirstName == "Test" && p.LastName == "Person").FirstOrDefault(),
                        PersonId = db.People.Where(p => p.FirstName == "Test" && p.LastName == "Person").FirstOrDefault().Id,
                        Activity = db.Activities.Where(a => a.ActivityName == "Darting").FirstOrDefault(),
                        ActivityId = db.Activities.Where(a => a.ActivityName == "Darting").FirstOrDefault().Id,
                        Accepted = false
                    }
                };

                db.PersonActivities.AddRange(invites);
                db.SaveChanges();

            }

            if (db.PersonOrginizers.Count() == 0)
            {
                List<PersonOrganizedActivity> orginizedActivities = new List<PersonOrganizedActivity>()
                {
                    new PersonOrganizedActivity
                    {
                        Organizer = db.People.Where(p => p.FirstName == "Test" && p.LastName == "Person").FirstOrDefault(),
                        OrganizerId = db.People.Where(p => p.FirstName == "Test" && p.LastName == "Person").FirstOrDefault().Id,
                        OrganizedActivity = db.Activities.Where(a => a.ActivityName == "Scating").FirstOrDefault(),
                        OrganizedActivityId = db.Activities.Where(a => a.ActivityName == "Scating").FirstOrDefault().Id
                    },
                    new PersonOrganizedActivity
                    {
                        Organizer = db.People.Where(p => p.FirstName == "Rowan" && p.LastName == "Brouwer").FirstOrDefault(),
                        OrganizerId = db.People.Where(p => p.FirstName == "Rowan" && p.LastName == "Brouwer").FirstOrDefault().Id,
                        OrganizedActivity = db.Activities.Where(a => a.ActivityName == "Bowling").FirstOrDefault(),
                        OrganizedActivityId = db.Activities.Where(a => a.ActivityName == "Bowling").FirstOrDefault().Id
                    },
                    new PersonOrganizedActivity
                    {
                        Organizer = db.People.Where(p => p.FirstName == "Rowan" && p.LastName == "Brouwer").FirstOrDefault(),
                        OrganizerId = db.People.Where(p => p.FirstName == "Rowan" && p.LastName == "Brouwer").FirstOrDefault().Id,
                        OrganizedActivity = db.Activities.Where(a => a.ActivityName == "Darting").FirstOrDefault(),
                        OrganizedActivityId = db.Activities.Where(a => a.ActivityName == "Darting").FirstOrDefault().Id
                    },
                    new PersonOrganizedActivity
                    {
                        Organizer = db.People.Where(p => p.FirstName == "Rowan" && p.MiddleName == "de" && p.LastName == "Drouwer").FirstOrDefault(),
                        OrganizerId = db.People.Where(p => p.FirstName == "Rowan" && p.MiddleName == "de" && p.LastName == "Drouwer").FirstOrDefault().Id,
                        OrganizedActivity = db.Activities.Where(a => a.ActivityName == "Football").FirstOrDefault(),
                        OrganizedActivityId = db.Activities.Where(a => a.ActivityName == "Football").FirstOrDefault().Id
                    },
                };

                db.PersonOrginizers.AddRange(orginizedActivities);
                db.SaveChanges();
            }
        }
    }
}
