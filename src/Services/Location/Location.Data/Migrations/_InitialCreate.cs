namespace Location.Data.Migrations
{
    using FluentMigrator;

    [Migration(1)]
    public class _InitialCreate : Migration
    {
        public override void Up()
        {
            Create.Table("country")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("name").AsString(255).NotNullable();

            Create.Table("city")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("name").AsString(255).NotNullable()
                .WithColumn("country_Id").AsInt32().NotNullable().ForeignKey("fk_city_country", "country", "id");

            Create.Table("location")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("code").AsString(255).NotNullable()
                .WithColumn("latitude").AsString(255).NotNullable()
                .WithColumn("longitude").AsString(255).NotNullable()
                .WithColumn("country_id").AsInt32().NotNullable().ForeignKey("fk_location_country", "country", "id")
                .WithColumn("city_id").AsInt32().NotNullable().ForeignKey("fk_location_city", "city", "id")
                .WithColumn("street").AsString(255).NotNullable()
                .WithColumn("street_number").AsInt32().NotNullable()
                .WithColumn("organization_id").AsInt32().Nullable()
                .WithColumn("updated_by").AsInt32().NotNullable();

            Create.Table("location_events_audit")
                .WithColumn("event_id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("id").AsInt32().NotNullable()
                .WithColumn("created_by").AsInt32().NotNullable()
                .WithColumn("created_date").AsDate().NotNullable()
                .WithColumn("action").AsString(255).NotNullable();

            Create.Table("location_values_audit")
                .WithColumn("event_id").AsInt32().NotNullable().ForeignKey("fk_locationValues_locationEvents", "location_events_audit", "event_id")
                .WithColumn("field").AsString(255).NotNullable()
                .WithColumn("old_value").AsString(255)
                .WithColumn("new_value").AsString(255);
        }

        public override void Down()
        {
            Delete.Table("country");

            Delete.Table("city");

            Delete.Table("location");

            Delete.Table("location_events_audi");

            Delete.Table("location_values_audit");
        }
    }
}
