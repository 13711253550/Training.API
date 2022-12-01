using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Training.EFCore.Migrations
{
    public partial class demol26 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OrderState",
                table: "DrugOrder",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "OrderState",
                table: "DrugOrder",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
