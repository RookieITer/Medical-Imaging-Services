namespace PortFolio_A1_Version2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteBookingID : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Items", "BookingID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "BookingID", c => c.Guid(nullable: false));
        }
    }
}
