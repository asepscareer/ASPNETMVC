namespace ASPNETMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createfirst1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Tb_Divison", newName: "Tb_Division");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Tb_Division", newName: "Tb_Divison");
        }
    }
}
