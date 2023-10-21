namespace PortFolio_A1_Version2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateFieldsToItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "LastModifiedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "LastModifiedDate");
        }
    }
}
