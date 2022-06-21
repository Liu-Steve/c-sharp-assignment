﻿// <auto-generated />
using BusHelper.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BusHelper.Migrations
{
    [DbContext(typeof(BusContext))]
    partial class BusContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BusHelper.Bus", b =>
                {
                    b.Property<string>("BusID")
                        .HasColumnType("varchar(255)");

                    b.Property<double>("X")
                        .HasColumnType("double");

                    b.Property<double>("Y")
                        .HasColumnType("double");

                    b.HasKey("BusID");

                    b.ToTable("Buses");
                });
#pragma warning restore 612, 618
        }
    }
}
