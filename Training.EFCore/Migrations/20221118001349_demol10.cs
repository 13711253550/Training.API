using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Training.EFCore.Migrations
{
    public partial class demol10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserSex",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserSex",
                table: "User");
        }
    }
}
