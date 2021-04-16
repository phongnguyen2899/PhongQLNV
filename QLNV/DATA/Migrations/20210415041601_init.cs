using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DATA.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chucvus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tenchucvu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chucvus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vitris",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tenvitri = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vitris", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nhanviens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CV = table.Column<string>(nullable: true),
                    Ten = table.Column<string>(nullable: true),
                    Ngaysinhh = table.Column<DateTime>(nullable: false),
                    Diachi = table.Column<string>(nullable: true),
                    SDT = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ChucvuId = table.Column<int>(nullable: false),
                    VitriId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nhanviens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nhanviens_Chucvus_ChucvuId",
                        column: x => x.ChucvuId,
                        principalTable: "Chucvus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Nhanviens_Vitris_VitriId",
                        column: x => x.VitriId,
                        principalTable: "Vitris",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nhanviens_ChucvuId",
                table: "Nhanviens",
                column: "ChucvuId");

            migrationBuilder.CreateIndex(
                name: "IX_Nhanviens_VitriId",
                table: "Nhanviens",
                column: "VitriId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nhanviens");

            migrationBuilder.DropTable(
                name: "Chucvus");

            migrationBuilder.DropTable(
                name: "Vitris");
        }
    }
}
