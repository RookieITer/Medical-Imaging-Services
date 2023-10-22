namespace PortFolio_A1_Version2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDoctorRating1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DoctorRatings", "DoctorId", "dbo.DoctorDetails");
            DropForeignKey("dbo.DoctorRatings", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.DoctorRatings", new[] { "UserId" });
            DropIndex("dbo.DoctorRatings", new[] { "DoctorId" });
            AddColumn("dbo.DoctorRatings", "Score", c => c.Int(nullable: false));
            AddColumn("dbo.DoctorRatings", "Timestamp", c => c.DateTime(nullable: false));
            AlterColumn("dbo.DoctorRatings", "UserId", c => c.String());
            DropColumn("dbo.DoctorRatings", "Rating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DoctorRatings", "Rating", c => c.Int(nullable: false));
            AlterColumn("dbo.DoctorRatings", "UserId", c => c.String(maxLength: 128));
            DropColumn("dbo.DoctorRatings", "Timestamp");
            DropColumn("dbo.DoctorRatings", "Score");
            CreateIndex("dbo.DoctorRatings", "DoctorId");
            CreateIndex("dbo.DoctorRatings", "UserId");
            AddForeignKey("dbo.DoctorRatings", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.DoctorRatings", "DoctorId", "dbo.DoctorDetails", "Id", cascadeDelete: true);
        }
    }
}
