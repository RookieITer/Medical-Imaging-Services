namespace PortFolio_A1_Version2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateValidationForDoctorDetails : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DoctorDetails", "PhoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.DoctorDetails", "WorkingHours", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DoctorDetails", "WorkingHours", c => c.String(nullable: false, maxLength: 8));
            AlterColumn("dbo.DoctorDetails", "PhoneNumber", c => c.String(nullable: false, maxLength: 15));
        }
    }
}
