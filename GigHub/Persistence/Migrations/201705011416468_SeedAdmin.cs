namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedAdmin : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Name]) VALUES (N'6c89ad0c-3025-4094-88d4-d9aadf716b39', N'admin@gighub.com', 0, N'AOPFG2+d/YXuyzKpLobqRsHlU/rch3zDG0kw6UHGPMdPfnMe5hFZRCfFdh03D/4J/w==', N'e2cd0cfd-6237-4bbc-8499-78c746b2025d', NULL, 0, 0, NULL, 1, 0, N'admin@gighub.com', N'Admin')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'7839a9a0-17ea-47f5-870e-bba094fab4c5', N'Admin')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'c9a8b899-afaa-4baa-9f42-3bc65925be79', N'GroupManager')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'6c89ad0c-3025-4094-88d4-d9aadf716b39', N'7839a9a0-17ea-47f5-870e-bba094fab4c5')
            ");

        }
        
        public override void Down()
        {
        }
    }
}
