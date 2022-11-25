using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Training.EFCore.Migrations
{
    public partial class demol16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserAge",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserAge",
                table: "Clinical_Reception",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserSex",
                table: "Clinical_Reception",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "Clinical_Reception",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "phone",
                table: "Clinical_Reception",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserAge",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserAge",
                table: "Clinical_Reception");

            migrationBuilder.DropColumn(
                name: "UserSex",
                table: "Clinical_Reception");

            migrationBuilder.DropColumn(
                name: "address",
                table: "Clinical_Reception");

            migrationBuilder.DropColumn(
                name: "phone",
                table: "Clinical_Reception");
        }
    }
}
