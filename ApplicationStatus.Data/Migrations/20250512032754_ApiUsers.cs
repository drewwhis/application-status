using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationStatus.Data.Migrations
{
    /// <inheritdoc />
    public partial class ApiUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    ApplicationName = table.Column<string>(type: "varchar(255)", nullable: false),
                    ApiKeyHash = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiUsers", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ApiUsers_ApplicationName",
                table: "ApiUsers",
                column: "ApplicationName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiUsers");
        }
    }
}
