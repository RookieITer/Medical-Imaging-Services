namespace PortFolio_A1_Version2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDoctorRating2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        DoctorId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DoctorId);
            
            CreateIndex("dbo.DoctorRatings", "DoctorId");
            AddForeignKey("dbo.DoctorRatings", "DoctorId", "dbo.Doctors", "DoctorId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DoctorRatings", "DoctorId", "dbo.Doctors");
            DropIndex("dbo.DoctorRatings", new[] { "DoctorId" });
            DropTable("dbo.Doctors");
        }
    }
}
