namespace ASPNETMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tb_Account",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tb_M_Employee", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Tb_M_Employee",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        JoinDate = c.DateTime(nullable: false),
                        Email = c.String(),
                        Phone = c.String(),
                        Address = c.String(),
                        DivisionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tb_Divison", t => t.DivisionID, cascadeDelete: true)
                .Index(t => t.DivisionID);
            
            CreateTable(
                "dbo.Tb_Divison",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Divison_Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tb_Account", "Id", "dbo.Tb_M_Employee");
            DropForeignKey("dbo.Tb_M_Employee", "DivisionID", "dbo.Tb_Divison");
            DropIndex("dbo.Tb_M_Employee", new[] { "DivisionID" });
            DropIndex("dbo.Tb_Account", new[] { "Id" });
            DropTable("dbo.Tb_Divison");
            DropTable("dbo.Tb_M_Employee");
            DropTable("dbo.Tb_Account");
        }
    }
}
