﻿// <auto-generated />
using System;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Domain.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230622101701_ChangeColumnSliderDiscoveryVideoTable")]
    partial class ChangeColumnSliderDiscoveryVideoTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Banners.Banner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("SoftDelete")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Banners");
                });

            modelBuilder.Entity("Domain.Entities.Banners.BannerTranslate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("BannerId")
                        .HasColumnType("integer");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LangCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Links")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BannerId");

                    b.ToTable("BannerTranslates");
                });

            modelBuilder.Entity("Domain.Entities.Products.Product", b =>
                {
                    b.Property<string>("ItemCode")
                        .HasColumnType("text");

                    b.Property<string>("Artikul")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<bool>("Package")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("Stock")
                        .HasColumnType("integer");

                    b.HasKey("ItemCode");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Domain.Entities.ProductStock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("ProductStocks");
                });

            modelBuilder.Entity("Domain.Entities.Sliders.Slider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Sliders");
                });

            modelBuilder.Entity("Domain.Entities.Sliders.SlidersTranslate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LangCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Links")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("SliderId")
                        .HasColumnType("integer");

                    b.Property<string>("Tittle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SliderId");

                    b.ToTable("SlidersTranslates");
                });

            modelBuilder.Entity("Domain.Entities.SlidersDiscoveryVideo.SliderDiscoveryVideo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SliderDiscoveryVideos");
                });

            modelBuilder.Entity("Domain.Entities.SlidersDiscoveryVideo.SliderDiscoveryVideoTranslate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LangCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Links")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("SliderDiscoveryVideoId")
                        .HasColumnType("integer");

                    b.Property<string>("Tittle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SliderDiscoveryVideoId");

                    b.ToTable("SliderDiscoveryVideoTranslates");
                });

            modelBuilder.Entity("Domain.Entities.Stocks.Stock", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("Domain.Entities.Banners.BannerTranslate", b =>
                {
                    b.HasOne("Domain.Entities.Banners.Banner", null)
                        .WithMany("BannerTranslates")
                        .HasForeignKey("BannerId");
                });

            modelBuilder.Entity("Domain.Entities.Sliders.SlidersTranslate", b =>
                {
                    b.HasOne("Domain.Entities.Sliders.Slider", null)
                        .WithMany("SlidersTranslates")
                        .HasForeignKey("SliderId");
                });

            modelBuilder.Entity("Domain.Entities.SlidersDiscoveryVideo.SliderDiscoveryVideoTranslate", b =>
                {
                    b.HasOne("Domain.Entities.SlidersDiscoveryVideo.SliderDiscoveryVideo", null)
                        .WithMany("SliderDiscoveryVideoTranslates")
                        .HasForeignKey("SliderDiscoveryVideoId");
                });

            modelBuilder.Entity("Domain.Entities.Banners.Banner", b =>
                {
                    b.Navigation("BannerTranslates");
                });

            modelBuilder.Entity("Domain.Entities.Sliders.Slider", b =>
                {
                    b.Navigation("SlidersTranslates");
                });

            modelBuilder.Entity("Domain.Entities.SlidersDiscoveryVideo.SliderDiscoveryVideo", b =>
                {
                    b.Navigation("SliderDiscoveryVideoTranslates");
                });
#pragma warning restore 612, 618
        }
    }
}
