using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ONW_API.Migrations
{
    /// <inheritdoc />
    public partial class VehicleId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ShipmentTrackingEvents");

            migrationBuilder.DropColumn(
                name: "EstimatedDeliveryDate",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "PickupDate",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "Vehicle",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "VehiclePlate",
                table: "Drivers");

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

            migrationBuilder.AddColumn<Guid>(
                name: "VehicleId",
                table: "Shipments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "VehicleId",
                table: "Drivers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipientName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RecipientEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RecipientPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RecipientStreet = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RecipientNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RecipientDistrict = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RecipientCity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RecipientState = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RecipientZipCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Packages_Shipments_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Plate = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TransporterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PackageTrackingEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PackageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Location_Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location_State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationLatitude = table.Column<double>(type: "float", maxLength: 50, nullable: true),
                    LocationLongitude = table.Column<double>(type: "float", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageTrackingEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackageTrackingEvents_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_VehicleId",
                table: "Drivers",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_ShipmentId",
                table: "Packages",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageTrackingEvents_PackageId",
                table: "PackageTrackingEvents",
                column: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Vehicles_VehicleId",
                table: "Drivers",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Vehicles_VehicleId",
                table: "Drivers");

            migrationBuilder.DropTable(
                name: "PackageTrackingEvents");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_VehicleId",
                table: "Drivers");

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
                name: "VehicleId",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Drivers");

            migrationBuilder.AddColumn<DateTime>(
                name: "EstimatedDeliveryDate",
                table: "Shipments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Shipments",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PickupDate",
                table: "Shipments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Vehicle",
                table: "Drivers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VehiclePlate",
                table: "Drivers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ShipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Shipments_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShipmentTrackingEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ShipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentTrackingEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipmentTrackingEvents_Shipments_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_ShipmentId",
                table: "Product",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentTrackingEvents_ShipmentId",
                table: "ShipmentTrackingEvents",
                column: "ShipmentId");
        }
    }
}
