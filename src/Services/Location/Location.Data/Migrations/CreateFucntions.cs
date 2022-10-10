namespace Location.Data.Migrations
{
    using FluentMigrator;

    [Migration(3)]
    public class CreateFucntions : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"
CREATE FUNCTION get_location_by_id(
  location_id integer
) 
	RETURNS TABLE (
		id integer,
		code varchar,
		longitude varchar,
		latitude varchar,
		countryId integer,
		countryName varchar,
		cityId integer,
		cityName varchar,
		street varchar,
		streetNumber integer,
		organizationId integer
	) 
	LANGUAGE plpgsql
AS $$
BEGIN
	RETURN QUERY 
		SELECT
			l.id ,l.code, l.longitude, l.latitude, l.country_id, ct.name, l.city_id, ci.name, l.street, l.street_number, l.organization_id
		FROM location l
		JOIN country ct ON ct.id = l.country_id
		JOIN city ci ON  ci.id = l.city_id
		WHERE l.id = location_id;
END;$$
");

            Execute.Sql(@"
CREATE FUNCTION get_all_locations_by_organization_id(
  f_organization_id integer
) 
	RETURNS TABLE (
		id integer,
		code varchar,
		longitude varchar,
		latitude varchar,
		countryId int,
		countryName varchar,
		cityId int,
		cityName varchar,
		street varchar,
		streetNumber integer,
		organizationId integer
	)
	LANGUAGE plpgsql
AS $$
BEGIN
		RETURN QUERY 
		SELECT
			l.id ,l.code, l.longitude, l.latitude, l.country_id, ct.name, l.city_id, ci.name, l.street, l.street_number, l.organization_id
		FROM location l
		JOIN country ct ON ct.id = l.country_id
		JOIN city ci ON  ci.id = l.city_id
		WHERE l.organization_id = f_organization_id;
END;$$

");
        }

        public override void Down()
        {
            Execute.Sql(@"DROP FUNCTION IF EXISTS get_location_by_id;");

            Execute.Sql(@"DROP FUNCTION IF EXISTS get_all_locations_by_organization_id;");
        }
    }
}
