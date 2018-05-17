namespace Albertlund_Hjemmepleje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class boooh : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.People", "email", c => c.String(nullable: false));
            AlterColumn("dbo.People", "name", c => c.String(nullable: false));
            AlterColumn("dbo.People", "password", c => c.String(nullable: false));
            AlterColumn("dbo.People", "role", c => c.Boolean(nullable: false));
            AlterColumn("dbo.People", "occupation", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.People", "occupation", c => c.String());
            AlterColumn("dbo.People", "role", c => c.String());
            AlterColumn("dbo.People", "password", c => c.String());
            AlterColumn("dbo.People", "name", c => c.String());
            AlterColumn("dbo.People", "email", c => c.String());
        }
    }
}
