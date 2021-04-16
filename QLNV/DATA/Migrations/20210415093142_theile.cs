using Microsoft.EntityFrameworkCore.Migrations;

namespace DATA.Migrations
{
    public partial class theile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Nguoigioithieu",
                table: "Nhanviens",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nguoigioithieu",
                table: "Nhanviens");
        }
    }
}
