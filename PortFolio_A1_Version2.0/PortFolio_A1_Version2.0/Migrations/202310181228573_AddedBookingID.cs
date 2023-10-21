namespace PortFolio_A1_Version2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBookingID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.Items", "PhoneNumber", c => c.String(nullable: false, maxLength: 15));
            AddColumn("dbo.Items", "Address", c => c.String(nullable: false, maxLength: 200));
            AddColumn("dbo.Items", "BookingID", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "BookingID");
            DropColumn("dbo.Items", "Address");
            DropColumn("dbo.Items", "PhoneNumber");
            DropColumn("dbo.Items", "Age");
        }
    }
}
