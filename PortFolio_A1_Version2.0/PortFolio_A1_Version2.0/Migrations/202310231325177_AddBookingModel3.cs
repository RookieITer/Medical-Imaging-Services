namespace PortFolio_A1_Version2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookingModel3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bookings", "StartTime", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Bookings", "EndTime", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Bookings", "EndTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Bookings", "StartTime", c => c.DateTime(nullable: false));
        }
    }
}
