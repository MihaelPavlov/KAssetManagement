using FluentMigrator;

namespace Location.Data.Migrations
{
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
                .WithColumn("event_Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("id").AsInt32().NotNullable()
                .WithColumn("created_by").AsInt32().NotNullable()
                .WithColumn("created_date").AsDate().NotNullable()
                .WithColumn("action").AsString(255).NotNullable();

            Create.Table("location_values_audit")
                .WithColumn("event_Id").AsInt32().NotNullable().ForeignKey("fk_locationValues_locationEvents", "location_events_audit", "event_Id")
                .WithColumn("field").AsString(255).NotNullable()
                .WithColumn("old_Value").AsString(255)
                .WithColumn("new_Value").AsString(255);

            Execute.Sql(@"
CREATE FUNCTION  location_event_after_delete() RETURNS TRIGGER AS $location_events_audit$
   BEGIN
        INSERT INTO location_events_audit (id,created_by,created_date,action) SELECT OLD.id, OLD.updated_by, current_timestamp, 'DELETE';
		RETURN NULL;
   END;
$location_events_audit$ LANGUAGE plpgsql;
");

            Execute.Sql(@"
CREATE TRIGGER location_after_delete
    AFTER DELETE ON location
    FOR EACH ROW
    EXECUTE FUNCTION location_event_after_delete();
");
        }

        public override void Down()
        {
            Delete.Table("country");
            Delete.Table("city");
            Delete.Table("location");
            Delete.Table("location_events_audi");
            Delete.Table("location_values_audit");
            Execute.Sql(@"DROP TRIGGER IF EXISTS location_event_after_delete ON location;");
            Execute.Sql(@"DROP FUNCTION IF EXISTS insert_after_delete;");
        }
    }
}
