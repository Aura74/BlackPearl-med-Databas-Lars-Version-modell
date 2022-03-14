using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlackPearl.Migrations
{
    public partial class two : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PearlID",
                table: "PearlLists",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PearlID",
                table: "PearlLists");
        }
    }
}
