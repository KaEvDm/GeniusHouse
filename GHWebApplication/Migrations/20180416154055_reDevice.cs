using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GHWebApplication.Migrations
{
    public partial class reDevice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Devices",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Power",
                table: "Devices",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Room",
                table: "Devices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "Power",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "Room",
                table: "Devices");
        }
    }
}
