namespace PortFolio_A1_Version2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateValidationForItems : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Items", "PhoneNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Items", "PhoneNumber", c => c.String(nullable: false, maxLength: 15));
        }
    }
}
