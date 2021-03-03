using Microsoft.EntityFrameworkCore.Migrations;

namespace app1.Data.Migrations
{
    public partial class procedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string Proc = @"Create Procedure ProcedurStk as begin Select * From ApplicationUser End";
            migrationBuilder.Sql(Proc);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string Proc = @"Create Procedure ProcedurStk";
            migrationBuilder.Sql(Proc);
        }
    }
}
