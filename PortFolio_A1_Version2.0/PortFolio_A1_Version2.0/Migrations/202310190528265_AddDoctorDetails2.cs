namespace PortFolio_A1_Version2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDoctorDetails2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DoctorDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DoctorName = c.String(nullable: false, maxLength: 100),
                        PhoneNumber = c.String(nullable: false, maxLength: 15),
                        Description = c.String(nullable: false, maxLength: 500),
                        WorkingHours = c.String(nullable: false, maxLength: 8),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DoctorDetails");
        }
    }
}
