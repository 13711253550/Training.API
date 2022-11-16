using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Training.EFCore.Migrations
{
    public partial class demol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "actual_amount",
                table: "Orderdetail");

            migrationBuilder.DropColumn(
                name: "consignee",
                table: "Orderdetail");

            migrationBuilder.DropColumn(
                name: "consignee_address",
                table: "Orderdetail");

            migrationBuilder.DropColumn(
                name: "consignee_phone",
                table: "Orderdetail");

            migrationBuilder.DropColumn(
                name: "drug_image",
                table: "Orderdetail");

            migrationBuilder.DropColumn(
                name: "drug_introduction",
                table: "Orderdetail");

            migrationBuilder.DropColumn(
                name: "drug_specification",
                table: "Orderdetail");

            migrationBuilder.DropColumn(
                name: "payable_amount",
                table: "Orderdetail");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "Orderdetail",
                newName: "UserId");

            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "User",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "User",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "phone",
                table: "User",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "logistics_number",
                table: "Orderdetail",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "DrugId",
                table: "Orderdetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orderdetail_DrugId",
                table: "Orderdetail",
                column: "DrugId");

            migrationBuilder.CreateIndex(
                name: "IX_Orderdetail_UserId",
                table: "Orderdetail",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orderdetail_Drug_DrugId",
                table: "Orderdetail",
                column: "DrugId",
                principalTable: "Drug",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orderdetail_User_UserId",
                table: "Orderdetail",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orderdetail_Drug_DrugId",
                table: "Orderdetail");

            migrationBuilder.DropForeignKey(
                name: "FK_Orderdetail_User_UserId",
                table: "Orderdetail");

            migrationBuilder.DropIndex(
                name: "IX_Orderdetail_DrugId",
                table: "Orderdetail");

            migrationBuilder.DropIndex(
                name: "IX_Orderdetail_UserId",
                table: "Orderdetail");

            migrationBuilder.DropColumn(
                name: "address",
                table: "User");

            migrationBuilder.DropColumn(
                name: "name",
                table: "User");

            migrationBuilder.DropColumn(
                name: "phone",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DrugId",
                table: "Orderdetail");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Orderdetail",
                newName: "User_Id");

            migrationBuilder.UpdateData(
                table: "Orderdetail",
                keyColumn: "logistics_number",
                keyValue: null,
                column: "logistics_number",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "logistics_number",
                table: "Orderdetail",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<decimal>(
                name: "actual_amount",
                table: "Orderdetail",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "consignee",
                table: "Orderdetail",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "consignee_address",
                table: "Orderdetail",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "consignee_phone",
                table: "Orderdetail",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "drug_image",
                table: "Orderdetail",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "drug_introduction",
                table: "Orderdetail",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "drug_specification",
                table: "Orderdetail",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<decimal>(
                name: "payable_amount",
                table: "Orderdetail",
                type: "decimal(65,30)",
                nullable: true);
        }
    }
}
