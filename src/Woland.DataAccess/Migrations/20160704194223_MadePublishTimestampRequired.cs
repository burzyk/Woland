using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Woland.DataAccess.Migrations
{
    public partial class MadePublishTimestampRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PostedTimestamp",
                table: "JobLeads",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PostedTimestamp",
                table: "JobLeads",
                nullable: true);
        }
    }
}
