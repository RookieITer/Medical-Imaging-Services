namespace PortFolio_A1_Version2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DoctorInstance1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "Specialization", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctors", "Specialization");
        }
    }
}
