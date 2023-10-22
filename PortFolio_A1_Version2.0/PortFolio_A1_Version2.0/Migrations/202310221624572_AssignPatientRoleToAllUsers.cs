namespace PortFolio_A1_Version2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssignPatientRoleToAllUsers : DbMigration
    {
        public override void Up()
        {
            // Ensure the Patient role exists
            Sql("IF NOT EXISTS (SELECT 1 FROM AspNetRoles WHERE Name = 'Patient') INSERT INTO AspNetRoles (Id, Name) VALUES (NEWID(), 'Patient')");

            // Assign the Patient role to all users who don't have it yet
            Sql(@"
            INSERT INTO AspNetUserRoles (UserId, RoleId)
            SELECT u.Id, r.Id
            FROM AspNetUsers u
            CROSS JOIN AspNetRoles r
            WHERE r.Name = 'Patient'
            AND NOT EXISTS (SELECT 1 FROM AspNetUserRoles ur WHERE ur.UserId = u.Id AND ur.RoleId = r.Id)
            ");


        }

        public override void Down()
        {
        }
    }
}
