namespace Vidly.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class SeedUsers : DbMigration
  {
    public override void Up()
    {
      Sql(
        @"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'be689d3e-995f-4a36-98b6-a45fdc7da022', N'guest@vidly.com', 0, N'AHDj+sza+30OddV2IMiJxOmuxqRIJ0ujaTFPHZEJfBU6/cRYawES1NKqb/+Kn+cOqA==', N'bcb6028e-102b-42da-bedf-827974749999', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
          INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'db976185-0ff8-468f-b243-a5e71c347318', N'admin@vidly.com', 0, N'AGrxlLkQS6MmJ7AfIU8EaGKruMxnBGLMg1+mbaKjLDEBaVZLDxmKFHtrnLl2p3s1BQ==', N'e27d2b04-7bfa-4d43-93a3-1789044cc1c5', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
          INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'92388550-b9cc-4f0e-b70f-53b175d65237', N'CanManageMovies')
          INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'db976185-0ff8-468f-b243-a5e71c347318', N'92388550-b9cc-4f0e-b70f-53b175d65237')" );
    }

    public override void Down()
    {
    }
  }
}
