﻿// <auto-generated />
using System;
using CellSync.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CellSync.Infrastructure.Migrations
{
    [DbContext(typeof(CellSyncDbContext))]
    partial class CellSyncDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CellSync.Domain.Entities.Cell", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Cells");
                });

            modelBuilder.Entity("CellSync.Domain.Entities.CellAddress", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("CellId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsCurrent")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("CellId")
                        .IsUnique();

                    b.ToTable("CellAddresses");
                });

            modelBuilder.Entity("CellSync.Domain.Entities.Member", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<string>("ProfileType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("CellSync.Domain.Entities.CellAddress", b =>
                {
                    b.HasOne("CellSync.Domain.Entities.Cell", "Cell")
                        .WithOne("CurrentAddress")
                        .HasForeignKey("CellSync.Domain.Entities.CellAddress", "CellId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cell");
                });

            modelBuilder.Entity("CellSync.Domain.Entities.Cell", b =>
                {
                    b.Navigation("CurrentAddress");
                });
#pragma warning restore 612, 618
        }
    }
}
