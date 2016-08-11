using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Woland.DataAccess;

namespace Woland.DataAccess.Migrations
{
    [DbContext(typeof(EfDataContext))]
    [Migration("20160811203641_AddedGenericDataStructure")]
    partial class AddedGenericDataStructure
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Woland.DataAccess.Logging.LogEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("DbTimestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Level");

                    b.Property<string>("Message");

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("Id");

                    b.ToTable("LogEntries");
                });

            modelBuilder.Entity("Woland.Domain.Entities.ImportResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("DbTimestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int?>("ImportScheduleId")
                        .IsRequired();

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("Id");

                    b.HasIndex("ImportScheduleId");

                    b.ToTable("ImportResults");
                });

            modelBuilder.Entity("Woland.Domain.Entities.ImportResultProperty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("DbTimestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int?>("ImportResultId")
                        .IsRequired();

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("ImportResultId");

                    b.ToTable("ImportResultProperty");
                });

            modelBuilder.Entity("Woland.Domain.Entities.ImportSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("DbTimestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("Hour");

                    b.Property<string>("ImporterName")
                        .IsRequired();

                    b.Property<int>("Minute");

                    b.Property<DateTime?>("NextRunDate");

                    b.HasKey("Id");

                    b.ToTable("ImportSchedules");
                });

            modelBuilder.Entity("Woland.Domain.Entities.ImportScheduleProperty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("DbTimestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int?>("ImportScheduleId")
                        .IsRequired();

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("ImportScheduleId");

                    b.ToTable("ImportScheduleProperty");
                });

            modelBuilder.Entity("Woland.Domain.Entities.WebRequestLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("DbTimestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

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

            modelBuilder.Entity("Woland.Domain.Entities.ImportResult", b =>
                {
                    b.HasOne("Woland.Domain.Entities.ImportSchedule", "ImportSchedule")
                        .WithMany()
                        .HasForeignKey("ImportScheduleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Woland.Domain.Entities.ImportResultProperty", b =>
                {
                    b.HasOne("Woland.Domain.Entities.ImportResult", "ImportResult")
                        .WithMany("Properties")
                        .HasForeignKey("ImportResultId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Woland.Domain.Entities.ImportScheduleProperty", b =>
                {
                    b.HasOne("Woland.Domain.Entities.ImportSchedule", "ImportSchedule")
                        .WithMany("Properties")
                        .HasForeignKey("ImportScheduleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
