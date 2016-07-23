﻿using System;
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

            modelBuilder.Entity("Woland.Domain.Entities.ImportSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("DbTimestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("Hour");

                    b.Property<int>("Minute");

                    b.Property<DateTime?>("NextRunDate");

                    b.Property<string>("SearchKeywords")
                        .IsRequired();

                    b.Property<string>("SearchLocation")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("ImportSchedules");
                });

            modelBuilder.Entity("Woland.Domain.Entities.JobLead", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AgencyName")
                        .HasAnnotation("MaxLength", 128);

                    b.Property<string>("Body")
                        .IsRequired();

                    b.Property<byte[]>("DbTimestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 128);

                    b.Property<string>("FullName")
                        .HasAnnotation("MaxLength", 128);

                    b.Property<decimal?>("MaxRate");

                    b.Property<decimal?>("MinRate");

                    b.Property<DateTime>("PostedTimestamp");

                    b.Property<string>("SearchKeywords")
                        .IsRequired();

                    b.Property<string>("SearchLocation")
                        .IsRequired();

                    b.Property<string>("SourceName")
                        .IsRequired();

                    b.Property<string>("SourceUrl")
                        .IsRequired();

                    b.Property<string>("Telephone")
                        .HasAnnotation("MaxLength", 128);

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("JobLeads");
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
        }
    }
}
