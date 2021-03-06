// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RepositoryLayer.Context;

namespace RepositoryLayer.Migrations
{
    [DbContext(typeof(FundooContext))]
    [Migration("20220722023510_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RepositoryLayer.Entities.CollabEntity", b =>
                {
                    b.Property<long>("CollabID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EmailID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("NoteID")
                        .HasColumnType("bigint");

                    b.Property<long>("UserID")
                        .HasColumnType("bigint");

                    b.HasKey("CollabID");

                    b.HasIndex("NoteID");

                    b.HasIndex("UserID");

                    b.ToTable("CollabTable");
                });

            modelBuilder.Entity("RepositoryLayer.Entities.NotesEntity", b =>
                {
                    b.Property<long>("NoteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserID")
                        .HasColumnType("bigint");

                    b.Property<bool>("archieve")
                        .HasColumnType("bit");

                    b.Property<string>("color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("modifidedate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("pin")
                        .HasColumnType("bit");

                    b.Property<DateTime>("remainder")
                        .HasColumnType("datetime2");

                    b.Property<bool>("trash")
                        .HasColumnType("bit");

                    b.HasKey("NoteID");

                    b.HasIndex("UserID");

                    b.ToTable("NotesTable");
                });

            modelBuilder.Entity("RepositoryLayer.Entities.UserEntity", b =>
                {
                    b.Property<long>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("UserTable");
                });

            modelBuilder.Entity("RepositoryLayer.Entities.CollabEntity", b =>
                {
                    b.HasOne("RepositoryLayer.Entities.NotesEntity", "Note")
                        .WithMany()
                        .HasForeignKey("NoteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RepositoryLayer.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RepositoryLayer.Entities.NotesEntity", b =>
                {
                    b.HasOne("RepositoryLayer.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
