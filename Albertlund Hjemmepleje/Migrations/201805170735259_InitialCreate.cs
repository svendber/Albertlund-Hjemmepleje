namespace Albertlund_Hjemmepleje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.People",
                c => new
                    {
                        iD = c.Int(nullable: false, identity: true),
                        email = c.String(),
                        name = c.String(),
                        password = c.String(),
                        role = c.String(),
                        occupation = c.String(),
                    })
                .PrimaryKey(t => t.iD);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.People");
        }
    }
}
