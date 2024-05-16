﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MushroomPocket;

#nullable disable

namespace MushroomPocket.Migrations
{
    [DbContext(typeof(MushroomDatabase))]
    partial class MushroomDatabaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.5");

            modelBuilder.Entity("MushroomMaster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("NoToTransform")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TransformTo")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MushroomMasters");
                });

            modelBuilder.Entity("MushroomPocket.Character", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("TEXT");

                    b.Property<int>("EXP")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HP")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Skill")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Characters");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Character");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("MushroomPocket.Luigi", b =>
                {
                    b.HasBaseType("MushroomPocket.Character");

                    b.HasDiscriminator().HasValue("Luigi");
                });

            modelBuilder.Entity("MushroomPocket.Mario", b =>
                {
                    b.HasBaseType("MushroomPocket.Character");

                    b.HasDiscriminator().HasValue("Mario");
                });

            modelBuilder.Entity("MushroomPocket.Peach", b =>
                {
                    b.HasBaseType("MushroomPocket.Character");

                    b.HasDiscriminator().HasValue("Peach");
                });

            modelBuilder.Entity("MushroomPocket.Wario", b =>
                {
                    b.HasBaseType("MushroomPocket.Character");

                    b.HasDiscriminator().HasValue("Wario");
                });
#pragma warning restore 612, 618
        }
    }
}