namespace MyAwesomeDiary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Diaries",
                c => new
                    {
                        DiaryID = c.Int(nullable: false, identity: true),
                        UserID = c.String(nullable: false, maxLength: 128),
                        WritingDate = c.DateTime(nullable: false),
                        Content = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DiaryID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        Password = c.String(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        BirthDate = c.DateTime(),
                        PrivateAnswer = c.String(nullable: false),
                        NationID = c.Int(nullable: false),
                        Detail_UserAddress = c.String(),
                        Detail_Email = c.String(),
                        Detail_PhoneNumber = c.String(),
                        Detail_Work = c.String(),
                        Detail_Intro = c.String(),
                        Detail_Image = c.String(),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Nations", t => t.NationID, cascadeDelete: true)
                .Index(t => t.NationID);
            
            CreateTable(
                "dbo.Nations",
                c => new
                    {
                        NationID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.NationID);
            
            CreateTable(
                "dbo.EventPriorities",
                c => new
                    {
                        EventPriorityID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.EventPriorityID);
            
            CreateTable(
                "dbo.EventTypes",
                c => new
                    {
                        EventTypeID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.EventTypeID);
            
            CreateTable(
                "dbo.Moods",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.NationalDays",
                c => new
                    {
                        NationID = c.Int(nullable: false),
                        SpecialDayID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.NationID, t.SpecialDayID })
                .ForeignKey("dbo.Nations", t => t.NationID, cascadeDelete: true)
                .ForeignKey("dbo.SpecialDays", t => t.SpecialDayID, cascadeDelete: true)
                .Index(t => t.NationID)
                .Index(t => t.SpecialDayID);
            
            CreateTable(
                "dbo.SpecialDays",
                c => new
                    {
                        SpecialDayID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.SpecialDayID);
            
            CreateTable(
                "dbo.TaskStates",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserEvents",
                c => new
                    {
                        UserEventID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Location = c.String(maxLength: 50),
                        AllDay = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Descriptions = c.String(maxLength: 200),
                        UserID = c.String(maxLength: 128),
                        EvenTypeID = c.Int(nullable: false),
                        PriorityID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserEventID)
                .ForeignKey("dbo.EventPriorities", t => t.PriorityID, cascadeDelete: true)
                .ForeignKey("dbo.EventTypes", t => t.EvenTypeID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.EvenTypeID)
                .Index(t => t.PriorityID);
            
            CreateTable(
                "dbo.UserMoods",
                c => new
                    {
                        UserMoodID = c.Int(nullable: false, identity: true),
                        UserID = c.String(maxLength: 128),
                        MoodID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserMoodID)
                .ForeignKey("dbo.Moods", t => t.MoodID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.MoodID);
            
            CreateTable(
                "dbo.UserTasks",
                c => new
                    {
                        UserTaskID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        TaskDescription = c.String(maxLength: 200),
                        Active = c.Boolean(nullable: false),
                        UserID = c.String(maxLength: 128),
                        TaskStateID = c.Int(nullable: false),
                        PriorityID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserTaskID)
                .ForeignKey("dbo.EventPriorities", t => t.PriorityID, cascadeDelete: true)
                .ForeignKey("dbo.TaskStates", t => t.TaskStateID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.TaskStateID)
                .Index(t => t.PriorityID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserTasks", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserTasks", "TaskStateID", "dbo.TaskStates");
            DropForeignKey("dbo.UserTasks", "PriorityID", "dbo.EventPriorities");
            DropForeignKey("dbo.UserMoods", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserMoods", "MoodID", "dbo.Moods");
            DropForeignKey("dbo.UserEvents", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserEvents", "EvenTypeID", "dbo.EventTypes");
            DropForeignKey("dbo.UserEvents", "PriorityID", "dbo.EventPriorities");
            DropForeignKey("dbo.NationalDays", "SpecialDayID", "dbo.SpecialDays");
            DropForeignKey("dbo.NationalDays", "NationID", "dbo.Nations");
            DropForeignKey("dbo.Diaries", "UserID", "dbo.Users");
            DropForeignKey("dbo.Users", "NationID", "dbo.Nations");
            DropIndex("dbo.UserTasks", new[] { "PriorityID" });
            DropIndex("dbo.UserTasks", new[] { "TaskStateID" });
            DropIndex("dbo.UserTasks", new[] { "UserID" });
            DropIndex("dbo.UserMoods", new[] { "MoodID" });
            DropIndex("dbo.UserMoods", new[] { "UserID" });
            DropIndex("dbo.UserEvents", new[] { "PriorityID" });
            DropIndex("dbo.UserEvents", new[] { "EvenTypeID" });
            DropIndex("dbo.UserEvents", new[] { "UserID" });
            DropIndex("dbo.NationalDays", new[] { "SpecialDayID" });
            DropIndex("dbo.NationalDays", new[] { "NationID" });
            DropIndex("dbo.Users", new[] { "NationID" });
            DropIndex("dbo.Diaries", new[] { "UserID" });
            DropTable("dbo.UserTasks");
            DropTable("dbo.UserMoods");
            DropTable("dbo.UserEvents");
            DropTable("dbo.TaskStates");
            DropTable("dbo.SpecialDays");
            DropTable("dbo.NationalDays");
            DropTable("dbo.Moods");
            DropTable("dbo.EventTypes");
            DropTable("dbo.EventPriorities");
            DropTable("dbo.Nations");
            DropTable("dbo.Users");
            DropTable("dbo.Diaries");
        }
    }
}
