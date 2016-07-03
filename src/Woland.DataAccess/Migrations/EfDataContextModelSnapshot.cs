using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Woland.DataAccess;

namespace Woland.DataAccess.Migrations
{
    [DbContext(typeof(EfDataContext))]
    partial class EfDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Woland.Domain.Entities.WebRequestLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Method")
                        .IsRequired();

                    b.Property<string>("Request")
                        .IsRequired();

                    b.Property<string>("RequestBody");

                    b.Property<string>("Response")
                        .IsRequired();

                    b.Property<string>("ResponseBody");

                    b.Property<int>("ResponseCode");

                    b.Property<DateTime>("Timestamp");

                    b.Property<string>("Url")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("WebRequestLogs");
                });
        }
    }
}
