using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ONW_API.Migrations
{
    /// <inheritdoc />
    public partial class RemovingIsActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransporterId",
                table: "TransporterVerifications");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Transporters");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "TransporterVerifications",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "TransporterVerifications",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TransporterVerifications",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "TransporterVerifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "TransporterVerifications",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TransporterVerifications_Email",
                table: "TransporterVerifications",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TransporterVerifications_Email",
                table: "TransporterVerifications");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "TransporterVerifications");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "TransporterVerifications");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "TransporterVerifications");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "TransporterVerifications");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "TransporterVerifications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(6)",
                oldMaxLength: 6);

            migrationBuilder.AddColumn<Guid>(
                name: "TransporterId",
                table: "TransporterVerifications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Transporters",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
