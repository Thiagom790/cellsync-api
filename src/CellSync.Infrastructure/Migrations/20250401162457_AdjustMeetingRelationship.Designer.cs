﻿// <auto-generated />
using System;
using CellSync.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CellSync.Infrastructure.Migrations
{
    [DbContext(typeof(CellSyncDbContext))]
    [Migration("20250401162457_AdjustMeetingRelationship")]
    partial class AdjustMeetingRelationship
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Cells");
                });

            modelBuilder.Entity("CellSync.Domain.Entities.Meeting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CellId")
                        .HasColumnType("uuid");

                    b.Property<string>("MeetingAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("MeetingDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CellId");

                    b.ToTable("Meetings");
                });

            modelBuilder.Entity("CellSync.Domain.Entities.MeetingMember", b =>
                {
                    b.Property<Guid>("MeetingId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("MemberId")
                        .HasColumnType("uuid");

                    b.HasKey("MeetingId", "MemberId");

                    b.HasIndex("MemberId");

                    b.ToTable("MeetingMembers");
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

            modelBuilder.Entity("CellSync.Domain.Entities.Meeting", b =>
                {
                    b.HasOne("CellSync.Domain.Entities.Cell", "Cell")
                        .WithMany()
                        .HasForeignKey("CellId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cell");
                });

            modelBuilder.Entity("CellSync.Domain.Entities.MeetingMember", b =>
                {
                    b.HasOne("CellSync.Domain.Entities.Meeting", "Meeting")
                        .WithMany("MeetingMembers")
                        .HasForeignKey("MeetingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CellSync.Domain.Entities.Member", "Member")
                        .WithMany()
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Meeting");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("CellSync.Domain.Entities.Meeting", b =>
                {
                    b.Navigation("MeetingMembers");
                });
#pragma warning restore 612, 618
        }
    }
}
