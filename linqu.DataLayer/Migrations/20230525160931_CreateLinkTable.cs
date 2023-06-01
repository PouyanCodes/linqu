using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace linqu.DataLayer.Migrations
{
    public partial class CreateLinkTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Links",
                columns: table => new
                {
                    LinkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkOwnerId = table.Column<int>(type: "int", nullable: false),
                    LinkTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LinkAddress = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    ShortKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Links", x => x.LinkId);
                    table.ForeignKey(
                        name: "FK_Links_Users_LinkOwnerId",
                        column: x => x.LinkOwnerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Links_LinkOwnerId",
                table: "Links",
                column: "LinkOwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Links");
        }
    }
}
