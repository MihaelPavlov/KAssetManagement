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

            Execute.Sql(@"
CREATE FUNCTION  location_event_after_insert() RETURNS TRIGGER AS $location_events_audit$
   BEGIN
        INSERT INTO location_events_audit (id,created_by,created_date,action) SELECT NEW.id, NEW.updated_by, current_timestamp, 'INSERT';
		RETURN NULL;
   END;
$location_events_audit$ LANGUAGE plpgsql;
");

            Execute.Sql(@"
CREATE TRIGGER location_after_insert
    AFTER INSERT ON location
    FOR EACH ROW
    EXECUTE FUNCTION location_event_after_insert();
");

            Execute.Sql(@"
CREATE FUNCTION  location_event_after_update() RETURNS TRIGGER AS $location_events_audit$
   	DECLARE v_event_id integer;
BEGIN	
		IF NOT(
			OLD.code IS DISTINCT FROM NEW.code OR 
			OLD.latitude IS DISTINCT FROM NEW.latitude OR
			OLD.longitude IS DISTINCT FROM NEW.longitude OR
		   	OLD.country_id IS DISTINCT FROM NEW.country_id OR
		   	OLD.city_id IS DISTINCT FROM NEW.city_id OR
		    OLD.street IS DISTINCT FROM NEW.street OR
		   	OLD.street_number IS  DISTINCT FROM NEW.street_number OR
		   	OLD.organization_id IS  DISTINCT FROM NEW.organization_id ) THEN
		RETURN NULL;
		END IF;
		
        INSERT INTO location_events_audit (id,created_by,created_date,action) 
		SELECT NEW.id, NEW.updated_by, current_timestamp, 'UPDATE';

  		SELECT currval(pg_get_serial_sequence('location_events_audit','event_id')) INTO v_event_id;

		INSERT INTO location_values_audit(event_id, field, old_value, new_value) 
		SELECT v_event_id, 'code', CAST(OLD.code AS varchar), CAST(NEW.code AS varchar)
		WHERE NEW.code != OLD.code;		
				
		INSERT INTO location_values_audit(event_id, field, old_value, new_value) 
		SELECT v_event_id, 'latitude', CAST(OLD.latitude AS varchar), CAST(NEW.latitude AS varchar)
		WHERE NEW.latitude != OLD.latitude;
		
		INSERT INTO location_values_audit(event_id, field, old_value, new_value) 
		SELECT v_event_id, 'longitude', CAST(OLD.longitude AS varchar), CAST(NEW.longitude AS varchar)
		WHERE NEW.longitude != OLD.longitude;
		
		INSERT INTO location_values_audit(event_id, field, old_value, new_value) 
		SELECT v_event_id, 'country_id', CAST(OLD.country_id AS varchar), CAST(NEW.country_id AS varchar)
		WHERE NEW.country_id != OLD.country_id;
		
		INSERT INTO location_values_audit(event_id, field, old_value, new_value) 
		SELECT v_event_id, 'city_id', CAST(OLD.city_id AS varchar), CAST(NEW.city_id AS varchar)
		WHERE NEW.city_id != OLD.city_id;
		
		INSERT INTO location_values_audit(event_id, field, old_value, new_value) 
		SELECT v_event_id, 'street', CAST(OLD.street AS varchar), CAST(NEW.street AS varchar)
		WHERE NEW.street != OLD.street;
		
		INSERT INTO location_values_audit(event_id, field, old_value, new_value) 
		SELECT v_event_id, 'street_number', CAST(OLD.street_number AS varchar), CAST(NEW.street_number AS varchar)
		WHERE NEW.street_number != OLD.street_number;
		
		INSERT INTO location_values_audit(event_id, field, old_value, new_value) 
		SELECT v_event_id, 'organization_id', CAST(OLD.organization_id AS varchar), CAST(NEW.organization_id AS varchar)
		WHERE NEW.organization_id != OLD.organization_id;
		
	RETURN NULL;
END;
$location_events_audit$ LANGUAGE plpgsql;
");

            Execute.Sql(@"
CREATE TRIGGER location_after_update
    AFTER UPDATE ON location
    FOR EACH ROW
    EXECUTE FUNCTION location_event_after_update();
");
        }

        public override void Down()
        {
            Execute.Sql(@"DROP TRIGGER IF EXISTS location_after_delete ON location;");

            Execute.Sql(@"DROP FUNCTION IF EXISTS location_event_after_delete;");

            Execute.Sql(@"DROP TRIGGER IF EXISTS location_after_insert ON location;");

            Execute.Sql(@"DROP FUNCTION IF EXISTS location_event_after_insert;");

            Execute.Sql(@"DROP TRIGGER IF EXISTS location_after_update ON location;");

            Execute.Sql(@"DROP FUNCTION IF EXISTS location_event_after_update;");

            Delete.Table("country");

            Delete.Table("city");

            Delete.Table("location");

            Delete.Table("location_events_audi");

            Delete.Table("location_values_audit");
        }
    }
}
