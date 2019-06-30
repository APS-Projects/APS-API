using FluentMigrator;

namespace MigrationHelper.Migrations
{
    [Migration(20190630133745)]
    public class AddBaseTables_20190630133745 : Migration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            Create.Table("Bar")
                .WithColumn("Id").AsInt64().PrimaryKey().NotNullable()
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("ZipCode").AsInt16().NotNullable()
                .WithColumn("StreetAddress").AsString(300).NotNullable()
                .WithColumn("City").AsString(100).NotNullable()
                .WithColumn("OpenHour").AsDateTime().NotNullable()
                .WithColumn("CloseHour").AsDateTime().NotNullable();

            Create.Table("BarHappyHour")
                .WithColumn("Id").AsInt64().PrimaryKey().NotNullable()
                .WithColumn("BarId").AsInt64().NotNullable()
                .WithColumn("DayOfWeek").AsString(50).NotNullable()
                .WithColumn("StartTime").AsDateTime().NotNullable()
                .WithColumn("EndTime").AsDateTime().NotNullable()
                .WithColumn("Description").AsString(500).NotNullable();

            Create.Table("BarTrivia")
                .WithColumn("Id").AsInt64().PrimaryKey().NotNullable()
                .WithColumn("BarId").AsInt64().NotNullable()
                .WithColumn("DayOfWeek").AsString(50).NotNullable()
                .WithColumn("StartTime").AsDateTime().NotNullable()
                .WithColumn("EndTime").AsDateTime().NotNullable()
                .WithColumn("Description").AsString(500).NotNullable();

            Create.Table("Friends")
                .WithColumn("Id").AsInt64().PrimaryKey().NotNullable()
                .WithColumn("UserId").AsGuid().NotNullable()
                .WithColumn("FriendId").AsGuid().NotNullable();

            Create.Table("User")
                .WithColumn("Id").AsString(250).PrimaryKey().NotNullable()
                .WithColumn("AvatarUrl").AsString(250).Nullable()
                .WithColumn("FirstName").AsString(100).NotNullable()
                .WithColumn("LastName").AsString(100).NotNullable()
                .WithColumn("IsActive").AsBoolean().NotNullable()
                .WithColumn("DefaultZipCode").AsInt16().NotNullable()
                .WithColumn("CurrentZipCode").AsInt16().Nullable()
                .WithColumn("CurrentLocation").AsString(300).Nullable();

            Create.Table("UserFavoriteBar")
                .WithColumn("Id").AsInt64().PrimaryKey().NotNullable()
                .WithColumn("UserId").AsString(250).NotNullable()
                .WithColumn("BarId").AsInt64().NotNullable();
        }
    }
}
