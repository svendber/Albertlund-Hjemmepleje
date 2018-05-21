namespace Albertlund_Hjemmepleje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Log : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.People");
            AddColumn("dbo.People", "phone", c => c.String(nullable: false));
            AlterColumn("dbo.People", "email", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.People", "email");
            DropColumn("dbo.People", "iD");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "iD", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.People");
            AlterColumn("dbo.People", "email", c => c.String(nullable: false));
            DropColumn("dbo.People", "phone");
            AddPrimaryKey("dbo.People", "iD");
        }
    }
}
