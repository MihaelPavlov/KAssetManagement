using FluentMigrator;

namespace Location.Data.Migrations
{
    [Migration(2)]
    public class CreateTestMigration : Migration
    {
        public override void Up()
        {
            Create.Table("Test").WithColumn("Name").AsString();
        }

        public override void Down()
        {
            Delete.Table("Test");
        }
    }
}
