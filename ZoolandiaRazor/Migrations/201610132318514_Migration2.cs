namespace ZoolandiaRazor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.HabitatEmployees", newName: "EmployeeHabitats");
            RenameColumn(table: "dbo.Animals", name: "HabitatId", newName: "Habitat_HabitatId");
            RenameIndex(table: "dbo.Animals", name: "IX_HabitatId", newName: "IX_Habitat_HabitatId");
            DropPrimaryKey("dbo.EmployeeHabitats");
            AddPrimaryKey("dbo.EmployeeHabitats", new[] { "Employee_EmployeeId", "Habitat_HabitatId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.EmployeeHabitats");
            AddPrimaryKey("dbo.EmployeeHabitats", new[] { "Habitat_HabitatId", "Employee_EmployeeId" });
            RenameIndex(table: "dbo.Animals", name: "IX_Habitat_HabitatId", newName: "IX_HabitatId");
            RenameColumn(table: "dbo.Animals", name: "Habitat_HabitatId", newName: "HabitatId");
            RenameTable(name: "dbo.EmployeeHabitats", newName: "HabitatEmployees");
        }
    }
}
