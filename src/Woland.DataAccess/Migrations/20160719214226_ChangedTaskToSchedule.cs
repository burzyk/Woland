using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Woland.DataAccess.Migrations
{
    public partial class ChangedTaskToSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(name: "ImportTasks", newName: "ImportSchedules", schema: "dbo");
            migrationBuilder.DropColumn(name: "LastExecuted", table: "ImportSchedules");
            migrationBuilder.AddColumn<int>(name: "Hour", table: "ImportSchedules", nullable: true);
            migrationBuilder.AddColumn<int>(name: "Minute", table: "ImportSchedules", nullable: true);
            migrationBuilder.AddColumn<DateTime>(name: "NextRunDate", table: "ImportSchedules", nullable: true);

            migrationBuilder.Sql("UPDATE ImportSchedules SET Hour = 19, Minute = 00");

            migrationBuilder.AlterColumn<int>(name: "Hour", table: "ImportSchedules", nullable: false);
            migrationBuilder.AlterColumn<int>(name: "Minute", table: "ImportSchedules", nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(name: "ImportSchedules", newName: "ImportTasks");

            migrationBuilder.DropColumn(name: "Hour", table: "ImportTasks");
            migrationBuilder.DropColumn(name: "Minute", table: "ImportTasks");
            migrationBuilder.AddColumn<DateTime>(name: "LastExecuted", table: "ImportTasks", nullable: true);
        }
    }
}
