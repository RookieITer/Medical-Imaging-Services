namespace PortFolio_A1_Version2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDoctorRating3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DoctorRatings", "RatingDate", c => c.DateTime());
            DropColumn("dbo.DoctorRatings", "Timestamp");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DoctorRatings", "Timestamp", c => c.DateTime(nullable: false));
            DropColumn("dbo.DoctorRatings", "RatingDate");
        }
    }
}
