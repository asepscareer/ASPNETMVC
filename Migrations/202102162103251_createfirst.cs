namespace ASPNETMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createfirst : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tb_Divison", "Division_Name", c => c.String());
            DropColumn("dbo.Tb_Divison", "Divison_Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tb_Divison", "Divison_Name", c => c.String());
            DropColumn("dbo.Tb_Divison", "Division_Name");
        }
    }
}
