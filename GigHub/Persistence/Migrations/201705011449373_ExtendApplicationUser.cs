namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtendApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsGroupRepresentative", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "ArtistInfo", c => c.String());
            AddColumn("dbo.AspNetUsers", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Image");
            DropColumn("dbo.AspNetUsers", "ArtistInfo");
            DropColumn("dbo.AspNetUsers", "IsGroupRepresentative");
        }
    }
}
