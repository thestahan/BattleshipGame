﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Core.Entities.Board", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("Core.Entities.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Finished")
                        .HasColumnType("bit");

                    b.Property<Guid>("NextTurnPlayerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PlayerOneId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PlayerTwoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PlayerOneId");

                    b.HasIndex("PlayerTwoId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Core.Entities.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("EnemyBoardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("SelfBoardId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EnemyBoardId");

                    b.HasIndex("SelfBoardId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Core.Entities.Ship", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BoardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CellsPositions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.ToTable("Ships");
                });

            modelBuilder.Entity("Core.Entities.Shot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BoardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ShipWasHit")
                        .HasColumnType("bit");

                    b.Property<string>("ShotByPlayerName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.ToTable("Shots");
                });

            modelBuilder.Entity("Core.Entities.Game", b =>
                {
                    b.HasOne("Core.Entities.Player", "PlayerOne")
                        .WithMany()
                        .HasForeignKey("PlayerOneId");

                    b.HasOne("Core.Entities.Player", "PlayerTwo")
                        .WithMany()
                        .HasForeignKey("PlayerTwoId");

                    b.Navigation("PlayerOne");

                    b.Navigation("PlayerTwo");
                });

            modelBuilder.Entity("Core.Entities.Player", b =>
                {
                    b.HasOne("Core.Entities.Board", "EnemyBoard")
                        .WithMany()
                        .HasForeignKey("EnemyBoardId");

                    b.HasOne("Core.Entities.Board", "SelfBoard")
                        .WithMany()
                        .HasForeignKey("SelfBoardId");

                    b.Navigation("EnemyBoard");

                    b.Navigation("SelfBoard");
                });

            modelBuilder.Entity("Core.Entities.Ship", b =>
                {
                    b.HasOne("Core.Entities.Board", null)
                        .WithMany("Ships")
                        .HasForeignKey("BoardId");
                });

            modelBuilder.Entity("Core.Entities.Shot", b =>
                {
                    b.HasOne("Core.Entities.Board", null)
                        .WithMany("Shots")
                        .HasForeignKey("BoardId");
                });

            modelBuilder.Entity("Core.Entities.Board", b =>
                {
                    b.Navigation("Ships");

                    b.Navigation("Shots");
                });
#pragma warning restore 612, 618
        }
    }
}
