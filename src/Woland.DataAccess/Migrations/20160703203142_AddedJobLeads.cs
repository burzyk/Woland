using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Woland.DataAccess.Migrations
{
    public partial class AddedJobLeads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobLeads",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AgencyName = table.Column<string>(maxLength: 128, nullable: true),
                    Body = table.Column<string>(nullable: false),
                    Email = table.Column<string>(maxLength: 128, nullable: true),
                    FullName = table.Column<string>(maxLength: 128, nullable: true),
                    MaxRate = table.Column<decimal>(nullable: true),
                    MinRate = table.Column<decimal>(nullable: true),
                    PostedTimestamp = table.Column<DateTime>(nullable: true),
                    SearchKeywords = table.Column<string>(nullable: false),
                    SearchLocation = table.Column<string>(nullable: false),
                    SourceName = table.Column<string>(nullable: false),
                    SourceUrl = table.Column<string>(nullable: false),
                    Telephone = table.Column<string>(maxLength: 128, nullable: true),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobLeads", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobLeads");
        }
    }
}
