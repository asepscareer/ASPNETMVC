namespace ASPNETMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tb_M_Employee", "JoinDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tb_M_Employee", "JoinDate", c => c.DateTime(nullable: false));
        }
    }
}
