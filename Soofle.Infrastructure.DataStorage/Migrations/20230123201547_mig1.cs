using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Soofle.Infrastructure.DataStorage.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AllParticipants",
                table: "LikeReports",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AllParticipants",
                table: "CommentReports",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllParticipants",
                table: "LikeReports");

            migrationBuilder.DropColumn(
                name: "AllParticipants",
                table: "CommentReports");
        }
    }
}
