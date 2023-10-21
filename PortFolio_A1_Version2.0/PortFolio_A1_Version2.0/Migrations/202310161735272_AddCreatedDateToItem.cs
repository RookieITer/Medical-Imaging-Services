namespace PortFolio_A1_Version2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreatedDateToItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "CreatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "CreatedDate");
        }
    }
}
