using FluentMigrator;

namespace Location.Data.Migrations
{
    [Migration(1)]
    public class CreateLocationTable : Migration
    {
        public override void Up()
        {
            Create.Table("Location");
            Create.Table("Country").WithColumn("Name").AsString();
        }

        public override void Down()
        {
            Delete.Table("Location");
            Delete.Table("Country");
        }
    }
}
