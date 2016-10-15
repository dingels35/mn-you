using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mnyou.Migrations
{
    public partial class AddFieldsToVendor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Zip",
                table: "Vendors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "Zip",
                table: "Vendors");
        }
    }
}
