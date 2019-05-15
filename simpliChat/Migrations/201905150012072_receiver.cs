namespace simpliChat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class receiver : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SC_Receivers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 75),
                        IPAdress = c.String(nullable: false, maxLength: 50),
                        History = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SC_Receivers");
        }
    }
}
