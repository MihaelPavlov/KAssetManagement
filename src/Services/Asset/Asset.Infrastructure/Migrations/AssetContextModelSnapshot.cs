﻿// <auto-generated />
using System;
using Asset.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Asset.Infrastructure.Migrations
{
    [DbContext(typeof(AssetContext))]
    partial class AssetContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Asset.Domain.Entities.Asset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GuarantyMounts")
                        .HasColumnType("int");

                    b.Property<int>("InventoryNumber")
                        .HasColumnType("int");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PeriodType")
                        .HasColumnType("int");

                    b.Property<string>("Price")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Producer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Asset");
                });

            modelBuilder.Entity("Asset.Domain.Entities.RelocationRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AssetId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("FromLocationId")
                        .HasColumnType("int");

                    b.Property<int>("FromSiteId")
                        .HasColumnType("int");

                    b.Property<int?>("FromUserId")
                        .HasColumnType("int");

                    b.Property<int?>("GetRequest")
                        .HasColumnType("int");

                    b.Property<int?>("Received")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("ToLocationId")
                        .HasColumnType("int");

                    b.Property<int>("ToSiteId")
                        .HasColumnType("int");

                    b.Property<int?>("ToUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AssetId");

                    b.ToTable("RelocationRequest");
                });

            modelBuilder.Entity("Asset.Domain.Entities.RelocationRequest", b =>
                {
                    b.HasOne("Asset.Domain.Entities.Asset", "Asset")
                        .WithMany("RelocationRequests")
                        .HasForeignKey("AssetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Asset");
                });

            modelBuilder.Entity("Asset.Domain.Entities.Asset", b =>
                {
                    b.Navigation("RelocationRequests");
                });
#pragma warning restore 612, 618
        }
    }
}
