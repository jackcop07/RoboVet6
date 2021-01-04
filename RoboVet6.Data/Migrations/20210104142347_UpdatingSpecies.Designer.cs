﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RoboVet6.Data.DbContext;

namespace RoboVet6.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210104142347_UpdatingSpecies")]
    partial class UpdatingSpecies
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("RoboVet6.Data.Models.RoboVet6.AnimalModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("BreedId")
                        .HasColumnType("int");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<int>("ColourId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SpeciesId")
                        .HasColumnType("int");

                    b.Property<int?>("SpeciesModelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("SpeciesModelId");

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("RoboVet6.Data.Models.RoboVet6.BreedModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SpeciesId")
                        .HasColumnType("int");

                    b.Property<int?>("SpeciesModelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SpeciesModelId");

                    b.ToTable("Breeds");
                });

            modelBuilder.Entity("RoboVet6.Data.Models.RoboVet6.ClientModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HomePhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobilePhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Postcode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkPhone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("RoboVet6.Data.Models.RoboVet6.ColourModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("BreedId")
                        .HasColumnType("int");

                    b.Property<int?>("BreedModelId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BreedModelId");

                    b.ToTable("Colours");
                });

            modelBuilder.Entity("RoboVet6.Data.Models.RoboVet6.SpeciesModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Species");
                });

            modelBuilder.Entity("RoboVet6.Data.Models.RoboVet6.AnimalModel", b =>
                {
                    b.HasOne("RoboVet6.Data.Models.RoboVet6.ClientModel", "Client")
                        .WithMany("Animals")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RoboVet6.Data.Models.RoboVet6.SpeciesModel", null)
                        .WithMany("Animals")
                        .HasForeignKey("SpeciesModelId");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("RoboVet6.Data.Models.RoboVet6.BreedModel", b =>
                {
                    b.HasOne("RoboVet6.Data.Models.RoboVet6.SpeciesModel", null)
                        .WithMany("Breeds")
                        .HasForeignKey("SpeciesModelId");
                });

            modelBuilder.Entity("RoboVet6.Data.Models.RoboVet6.ColourModel", b =>
                {
                    b.HasOne("RoboVet6.Data.Models.RoboVet6.BreedModel", null)
                        .WithMany("Colours")
                        .HasForeignKey("BreedModelId");
                });

            modelBuilder.Entity("RoboVet6.Data.Models.RoboVet6.BreedModel", b =>
                {
                    b.Navigation("Colours");
                });

            modelBuilder.Entity("RoboVet6.Data.Models.RoboVet6.ClientModel", b =>
                {
                    b.Navigation("Animals");
                });

            modelBuilder.Entity("RoboVet6.Data.Models.RoboVet6.SpeciesModel", b =>
                {
                    b.Navigation("Animals");

                    b.Navigation("Breeds");
                });
#pragma warning restore 612, 618
        }
    }
}
