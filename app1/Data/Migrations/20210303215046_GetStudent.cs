using Microsoft.EntityFrameworkCore.Migrations;

namespace app1.Data.Migrations
{
    public partial class GetStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            var sp = @"CREATE PROCEDURE [dbo].[GetStudentPro]
                AS
                BEGIN
                    select * from AspNetUsers END";

            migrationBuilder.Sql(sp);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
