namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddCityToGigs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Gigs", "City", c => c.String(nullable: false, maxLength: 255));

            Sql("INSERT INTO Genres (Id, Name) VALUES (14, 'Post-hardcore')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (15, 'Deathcore')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (16, 'Metalcore')");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Gigs", "City");
        }
    }
}
