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
                .WithColumn("Id").AsInt64().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("CityId").AsInt16().NotNullable();

            Create.Table("Cities")
                .WithColumn("Id").AsInt16().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString(100).NotNullable();

            Create.Table("DayOfWeek")
                .WithColumn("Id").AsInt16().PrimaryKey().NotNullable()
                .WithColumn("Day").AsString(50).NotNullable();

            Create.Table("CityZipCodes")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity().NotNullable()
                .WithColumn("CityId").AsInt16().NotNullable()
                .WithColumn("ZipCode").AsInt16().NotNullable();

            Create.Table("BarHappyHour")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity().NotNullable()
                .WithColumn("BarId").AsInt64().NotNullable()
                .WithColumn("DayOfWeekId").AsString(50).NotNullable()
                .WithColumn("CityId").AsInt16().NotNullable()
                .WithColumn("Description").AsString(500).NotNullable();

            Create.Table("BarTrivia")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity().NotNullable()
                .WithColumn("BarId").AsInt64().NotNullable()
                .WithColumn("DayOfWeekId").AsString(50).NotNullable()
                .WithColumn("CityId").AsInt16().NotNullable()
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
