using Microsoft.EntityFrameworkCore.Migrations;

namespace SamuraiApp.Data.Migrations
{
    public partial class AddStoresProc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE PROCEDURE FilterSamuraiByNamePart
                    @namepart varchar(50)
                    AS
                    SELECT * FROM Samurais WHERE Name like '%'+@namepart+'%'"
                );
            migrationBuilder.Sql(
                @"CREATE PROCEDURE FindLongestName
                    @procResult varchar(50) OUTPUT
                    AS
                    BEGIN
                    SET NOCOUNT ON;
                    SELECT TOP 1 @procResult = Name FROM Samurais ORDER BY len(Name) DESC
                    END"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
