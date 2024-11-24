using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryPatternWithUOW.EF.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDateTimeAndUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LastUpdatedByUserId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDateTime",
                table: "Products",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdatedByUserId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LastUpdatedDateTime",
                table: "Products");
        }
    }
}
