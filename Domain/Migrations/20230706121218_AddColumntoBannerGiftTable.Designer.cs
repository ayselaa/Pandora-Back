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
    [Migration("20230706121218_AddColumntoBannerGiftTable")]
    partial class AddColumntoBannerGiftTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.BannerGifts.BannerGift", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("BannerGifts");
                });

            modelBuilder.Entity("Domain.Entities.BannerGifts.BannerGiftTranslate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("BannerGiftId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LangCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Tittle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BannerGiftId");

                    b.ToTable("BannerGiftTranslates");
                });

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

            modelBuilder.Entity("Domain.Entities.Blogs.Blog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("Domain.Entities.Blogs.BlogTranslate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("BlogId")
                        .HasColumnType("integer");

                    b.Property<string>("LangCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Links")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Tittle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BlogId");

                    b.ToTable("BlogTranslates");
                });

            modelBuilder.Entity("Domain.Entities.FooterMenus.FooterItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("FooterMenuId")
                        .HasColumnType("integer");

                    b.Property<bool>("SoftDelete")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("FooterMenuId");

                    b.ToTable("FooterItems");
                });

            modelBuilder.Entity("Domain.Entities.FooterMenus.FooterItemTranslate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("FooterItemId")
                        .HasColumnType("integer");

                    b.Property<string>("LangCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FooterItemId");

                    b.ToTable("FooterItemTranslates");
                });

            modelBuilder.Entity("Domain.Entities.FooterMenus.FooterMenu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("SoftDelete")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("FooterMenus");
                });

            modelBuilder.Entity("Domain.Entities.FooterMenus.FooterMenuTranslate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("FooterMenuId")
                        .HasColumnType("integer");

                    b.Property<string>("LangCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FooterMenuId");

                    b.ToTable("FooterMenuTranslates");
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

            modelBuilder.Entity("Domain.Entities.SliderDiscovery.SliderDiscovery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SliderDiscoveries");
                });

            modelBuilder.Entity("Domain.Entities.SliderDiscovery.SliderDiscoveryTranslate", b =>
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

                    b.Property<int?>("SliderDiscoveryId")
                        .HasColumnType("integer");

                    b.Property<string>("Tittle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SliderDiscoveryId");

                    b.ToTable("SliderDiscoveriesTranslates");
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

                    b.Property<string>("Video")
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

            modelBuilder.Entity("Domain.Entities.BannerGifts.BannerGiftTranslate", b =>
                {
                    b.HasOne("Domain.Entities.BannerGifts.BannerGift", null)
                        .WithMany("BannerGiftTranslates")
                        .HasForeignKey("BannerGiftId");
                });

            modelBuilder.Entity("Domain.Entities.Banners.BannerTranslate", b =>
                {
                    b.HasOne("Domain.Entities.Banners.Banner", null)
                        .WithMany("BannerTranslates")
                        .HasForeignKey("BannerId");
                });

            modelBuilder.Entity("Domain.Entities.Blogs.BlogTranslate", b =>
                {
                    b.HasOne("Domain.Entities.Blogs.Blog", null)
                        .WithMany("BlogTranslates")
                        .HasForeignKey("BlogId");
                });

            modelBuilder.Entity("Domain.Entities.FooterMenus.FooterItem", b =>
                {
                    b.HasOne("Domain.Entities.FooterMenus.FooterMenu", "FooterMenu")
                        .WithMany("Items")
                        .HasForeignKey("FooterMenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FooterMenu");
                });

            modelBuilder.Entity("Domain.Entities.FooterMenus.FooterItemTranslate", b =>
                {
                    b.HasOne("Domain.Entities.FooterMenus.FooterItem", null)
                        .WithMany("FooterItemTranslates")
                        .HasForeignKey("FooterItemId");
                });

            modelBuilder.Entity("Domain.Entities.FooterMenus.FooterMenuTranslate", b =>
                {
                    b.HasOne("Domain.Entities.FooterMenus.FooterMenu", null)
                        .WithMany("Translates")
                        .HasForeignKey("FooterMenuId");
                });

            modelBuilder.Entity("Domain.Entities.SliderDiscovery.SliderDiscoveryTranslate", b =>
                {
                    b.HasOne("Domain.Entities.SliderDiscovery.SliderDiscovery", null)
                        .WithMany("SliderDiscoveryTranslates")
                        .HasForeignKey("SliderDiscoveryId");
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

            modelBuilder.Entity("Domain.Entities.BannerGifts.BannerGift", b =>
                {
                    b.Navigation("BannerGiftTranslates");
                });

            modelBuilder.Entity("Domain.Entities.Banners.Banner", b =>
                {
                    b.Navigation("BannerTranslates");
                });

            modelBuilder.Entity("Domain.Entities.Blogs.Blog", b =>
                {
                    b.Navigation("BlogTranslates");
                });

            modelBuilder.Entity("Domain.Entities.FooterMenus.FooterItem", b =>
                {
                    b.Navigation("FooterItemTranslates");
                });

            modelBuilder.Entity("Domain.Entities.FooterMenus.FooterMenu", b =>
                {
                    b.Navigation("Items");

                    b.Navigation("Translates");
                });

            modelBuilder.Entity("Domain.Entities.SliderDiscovery.SliderDiscovery", b =>
                {
                    b.Navigation("SliderDiscoveryTranslates");
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
