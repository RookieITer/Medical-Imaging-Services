namespace PortFolio_A1_Version2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDoctorRating : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DoctorRatings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        DoctorId = c.Int(nullable: false),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DoctorDetails", t => t.DoctorId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.DoctorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DoctorRatings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.DoctorRatings", "DoctorId", "dbo.DoctorDetails");
            DropIndex("dbo.DoctorRatings", new[] { "DoctorId" });
            DropIndex("dbo.DoctorRatings", new[] { "UserId" });
            DropTable("dbo.DoctorRatings");
        }
    }
}
