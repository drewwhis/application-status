using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationStatus.Data.Migrations
{
    /// <inheritdoc />
    public partial class UniqueIndexApplicationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ApplicationName",
                table: "Heartbeats",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.CreateIndex(
                name: "IX_Heartbeats_ApplicationName",
                table: "Heartbeats",
                column: "ApplicationName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Heartbeats_ApplicationName",
                table: "Heartbeats");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationName",
                table: "Heartbeats",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");
        }
    }
}
