// <auto-generated />
using System;
using Hostel_System.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Hostel_System.Database.Migrations
{
    [DbContext(typeof(HostelSystemDbContext))]
    [Migration("20211017103124_AddingRoomInfo")]
    partial class AddingRoomInfo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Hostel_System.Database.Entity.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdditionalInformation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BookingFrom")
                        .HasColumnType("datetime2");

                    b.Property<int>("BookingRoomId")
                        .HasColumnType("int");

                    b.Property<DateTime>("BookingTo")
                        .HasColumnType("datetime2");

                    b.Property<int>("BookingUserId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TotalCost")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("BookingRoomId");

                    b.HasIndex("BookingUserId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("Hostel_System.Database.Entity.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Hostel_System.Database.Entity.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxPeopleInRoom")
                        .HasColumnType("int");

                    b.Property<int>("Phone")
                        .HasColumnType("int");

                    b.Property<double>("PriceForDay")
                        .HasColumnType("float");

                    b.Property<string>("RoomName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Hostel_System.Database.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BirthDay")
                        .HasColumnType("datetime2");

                    b.Property<string>("CarRegistrationNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsMale")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Phone")
                        .HasColumnType("int");

                    b.Property<int>("RoleNameId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleNameId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Hostel_System.Database.Entity.Reservation", b =>
                {
                    b.HasOne("Hostel_System.Database.Entity.Room", "BookingRoom")
                        .WithMany("Reservation")
                        .HasForeignKey("BookingRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hostel_System.Database.Entity.User", "BookingUser")
                        .WithMany("Reservation")
                        .HasForeignKey("BookingUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookingRoom");

                    b.Navigation("BookingUser");
                });

            modelBuilder.Entity("Hostel_System.Database.Entity.User", b =>
                {
                    b.HasOne("Hostel_System.Database.Entity.Role", "RoleName")
                        .WithMany("Users")
                        .HasForeignKey("RoleNameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RoleName");
                });

            modelBuilder.Entity("Hostel_System.Database.Entity.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Hostel_System.Database.Entity.Room", b =>
                {
                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("Hostel_System.Database.Entity.User", b =>
                {
                    b.Navigation("Reservation");
                });
#pragma warning restore 612, 618
        }
    }
}
