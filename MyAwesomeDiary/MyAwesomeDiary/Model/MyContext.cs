using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace MyAwesomeDiary.Model
{
    public class MyContext : DbContext
    {
        public MyContext() : base("MADStringConnection")
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Nation> Nations { get; set; }
        public DbSet<SpecialDay> SpecialDays { get; set; }
        public DbSet<NationalDay> NationalDays { get; set; }
        public DbSet<Diary> Diaries { get; set; }
        public DbSet<Mood> Moods { get; set; }
        public DbSet<UserMood> UserMoods { get; set; }
        public DbSet<UserEvent> UserEvents { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<EventPriority> EventPriorities { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }
        public DbSet<TaskState> TaskStates { get; set; }
    }
}
