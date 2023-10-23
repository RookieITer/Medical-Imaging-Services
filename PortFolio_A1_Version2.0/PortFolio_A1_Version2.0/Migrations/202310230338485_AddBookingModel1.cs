namespace PortFolio_A1_Version2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookingModel1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingId = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Description = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.BookingId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Bookings", new[] { "UserId" });
            DropTable("dbo.Bookings");
        }
    }
}
