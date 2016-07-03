using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Woland.DataAccess.Migrations
{
    public partial class AddedWebRequestLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WebRequestLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Method = table.Column<string>(nullable: false),
                    Request = table.Column<string>(nullable: false),
                    RequestBody = table.Column<string>(nullable: true),
                    Response = table.Column<string>(nullable: false),
                    ResponseBody = table.Column<string>(nullable: true),
                    ResponseCode = table.Column<int>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    Url = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebRequestLogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebRequestLogs");
        }
    }
}
