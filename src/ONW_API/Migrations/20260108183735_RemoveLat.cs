using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ONW_API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DestinationLat",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "DestinationLng",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "OriginLat",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "OriginLng",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "LocationLatitude",
                table: "PackageTrackingEvents");

            migrationBuilder.DropColumn(
                name: "LocationLongitude",
                table: "PackageTrackingEvents");

            migrationBuilder.RenameColumn(
                name: "Location_State",
                table: "PackageTrackingEvents",
                newName: "LocationState");

            migrationBuilder.RenameColumn(
                name: "Location_City",
                table: "PackageTrackingEvents",
                newName: "LocationCity");

            migrationBuilder.RenameColumn(
                name: "Location_Address",
                table: "PackageTrackingEvents",
                newName: "LocationAddress");

            migrationBuilder.AlterColumn<string>(
                name: "LocationState",
                table: "PackageTrackingEvents",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LocationCity",
                table: "PackageTrackingEvents",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LocationAddress",
                table: "PackageTrackingEvents",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LocationState",
                table: "PackageTrackingEvents",
                newName: "Location_State");

            migrationBuilder.RenameColumn(
                name: "LocationCity",
                table: "PackageTrackingEvents",
                newName: "Location_City");

            migrationBuilder.RenameColumn(
                name: "LocationAddress",
                table: "PackageTrackingEvents",
                newName: "Location_Address");

            migrationBuilder.AddColumn<double>(
                name: "DestinationLat",
                table: "Shipments",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DestinationLng",
                table: "Shipments",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "OriginLat",
                table: "Shipments",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "OriginLng",
                table: "Shipments",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<string>(
                name: "Location_State",
                table: "PackageTrackingEvents",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Location_City",
                table: "PackageTrackingEvents",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Location_Address",
                table: "PackageTrackingEvents",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "LocationLatitude",
                table: "PackageTrackingEvents",
                type: "float",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "LocationLongitude",
                table: "PackageTrackingEvents",
                type: "float",
                maxLength: 50,
                nullable: true);
        }
    }
}
