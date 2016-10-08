namespace ZoolandiaRazor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Animals",
                c => new
                    {
                        AnimalId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CommonName = c.String(),
                        ScientificName = c.String(nullable: false),
                        HabitatId = c.Int(nullable: false),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AnimalId)
                .ForeignKey("dbo.Habitats", t => t.HabitatId, cascadeDelete: true)
                .Index(t => t.HabitatId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Habitats",
                c => new
                    {
                        HabitatId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        HabitatTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HabitatId);
            
            CreateTable(
                "dbo.HabitatEmployees",
                c => new
                    {
                        Habitat_HabitatId = c.Int(nullable: false),
                        Employee_EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Habitat_HabitatId, t.Employee_EmployeeId })
                .ForeignKey("dbo.Habitats", t => t.Habitat_HabitatId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeId, cascadeDelete: true)
                .Index(t => t.Habitat_HabitatId)
                .Index(t => t.Employee_EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Animals", "HabitatId", "dbo.Habitats");
            DropForeignKey("dbo.HabitatEmployees", "Employee_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.HabitatEmployees", "Habitat_HabitatId", "dbo.Habitats");
            DropIndex("dbo.HabitatEmployees", new[] { "Employee_EmployeeId" });
            DropIndex("dbo.HabitatEmployees", new[] { "Habitat_HabitatId" });
            DropIndex("dbo.Animals", new[] { "HabitatId" });
            DropTable("dbo.HabitatEmployees");
            DropTable("dbo.Habitats");
            DropTable("dbo.Employees");
            DropTable("dbo.Animals");
        }
    }
}
