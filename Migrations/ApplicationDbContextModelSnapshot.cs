﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using entityFrameworkProyect.Data;

#nullable disable

namespace entityFrameworkProyect.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("entityFrameworkProyect.Models.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<double>("Qualification")
                        .HasColumnType("float");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("Título");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("entityFrameworkProyect.Models.ArticleLabel", b =>
                {
                    b.Property<int>("LabelId")
                        .HasColumnType("int");

                    b.Property<int>("ArticleId")
                        .HasColumnType("int");

                    b.HasKey("LabelId", "ArticleId");

                    b.HasIndex("ArticleId");

                    b.ToTable("ArticleLabels");
                });

            modelBuilder.Entity("entityFrameworkProyect.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("entityFrameworkProyect.Models.DetailUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Identification")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sport")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DetailUsers");
                });

            modelBuilder.Entity("entityFrameworkProyect.Models.Label", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<string>("Titulo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Labels");
                });

            modelBuilder.Entity("entityFrameworkProyect.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DetailUserId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DetailUserId")
                        .IsUnique()
                        .HasFilter("[DetailUserId] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("entityFrameworkProyect.Models.Article", b =>
                {
                    b.HasOne("entityFrameworkProyect.Models.Category", "Category")
                        .WithMany("Article")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("entityFrameworkProyect.Models.ArticleLabel", b =>
                {
                    b.HasOne("entityFrameworkProyect.Models.Article", "Article")
                        .WithMany("ArticleLabels")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("entityFrameworkProyect.Models.Label", "Label")
                        .WithMany("ArticleLabels")
                        .HasForeignKey("LabelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Article");

                    b.Navigation("Label");
                });

            modelBuilder.Entity("entityFrameworkProyect.Models.User", b =>
                {
                    b.HasOne("entityFrameworkProyect.Models.DetailUser", "DetailUser")
                        .WithOne("User")
                        .HasForeignKey("entityFrameworkProyect.Models.User", "DetailUserId");

                    b.Navigation("DetailUser");
                });

            modelBuilder.Entity("entityFrameworkProyect.Models.Article", b =>
                {
                    b.Navigation("ArticleLabels");
                });

            modelBuilder.Entity("entityFrameworkProyect.Models.Category", b =>
                {
                    b.Navigation("Article");
                });

            modelBuilder.Entity("entityFrameworkProyect.Models.DetailUser", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("entityFrameworkProyect.Models.Label", b =>
                {
                    b.Navigation("ArticleLabels");
                });
#pragma warning restore 612, 618
        }
    }
}
