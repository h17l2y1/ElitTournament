﻿// <auto-generated />
using ElitTournament.DAL.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ElitTournament.DAL.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ElitTournament.Core.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("ApiVersion");

                    b.Property<string>("Avatar");

                    b.Property<int>("ClientId");

                    b.Property<string>("Country");

                    b.Property<string>("DeviceType");

                    b.Property<bool>("IsTelegram");

                    b.Property<bool>("IsViber");

                    b.Property<string>("Language");

                    b.Property<int>("Mcc");

                    b.Property<int>("Mnc");

                    b.Property<string>("Name");

                    b.Property<string>("PrimaryDeviceOS");

                    b.Property<string>("ViberVersion");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
