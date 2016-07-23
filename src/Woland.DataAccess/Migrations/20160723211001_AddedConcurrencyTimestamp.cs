using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Woland.DataAccess.Migrations
{
    public partial class AddedConcurrencyTimestamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "DbTimestamp",
                table: "WebRequestLogs",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "DbTimestamp",
                table: "JobLeads",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "DbTimestamp",
                table: "ImportSchedules",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "DbTimestamp",
                table: "LogEntries",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DbTimestamp",
                table: "WebRequestLogs");

            migrationBuilder.DropColumn(
                name: "DbTimestamp",
                table: "JobLeads");

            migrationBuilder.DropColumn(
                name: "DbTimestamp",
                table: "ImportSchedules");

            migrationBuilder.DropColumn(
                name: "DbTimestamp",
                table: "LogEntries");
        }
    }
}
