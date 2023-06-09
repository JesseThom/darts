﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using darts.Models;

#nullable disable

namespace darts.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20230323214233_ThirdMigration")]
    partial class ThirdMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("darts.Models.League", b =>
                {
                    b.Property<int>("LeagueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LeagueName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("LeagueId");

                    b.HasIndex("TeamId");

                    b.ToTable("Leagues");
                });

            modelBuilder.Entity("darts.Models.Player", b =>
                {
                    b.Property<int>("PlayersId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Hat")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("MPR")
                        .HasColumnType("double");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Week")
                        .HasColumnType("int");

                    b.Property<int>("Whrse")
                        .HasColumnType("int");

                    b.Property<int>("_5MR")
                        .HasColumnType("int");

                    b.Property<int>("_6MR")
                        .HasColumnType("int");

                    b.Property<int>("_7MR")
                        .HasColumnType("int");

                    b.Property<int>("_8MR")
                        .HasColumnType("int");

                    b.Property<int>("_9MR")
                        .HasColumnType("int");

                    b.HasKey("PlayersId");

                    b.HasIndex("TeamId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("darts.Models.Team", b =>
                {
                    b.Property<int>("TeamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("TeamName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("TeamPoints")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("TeamId");

                    b.HasIndex("UserId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("darts.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("LeagueId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("UserId");

                    b.HasIndex("LeagueId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("darts.Models.League", b =>
                {
                    b.HasOne("darts.Models.Team", "TeamLeague")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TeamLeague");
                });

            modelBuilder.Entity("darts.Models.Player", b =>
                {
                    b.HasOne("darts.Models.Team", "PlayerTeam")
                        .WithMany("TeamPlayers")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlayerTeam");
                });

            modelBuilder.Entity("darts.Models.Team", b =>
                {
                    b.HasOne("darts.Models.User", "TeamOwner")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TeamOwner");
                });

            modelBuilder.Entity("darts.Models.User", b =>
                {
                    b.HasOne("darts.Models.League", "UserLeague")
                        .WithMany()
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserLeague");
                });

            modelBuilder.Entity("darts.Models.Team", b =>
                {
                    b.Navigation("TeamPlayers");
                });
#pragma warning restore 612, 618
        }
    }
}
