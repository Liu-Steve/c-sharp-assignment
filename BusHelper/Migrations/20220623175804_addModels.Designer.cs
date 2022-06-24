﻿// <auto-generated />
using System;
using BusHelper.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BusHelper.Migrations
{
    [DbContext(typeof(BusContext))]
    [Migration("20220623175804_addModels")]
    partial class addModels
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BusHelper.Models.Bus", b =>
                {
                    b.Property<string>("BusId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoadId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("BusId");

                    b.HasIndex("RoadId");

                    b.ToTable("Buses");
                });

            modelBuilder.Entity("BusHelper.Models.Call", b =>
                {
                    b.Property<string>("CallId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Content")
                        .HasColumnType("longtext");

                    b.Property<int>("Length")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("WorkInfoId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("CallId");

                    b.HasIndex("WorkInfoId");

                    b.ToTable("Calls");
                });

            modelBuilder.Entity("BusHelper.Models.DangerAction", b =>
                {
                    b.Property<string>("RecordId")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("CloseEye")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("Conflict")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LeavingSteering")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LookAround")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("NoSafetyBelt")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("RealTimeRecordId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("Smoke")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("UsingPhone")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("Yawn")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("RecordId");

                    b.HasIndex("RealTimeRecordId")
                        .IsUnique();

                    b.ToTable("DangerActions");
                });

            modelBuilder.Entity("BusHelper.Models.DangerIndex", b =>
                {
                    b.Property<string>("recordId")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("BloodOxygen")
                        .HasColumnType("int");

                    b.Property<int>("HeartRate")
                        .HasColumnType("int");

                    b.Property<int>("HighBloodPressure")
                        .HasColumnType("int");

                    b.Property<int>("LowBloodPressure")
                        .HasColumnType("int");

                    b.Property<string>("RealTimeRecordId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Temperature")
                        .HasColumnType("int");

                    b.HasKey("recordId");

                    b.HasIndex("RealTimeRecordId")
                        .IsUnique();

                    b.ToTable("DangerIndex");
                });

            modelBuilder.Entity("BusHelper.Models.DangerRecord", b =>
                {
                    b.Property<string>("RecordId")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("CloseEye")
                        .HasColumnType("int");

                    b.Property<int>("Conflict")
                        .HasColumnType("int");

                    b.Property<int>("LeavingSteering")
                        .HasColumnType("int");

                    b.Property<int>("LookAround")
                        .HasColumnType("int");

                    b.Property<int>("SafetyBelt")
                        .HasColumnType("int");

                    b.Property<int>("Smoke")
                        .HasColumnType("int");

                    b.Property<int>("UsingPhone")
                        .HasColumnType("int");

                    b.Property<string>("WorkInfoId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Yawn")
                        .HasColumnType("int");

                    b.HasKey("RecordId");

                    b.HasIndex("WorkInfoId")
                        .IsUnique();

                    b.ToTable("DangerRecords");
                });

            modelBuilder.Entity("BusHelper.Models.Driver", b =>
                {
                    b.Property<string>("DriverId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FacePic")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.HasKey("DriverId");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("BusHelper.Models.LeavingMsg", b =>
                {
                    b.Property<string>("MsgId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Content")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsRead")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("WorkInfoId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("MsgId");

                    b.HasIndex("WorkInfoId");

                    b.ToTable("LeavingMsgs");
                });

            modelBuilder.Entity("BusHelper.Models.Manager", b =>
                {
                    b.Property<string>("ManagerId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Area")
                        .HasColumnType("longtext");

                    b.Property<string>("Pwd")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ManagerId");

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("BusHelper.Models.RealTimeRecord", b =>
                {
                    b.Property<string>("RecordId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("BusId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RealPic")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime(6)");

                    b.Property<double>("X")
                        .HasColumnType("double");

                    b.Property<double>("Y")
                        .HasColumnType("double");

                    b.HasKey("RecordId");

                    b.HasIndex("BusId");

                    b.ToTable("RealTimeRecords");
                });

            modelBuilder.Entity("BusHelper.Models.Road", b =>
                {
                    b.Property<string>("RoadId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoadInfo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("RoadId");

                    b.ToTable("Roads");
                });

            modelBuilder.Entity("BusHelper.Models.WorkInfo", b =>
                {
                    b.Property<string>("WorkId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("BusId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("DriverId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ManagerId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("WorkId");

                    b.HasIndex("BusId");

                    b.HasIndex("DriverId");

                    b.HasIndex("ManagerId");

                    b.ToTable("WorkInfos");
                });

            modelBuilder.Entity("BusHelper.Models.Bus", b =>
                {
                    b.HasOne("BusHelper.Models.Road", "Road")
                        .WithMany("Buses")
                        .HasForeignKey("RoadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Road");
                });

            modelBuilder.Entity("BusHelper.Models.Call", b =>
                {
                    b.HasOne("BusHelper.Models.WorkInfo", "WorkInfo")
                        .WithMany("Calls")
                        .HasForeignKey("WorkInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WorkInfo");
                });

            modelBuilder.Entity("BusHelper.Models.DangerAction", b =>
                {
                    b.HasOne("BusHelper.Models.RealTimeRecord", "RealTimeRecord")
                        .WithOne("DangerAction")
                        .HasForeignKey("BusHelper.Models.DangerAction", "RealTimeRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RealTimeRecord");
                });

            modelBuilder.Entity("BusHelper.Models.DangerIndex", b =>
                {
                    b.HasOne("BusHelper.Models.RealTimeRecord", "RealTimeRecord")
                        .WithOne("DangerIndex")
                        .HasForeignKey("BusHelper.Models.DangerIndex", "RealTimeRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RealTimeRecord");
                });

            modelBuilder.Entity("BusHelper.Models.DangerRecord", b =>
                {
                    b.HasOne("BusHelper.Models.WorkInfo", "WorkInfo")
                        .WithOne("DangerRecord")
                        .HasForeignKey("BusHelper.Models.DangerRecord", "WorkInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WorkInfo");
                });

            modelBuilder.Entity("BusHelper.Models.LeavingMsg", b =>
                {
                    b.HasOne("BusHelper.Models.WorkInfo", "WorkInfo")
                        .WithMany("LeavingMsgs")
                        .HasForeignKey("WorkInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WorkInfo");
                });

            modelBuilder.Entity("BusHelper.Models.RealTimeRecord", b =>
                {
                    b.HasOne("BusHelper.Models.Bus", "Bus")
                        .WithMany("RealTimeRecords")
                        .HasForeignKey("BusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bus");
                });

            modelBuilder.Entity("BusHelper.Models.WorkInfo", b =>
                {
                    b.HasOne("BusHelper.Models.Bus", "Bus")
                        .WithMany("WorkInfos")
                        .HasForeignKey("BusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusHelper.Models.Driver", "Driver")
                        .WithMany("WorkInfos")
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusHelper.Models.Manager", "Manager")
                        .WithMany("WorkInfos")
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bus");

                    b.Navigation("Driver");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("BusHelper.Models.Bus", b =>
                {
                    b.Navigation("RealTimeRecords");

                    b.Navigation("WorkInfos");
                });

            modelBuilder.Entity("BusHelper.Models.Driver", b =>
                {
                    b.Navigation("WorkInfos");
                });

            modelBuilder.Entity("BusHelper.Models.Manager", b =>
                {
                    b.Navigation("WorkInfos");
                });

            modelBuilder.Entity("BusHelper.Models.RealTimeRecord", b =>
                {
                    b.Navigation("DangerAction")
                        .IsRequired();

                    b.Navigation("DangerIndex")
                        .IsRequired();
                });

            modelBuilder.Entity("BusHelper.Models.Road", b =>
                {
                    b.Navigation("Buses");
                });

            modelBuilder.Entity("BusHelper.Models.WorkInfo", b =>
                {
                    b.Navigation("Calls");

                    b.Navigation("DangerRecord")
                        .IsRequired();

                    b.Navigation("LeavingMsgs");
                });
#pragma warning restore 612, 618
        }
    }
}
