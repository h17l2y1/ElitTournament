﻿// <auto-generated />
using System;
using ElitTournament.DAL.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ElitTournament.DAL.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20200612133646_initt")]
    partial class initt
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ElitTournament.DAL.Enities.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Match");

                    b.Property<int>("ScheduleId");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleId");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("ElitTournament.DAL.Entities.League", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Leagues");
                });

            modelBuilder.Entity("ElitTournament.DAL.Entities.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Place");

                    b.HasKey("Id");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("ElitTournament.DAL.Entities.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<int>("Drawn");

                    b.Property<string>("GoalDifference");

                    b.Property<int>("Goals");

                    b.Property<int>("LeagueId");

                    b.Property<int>("Lost");

                    b.Property<string>("Name");

                    b.Property<int>("Played");

                    b.Property<int>("Points");

                    b.Property<int>("Position");

                    b.Property<int>("Won");

                    b.HasKey("Id");

                    b.HasIndex("LeagueId");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("ElitTournament.DAL.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("ApiVersion");

                    b.Property<string>("Avatar");

                    b.Property<string>("ClientId");

                    b.Property<string>("Country");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("DeviceType");

                    b.Property<bool>("IsTelegram");

                    b.Property<bool>("IsViber");

                    b.Property<string>("Language");

                    b.Property<int>("Mcc");

                    b.Property<int>("Mnc");

                    b.Property<string>("Name");

                    b.Property<string>("PrimaryDeviceOS");

                    b.Property<string>("Username");

                    b.Property<string>("ViberVersion");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ElitTournament.DAL.Enities.Game", b =>
                {
                    b.HasOne("ElitTournament.DAL.Entities.Schedule")
                        .WithMany("Games")
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ElitTournament.DAL.Entities.Team", b =>
                {
                    b.HasOne("ElitTournament.DAL.Entities.League")
                        .WithMany("Teams")
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
