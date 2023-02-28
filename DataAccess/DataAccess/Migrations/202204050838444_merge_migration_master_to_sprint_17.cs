namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class merge_migration_master_to_sprint_17 : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.IPDApgars",
            //    c => new
            //        {
            //            Id = c.Guid(nullable: false),
            //            VisitId = c.Guid(nullable: false),
            //            ApgarScore = c.String(),
            //            Total = c.String(),
            //            SkinColor = c.String(),
            //            Respiration = c.String(),
            //            Reflex = c.String(),
            //            MuscleTone = c.String(),
            //            HeartRate = c.String(),
            //            Order = c.Int(nullable: false, identity: true),
            //            IsDeleted = c.Boolean(nullable: false),
            //            DeletedBy = c.String(),
            //            DeletedAt = c.DateTime(),
            //            CreatedBy = c.String(),
            //            CreatedAt = c.DateTime(),
            //            UpdatedBy = c.String(),
            //            UpdatedAt = c.DateTime(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.IPDAssesseds",
            //    c => new
            //        {
            //            Id = c.Guid(nullable: false),
            //            VisitId = c.Guid(nullable: false),
            //            StaffName = c.String(),
            //            Time = c.String(),
            //            IntervalsFromTimeOfBirth = c.String(),
            //            PositionOfBaby = c.String(),
            //            Colour = c.String(),
            //            HeartRate = c.String(),
            //            Respirations = c.String(),
            //            Temperature = c.String(),
            //            Tone = c.String(),
            //            Activity = c.String(),
            //            FeedingCode = c.String(),
            //            Location = c.String(),
            //            Note = c.String(),
            //            Order = c.Int(nullable: false, identity: true),
            //            IsDeleted = c.Boolean(nullable: false),
            //            DeletedBy = c.String(),
            //            DeletedAt = c.DateTime(),
            //            CreatedBy = c.String(),
            //            CreatedAt = c.DateTime(),
            //            UpdatedBy = c.String(),
            //            UpdatedAt = c.DateTime(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.IPDDeliveryMethods",
            //    c => new
            //        {
            //            Id = c.Guid(nullable: false),
            //            VisitId = c.Guid(nullable: false),
            //            Year = c.String(),
            //            Name = c.String(),
            //            Order = c.Int(nullable: false, identity: true),
            //            IsDeleted = c.Boolean(nullable: false),
            //            DeletedBy = c.String(),
            //            DeletedAt = c.DateTime(),
            //            CreatedBy = c.String(),
            //            CreatedAt = c.DateTime(),
            //            UpdatedBy = c.String(),
            //            UpdatedAt = c.DateTime(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.IPDInitialAssessmentForNewborns",
            //    c => new
            //        {
            //            Id = c.Guid(nullable: false),
            //            VisitId = c.Guid(),
            //            RoomId = c.String(),
            //            MedicalStaff1ConfirmId = c.Guid(),
            //            MedicalStaff1ConfirmAt = c.DateTime(),
            //            MedicalStaff2ConfirmId = c.Guid(),
            //            MedicalStaff2ConfirmAt = c.DateTime(),
            //            IsDeleted = c.Boolean(nullable: false),
            //            DeletedBy = c.String(),
            //            DeletedAt = c.DateTime(),
            //            CreatedBy = c.String(),
            //            CreatedAt = c.DateTime(),
            //            UpdatedBy = c.String(),
            //            UpdatedAt = c.DateTime(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.IPDInitialAssessmentForPediatricInpatients",
            //    c => new
            //        {
            //            Id = c.Guid(nullable: false),
            //            VisitId = c.Guid(),
            //            RoomId = c.String(),
            //            IsDeleted = c.Boolean(nullable: false),
            //            DeletedBy = c.String(),
            //            DeletedAt = c.DateTime(),
            //            CreatedBy = c.String(),
            //            CreatedAt = c.DateTime(),
            //            UpdatedBy = c.String(),
            //            UpdatedAt = c.DateTime(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.IPDLabours",
            //    c => new
            //        {
            //            Id = c.Guid(nullable: false),
            //            VisitId = c.Guid(nullable: false),
            //            Stage = c.String(),
            //            Content = c.String(),
            //            Order = c.Int(nullable: false, identity: true),
            //            IsDeleted = c.Boolean(nullable: false),
            //            DeletedBy = c.String(),
            //            DeletedAt = c.DateTime(),
            //            CreatedBy = c.String(),
            //            CreatedAt = c.DateTime(),
            //            UpdatedBy = c.String(),
            //            UpdatedAt = c.DateTime(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.IPDMedicalRecordOfPatients",
            //    c => new
            //        {
            //            Id = c.Guid(nullable: false),
            //            VisitId = c.Guid(nullable: false),
            //            FormCode = c.String(maxLength: 50),
            //            FormId = c.Guid(),
            //            IsDeleted = c.Boolean(nullable: false),
            //            DeletedBy = c.String(),
            //            DeletedAt = c.DateTime(),
            //            CreatedBy = c.String(),
            //            CreatedAt = c.DateTime(),
            //            UpdatedBy = c.String(),
            //            UpdatedAt = c.DateTime(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //AddColumn("dbo.EDs", "UnlockFor", c => c.String());
            //AddColumn("dbo.EOCs", "UnlockFor", c => c.String());
            //AddColumn("dbo.IPDs", "UnlockFor", c => c.String());
            //AddColumn("dbo.OPDs", "UnlockFor", c => c.String());
            //AddColumn("dbo.UnlockVips", "GroupId", c => c.Guid(nullable: false));
            //AddColumn("dbo.UnlockVips", "Username", c => c.String());
            //AddColumn("dbo.UnlockVips", "Note", c => c.String());
            //AddColumn("dbo.UnlockVips", "Files", c => c.String());
        }
        
        public override void Down()
        {
            //DropColumn("dbo.UnlockVips", "Files");
            //DropColumn("dbo.UnlockVips", "Note");
            //DropColumn("dbo.UnlockVips", "Username");
            //DropColumn("dbo.UnlockVips", "GroupId");
            //DropColumn("dbo.OPDs", "UnlockFor");
            //DropColumn("dbo.IPDs", "UnlockFor");
            //DropColumn("dbo.EOCs", "UnlockFor");
            //DropColumn("dbo.EDs", "UnlockFor");
            //DropTable("dbo.IPDMedicalRecordOfPatients");
            //DropTable("dbo.IPDLabours");
            //DropTable("dbo.IPDInitialAssessmentForPediatricInpatients");
            //DropTable("dbo.IPDInitialAssessmentForNewborns");
            //DropTable("dbo.IPDDeliveryMethods");
            //DropTable("dbo.IPDAssesseds");
            //DropTable("dbo.IPDApgars");
        }
    }
}
