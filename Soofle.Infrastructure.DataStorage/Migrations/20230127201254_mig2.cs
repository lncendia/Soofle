using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Soofle.Infrastructure.DataStorage.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Vks_VkId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Vks");

            migrationBuilder.DropIndex(
                name: "IX_Users_VkId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VkId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "ChatId",
                table: "Users",
                newName: "Target");

            migrationBuilder.AddColumn<string>(
                name: "AccessToken",
                table: "Users",
                type: "nvarchar(500)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProxyId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "TargetSetTime",
                table: "Users",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VkName",
                table: "Users",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProxyId",
                table: "Users",
                column: "ProxyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Proxies_ProxyId",
                table: "Users",
                column: "ProxyId",
                principalTable: "Proxies",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Proxies_ProxyId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ProxyId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AccessToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProxyId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TargetSetTime",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VkName",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Target",
                table: "Users",
                newName: "ChatId");

            migrationBuilder.AddColumn<int>(
                name: "VkId",
                table: "Users",
                type: "int",
                nullable: true);
            

            migrationBuilder.CreateTable(
                name: "Vks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProxyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccessToken = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vks_Proxies_ProxyId",
                        column: x => x.ProxyId,
                        principalTable: "Proxies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_VkId",
                table: "Users",
                column: "VkId",
                unique: true,
                filter: "[VkId] IS NOT NULL");
            
            migrationBuilder.CreateIndex(
                name: "IX_Vks_ProxyId",
                table: "Vks",
                column: "ProxyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Vks_VkId",
                table: "Users",
                column: "VkId",
                principalTable: "Vks",
                principalColumn: "Id");
        }
    }
}
