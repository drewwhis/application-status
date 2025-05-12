using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationStatus.Data.Migrations
{
    /// <inheritdoc />
    public partial class PrefixField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ApiKeyHash",
                table: "ApiUsers",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AddColumn<string>(
                name: "Prefix",
                table: "ApiUsers",
                type: "varchar(7)",
                maxLength: 7,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ApiUsers_Prefix_ApiKeyHash",
                table: "ApiUsers",
                columns: new[] { "Prefix", "ApiKeyHash" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ApiUsers_Prefix_ApiKeyHash",
                table: "ApiUsers");

            migrationBuilder.DropColumn(
                name: "Prefix",
                table: "ApiUsers");

            migrationBuilder.AlterColumn<string>(
                name: "ApiKeyHash",
                table: "ApiUsers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");
        }
    }
}
