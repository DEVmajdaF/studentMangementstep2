using Microsoft.EntityFrameworkCore.Migrations;

namespace app1.Data.Migrations
{
    public partial class proc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string Proc = @"Create Procedure ProcStudent as begin Select * From AspNetUsers End";
            migrationBuilder.Sql(Proc);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string Proc = @"Create Procedure ProcStudent";
            migrationBuilder.Sql(Proc);
        }
    }
}
