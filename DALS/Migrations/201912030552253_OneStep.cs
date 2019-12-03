namespace DALS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OneStep : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShortUrls",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ShortUrlPath = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Urls", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Urls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullUrl = c.String(),
                        ShortUrlPath = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        ClickCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShortUrls", "Id", "dbo.Urls");
            DropIndex("dbo.ShortUrls", new[] { "Id" });
            DropTable("dbo.Urls");
            DropTable("dbo.ShortUrls");
        }
    }
}
