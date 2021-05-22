using Microsoft.EntityFrameworkCore.Migrations;

namespace MyShop.Data.Migrations
{
    public partial class _ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_Username",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_Username",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "Orders",
                newName: "Username");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Username",
                table: "Orders",
                column: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_Username",
                table: "Orders",
                column: "Username",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_Username",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_Username",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Orders",
                newName: "username");

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Username",
                table: "Orders",
                column: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_Username",
                table: "Orders",
                column: "Username",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
