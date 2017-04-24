namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMoreGenres : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (Id, Name) VALUES (5, 'Alternative')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (6, 'Hardcore')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (7, 'Shoegaze')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (8, 'Acoustic')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (9, 'Neoclassical')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (10, 'Electronic')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (11, 'Hip-Hop/Rap')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (12, 'Indie/Pop')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (13, 'Experimental')");
        }

        public override void Down()
        {
        }
    }
}
