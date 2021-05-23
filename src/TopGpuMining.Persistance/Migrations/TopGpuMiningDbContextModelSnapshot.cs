﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TopGpuMining.Persistance;

namespace TopGpuMining.Persistance.Migrations
{
    [DbContext(typeof(TopGpuMiningDbContext))]
    partial class TopGpuMiningDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("TopGpuMining.Domain.Models.Address", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountryId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedByUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedByUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("TopGpuMining.Domain.Models.Algorithm", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.ToTable("Algorithms");
                });

            modelBuilder.Entity("TopGpuMining.Domain.Models.Brand", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("TopGpuMining.Domain.Models.Coin", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AlgorithmId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("BlockReward")
                        .HasColumnType("float");

                    b.Property<double>("BlockReward24")
                        .HasColumnType("float");

                    b.Property<string>("BlockTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BtcRevenue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BtcRevenue24")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Difficulty")
                        .HasColumnType("float");

                    b.Property<double>("Difficulty24")
                        .HasColumnType("float");

                    b.Property<string>("EstimatedRewards")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EstimatedRewards24")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ExchangeRate")
                        .HasColumnType("float");

                    b.Property<double>("ExchangeRate24")
                        .HasColumnType("float");

                    b.Property<string>("ExchangeRateCurr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("ExchangeRateUsd")
                        .HasColumnType("float");

                    b.Property<double>("ExchangeRateVol")
                        .HasColumnType("float");

                    b.Property<bool>("IsDisabled")
                        .HasColumnType("bit");

                    b.Property<bool>("Lagging")
                        .HasColumnType("bit");

                    b.Property<int>("LastBlock")
                        .HasColumnType("int");

                    b.Property<string>("MarketCap")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Nethash")
                        .HasColumnType("float");

                    b.Property<int>("Profitability")
                        .HasColumnType("int");

                    b.Property<int>("Profitability24")
                        .HasColumnType("int");

                    b.Property<string>("Tag")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WhatToMineId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AlgorithmId");

                    b.ToTable("Coins");
                });

            modelBuilder.Entity("TopGpuMining.Domain.Models.Country", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double?>("Area")
                        .HasColumnType("float");

                    b.Property<string>("CapitalArabic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CapitalEnglish")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodeTwo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<float?>("Latitude")
                        .HasColumnType("real");

                    b.Property<float?>("Longtitude")
                        .HasColumnType("real");

                    b.Property<string>("NameArabic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEnglish")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Population")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("TopGpuMining.Domain.Models.ErrorLog", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("ErrorMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HttpRequest")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InnerExceptionErrorMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InnerExceptionStackTrace")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Line")
                        .HasColumnType("int");

                    b.Property<string>("MethodName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParametersCsv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StackTrace")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ErrorLogs");
                });

            modelBuilder.Entity("TopGpuMining.Domain.Models.FiatCurrency", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ExchangeRateUSD")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FiatCurrencies");
                });

            modelBuilder.Entity("TopGpuMining.Domain.Models.Gpu", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Asin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrandId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ean")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModelId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("ModelId");

                    b.ToTable("Gpus");
                });

            modelBuilder.Entity("TopGpuMining.Domain.Models.GpuInsightReport", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("GpuId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("GpusInsightsReportId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("GpuId");

                    b.HasIndex("GpusInsightsReportId");

                    b.ToTable("GpuInsightReport");
                });

            modelBuilder.Entity("TopGpuMining.Domain.Models.GpuPriceSource", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GpuId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PriceSourceId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("GpuId");

                    b.HasIndex("PriceSourceId");

                    b.ToTable("GpuPriceSources");
                });

            modelBuilder.Entity("TopGpuMining.Domain.Models.GpusInsightsReport", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("GpusInsightsReports");
                });

            modelBuilder.Entity("TopGpuMining.Domain.Models.Hashrate", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AlogrthimId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HashrateValueMhz")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModelId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("AlogrthimId");

                    b.HasIndex("ModelId");

                    b.ToTable("Hashrates");
                });

            modelBuilder.Entity("TopGpuMining.Domain.Models.Model", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.ToTable("Models");
                });

            modelBuilder.Entity("TopGpuMining.Domain.Models.PriceSource", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PriceSourceType")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PriceSources");
                });

            modelBuilder.Entity("TopGpuMining.Domain.Models.PriceSourceItem", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ASIN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ean")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GpuInsightReportId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Manufacturer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Merchant")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModelYear")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("PriceCurrency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PriceSourceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("PriceUSD")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("GpuInsightReportId");

                    b.HasIndex("PriceSourceId");

                    b.ToTable("PriceSourceItem");
                });

            modelBuilder.Entity("TopGpuMining.Domain.Models.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("TopGpuMining.Domain.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("TopGpuMining.Domain.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("TopGpuMining.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("TopGpuMining.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("TopGpuMining.Domain.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TopGpuMining.Domain.Models.User", null)
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("TopGpuMining.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TopGpuMining.Domain.Models.Address", b =>
                {
                    b.HasOne("TopGpuMining.Domain.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TopGpuMining.Domain.Models.Algorithm", b =>
                {
                    b.OwnsOne("TopGpuMining.Core.Entities.LocaleString", "Description", b1 =>
                        {
                            b1.Property<string>("AlgorithmId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Arabic")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("English")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("AlgorithmId");

                            b1.ToTable("Algorithms");

                            b1.WithOwner()
                                .HasForeignKey("AlgorithmId");
                        });

                    b.OwnsOne("TopGpuMining.Core.Entities.LocaleString", "Name", b1 =>
                        {
                            b1.Property<string>("AlgorithmId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Arabic")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("English")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("AlgorithmId");

                            b1.ToTable("Algorithms");

                            b1.WithOwner()
                                .HasForeignKey("AlgorithmId");
                        });
                });

            modelBuilder.Entity("TopGpuMining.Domain.Models.Brand", b =>
                {
                    b.OwnsOne("TopGpuMining.Core.Entities.LocaleString", "Description", b1 =>
                        {
                            b1.Property<string>("BrandId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Arabic")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("English")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("BrandId");

                            b1.ToTable("Brands");

                            b1.WithOwner()
                                .HasForeignKey("BrandId");
                        });

                    b.OwnsOne("TopGpuMining.Core.Entities.LocaleString", "Name", b1 =>
                        {
                            b1.Property<string>("BrandId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Arabic")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("English")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("BrandId");

                            b1.ToTable("Brands");

                            b1.WithOwner()
                                .HasForeignKey("BrandId");
                        });
                });

            modelBuilder.Entity("TopGpuMining.Domain.Models.Coin", b =>
                {
                    b.HasOne("TopGpuMining.Domain.Models.Algorithm", "Algorithm")
                        .WithMany()
                        .HasForeignKey("AlgorithmId");
                });

            modelBuilder.Entity("TopGpuMining.Domain.Models.ErrorLog", b =>
                {
                    b.HasOne("TopGpuMining.Domain.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("TopGpuMining.Domain.Models.Gpu", b =>
                {
                    b.HasOne("TopGpuMining.Domain.Models.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId");

                    b.HasOne("TopGpuMining.Domain.Models.Model", "Model")
                        .WithMany()
                        .HasForeignKey("ModelId");
                });

            modelBuilder.Entity("TopGpuMining.Domain.Models.GpuInsightReport", b =>
                {
                    b.HasOne("TopGpuMining.Domain.Models.Gpu", "Gpu")
                        .WithMany()
                        .HasForeignKey("GpuId");

                    b.HasOne("TopGpuMining.Domain.Models.GpusInsightsReport", null)
                        .WithMany("GpuInsightReports")
                        .HasForeignKey("GpusInsightsReportId");
                });

            modelBuilder.Entity("TopGpuMining.Domain.Models.GpuPriceSource", b =>
                {
                    b.HasOne("TopGpuMining.Domain.Models.Gpu", "Gpu")
                        .WithMany("GPUPriceSources")
                        .HasForeignKey("GpuId");

                    b.HasOne("TopGpuMining.Domain.Models.PriceSource", "PriceSource")
                        .WithMany()
                        .HasForeignKey("PriceSourceId");
                });

            modelBuilder.Entity("TopGpuMining.Domain.Models.Hashrate", b =>
                {
                    b.HasOne("TopGpuMining.Domain.Models.Algorithm", "Algorithm")
                        .WithMany()
                        .HasForeignKey("AlogrthimId");

                    b.HasOne("TopGpuMining.Domain.Models.Model", "Model")
                        .WithMany("HashRates")
                        .HasForeignKey("ModelId");
                });

            modelBuilder.Entity("TopGpuMining.Domain.Models.Model", b =>
                {
                    b.OwnsOne("TopGpuMining.Core.Entities.LocaleString", "Description", b1 =>
                        {
                            b1.Property<string>("ModelId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Arabic")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("English")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ModelId");

                            b1.ToTable("Models");

                            b1.WithOwner()
                                .HasForeignKey("ModelId");
                        });

                    b.OwnsOne("TopGpuMining.Core.Entities.LocaleString", "Name", b1 =>
                        {
                            b1.Property<string>("ModelId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Arabic")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("English")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ModelId");

                            b1.ToTable("Models");

                            b1.WithOwner()
                                .HasForeignKey("ModelId");
                        });
                });

            modelBuilder.Entity("TopGpuMining.Domain.Models.PriceSourceItem", b =>
                {
                    b.HasOne("TopGpuMining.Domain.Models.GpuInsightReport", null)
                        .WithMany("PriceSourceItems")
                        .HasForeignKey("GpuInsightReportId");

                    b.HasOne("TopGpuMining.Domain.Models.PriceSource", "PriceSource")
                        .WithMany()
                        .HasForeignKey("PriceSourceId");
                });

            modelBuilder.Entity("TopGpuMining.Domain.Models.Role", b =>
                {
                    b.OwnsOne("TopGpuMining.Core.Entities.LocaleString", "Description", b1 =>
                        {
                            b1.Property<string>("RoleId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Arabic")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("English")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("RoleId");

                            b1.ToTable("Roles");

                            b1.WithOwner()
                                .HasForeignKey("RoleId");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
