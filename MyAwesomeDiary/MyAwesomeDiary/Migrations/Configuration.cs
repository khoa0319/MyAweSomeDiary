namespace MyAwesomeDiary.Migrations
{
    using MyAwesomeDiary.Model;
    using MyAwesomeDiary.ViewModel;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MyAwesomeDiary.Model.MyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MyAwesomeDiary.Model.MyContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var nationsToAdd = new List<Nation>()
            {
                new Nation {Name = "Việt Nam"},
                new Nation {Name = "Mỹ"},
                new Nation {Name = "Nhật"},
                new Nation {Name = "Trung Quốc"}
            };
            context.Nations.AddRange(nationsToAdd);
            //người dùng mặc định
            context.Users.Add(new User
            {
                UserID = "admin",
                Password = new PasswordSecurityProvider().GetHashPassword("admin"),
                BirthDate = DateTime.Now,
                FirstName = "ad",
                LastName = "min",
                NationID = 1,
                PrivateAnswer = "admin"
            });
            // loại sự kiện mặc định
            var eventTypeToAdd = new List<EventType>()
            {
                new EventType {Name = "Meeting"},
                new EventType {Name = "Seminar"},
                new EventType {Name = "Birthday"},
                new EventType {Name = "FieldTrip"},
                new EventType {Name = "Wedding"},
                new EventType {Name = "FamilyMeeting"},
                new EventType {Name = "Hangout"},
                new EventType {Name = "Movie"},
                new EventType {Name = "Coffee"},
                new EventType {Name = "School"}

            };
            context.EventTypes.AddRange(eventTypeToAdd);
            // mức độ mặc định
            var EventPriorityToAdd = new List<EventPriority>()
            {
                new EventPriority {Name = "Highest"},
                new EventPriority {Name = "High"},
                new EventPriority {Name = "Normal"},
                new EventPriority {Name = "Low"},
                new EventPriority {Name = "Lowest"}
            };
            context.EventPriorities.AddRange(EventPriorityToAdd);
            // tâm trạng mặc định
            var moodsToAdd = new List<Mood>()
            {
                new Mood{Name = "Amazing fantastic day"},
                new Mood{Name = "Really good and happy day"},
                new Mood{Name = "Normal average day"},
                new Mood{Name = "Exhausted tired day"},
                new Mood{Name = "Depressed sad day"},
                new Mood{Name = "Frustrated angry day"},
                new Mood{Name = "Stress-out frantic day"}
            };
            context.Moods.AddRange(moodsToAdd);

            //save changeds
            context.SaveChanges();
        }
    }
}
