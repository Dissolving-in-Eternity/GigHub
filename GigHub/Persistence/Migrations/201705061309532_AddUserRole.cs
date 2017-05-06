namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddUserRole : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'b65c3583-a288-4433-a79f-215b8c748432', N'User')");
        }
        
        public override void Down()
        {
        }
    }
}
