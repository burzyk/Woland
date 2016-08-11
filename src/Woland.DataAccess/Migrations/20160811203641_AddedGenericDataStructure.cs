using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Woland.DataAccess.Migrations
{
    public partial class AddedGenericDataStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                 name: "ImportResults",
                 columns: table => new
                 {
                     Id = table.Column<int>(nullable: false)
                         .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                     DbTimestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                     ImportScheduleId = table.Column<int>(nullable: false),
                     Timestamp = table.Column<DateTime>(nullable: false)
                 },
                 constraints: table =>
                 {
                     table.PrimaryKey("PK_ImportResults", x => x.Id);
                     table.ForeignKey(
                         name: "FK_ImportResults_ImportSchedules_ImportScheduleId",
                         column: x => x.ImportScheduleId,
                         principalTable: "ImportSchedules",
                         principalColumn: "Id",
                         onDelete: ReferentialAction.Cascade);
                 });

            migrationBuilder.CreateTable(
                name: "ImportScheduleProperty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DbTimestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    ImportScheduleId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportScheduleProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImportScheduleProperty_ImportSchedules_ImportScheduleId",
                        column: x => x.ImportScheduleId,
                        principalTable: "ImportSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImportResultProperty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DbTimestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    ImportResultId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportResultProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImportResultProperty_ImportResults_ImportResultId",
                        column: x => x.ImportResultId,
                        principalTable: "ImportResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddColumn<string>(
                name: "ImporterName",
                table: "ImportSchedules",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ImportResults_ImportScheduleId",
                table: "ImportResults",
                column: "ImportScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportResultProperty_ImportResultId",
                table: "ImportResultProperty",
                column: "ImportResultId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportScheduleProperty_ImportScheduleId",
                table: "ImportScheduleProperty",
                column: "ImportScheduleId");


            migrationBuilder.Sql("SET IDENTITY_INSERT ImportResults ON");
            migrationBuilder.Sql("INSERT INTO ImportResults(Id, ImportScheduleId, Timestamp) SELECT Id, (SELECT Id FROM ImportSchedules), PostedTimestamp FROM JobLeads");
            migrationBuilder.Sql("SET IDENTITY_INSERT ImportResults OFF");

            migrationBuilder.Sql("INSERT INTO ImportResultProperty(ImportResultId, Name, Value) SELECT Id, 'SourceUrl', SourceUrl FROM JobLeads");
            migrationBuilder.Sql("INSERT INTO ImportResultProperty(ImportResultId, Name, Value) SELECT Id, 'Title', Title FROM JobLeads");
            migrationBuilder.Sql("INSERT INTO ImportResultProperty(ImportResultId, Name, Value) SELECT Id, 'Body', Body FROM JobLeads");
            migrationBuilder.Sql("INSERT INTO ImportResultProperty(ImportResultId, Name, Value) SELECT Id, 'MinRate', MinRate FROM JobLeads");
            migrationBuilder.Sql("INSERT INTO ImportResultProperty(ImportResultId, Name, Value) SELECT Id, 'MaxRate', MaxRate FROM JobLeads");
            migrationBuilder.Sql("INSERT INTO ImportResultProperty(ImportResultId, Name, Value) SELECT Id, 'PostedTimestamp', PostedTimestamp FROM JobLeads");
            migrationBuilder.Sql("INSERT INTO ImportResultProperty(ImportResultId, Name, Value) SELECT Id, 'FullName', FullName FROM JobLeads");
            migrationBuilder.Sql("INSERT INTO ImportResultProperty(ImportResultId, Name, Value) SELECT Id, 'Telephone', Telephone FROM JobLeads");
            migrationBuilder.Sql("INSERT INTO ImportResultProperty(ImportResultId, Name, Value) SELECT Id, 'Email', Email FROM JobLeads");
            migrationBuilder.Sql("INSERT INTO ImportResultProperty(ImportResultId, Name, Value) SELECT Id, 'AgencyName', AgencyName FROM JobLeads");

            migrationBuilder.Sql("INSERT INTO ImportScheduleProperty(ImportScheduleId, Name, Value) SELECT Id, 'SearchKeywords', SearchKeywords FROM ImportSchedules");
            migrationBuilder.Sql("INSERT INTO ImportScheduleProperty(ImportScheduleId, Name, Value) SELECT Id, 'SearchLocation', SearchLocation FROM ImportSchedules");

            migrationBuilder.DropColumn(
               name: "SearchKeywords",
               table: "ImportSchedules");

            migrationBuilder.DropColumn(
                name: "SearchLocation",
                table: "ImportSchedules");

            migrationBuilder.DropTable(
                name: "JobLeads");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            throw new NotSupportedException("This migration cannot be reverted");
        }
    }
}
