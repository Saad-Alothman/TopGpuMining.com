using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TopGpuMining.Persistance.Migrations
{
    public partial class upatedContect : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Algorithms",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name_Arabic = table.Column<string>(nullable: true),
                    Name_English = table.Column<string>(nullable: true),
                    Description_Arabic = table.Column<string>(nullable: true),
                    Description_English = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Algorithms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name_Arabic = table.Column<string>(nullable: true),
                    Name_English = table.Column<string>(nullable: true),
                    Description_Arabic = table.Column<string>(nullable: true),
                    Description_English = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ErrorLogs",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    ErrorMessage = table.Column<string>(nullable: true),
                    StackTrace = table.Column<string>(nullable: true),
                    HttpRequest = table.Column<string>(nullable: true),
                    InnerExceptionErrorMessage = table.Column<string>(nullable: true),
                    InnerExceptionStackTrace = table.Column<string>(nullable: true),
                    MethodName = table.Column<string>(nullable: true),
                    Line = table.Column<int>(nullable: true),
                    ParametersCsv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ErrorLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FiatCurrencies",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ExchangeRateUSD = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiatCurrencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GpusInsightsReports",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GpusInsightsReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name_Arabic = table.Column<string>(nullable: true),
                    Name_English = table.Column<string>(nullable: true),
                    Description_Arabic = table.Column<string>(nullable: true),
                    Description_English = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceSources",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    PriceSourceType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceSources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coins",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsDisabled = table.Column<bool>(nullable: false),
                    WhatToMineId = table.Column<int>(nullable: false),
                    Tag = table.Column<string>(nullable: true),
                    AlgorithmId = table.Column<string>(nullable: true),
                    BlockTime = table.Column<string>(nullable: true),
                    BlockReward = table.Column<double>(nullable: false),
                    BlockReward24 = table.Column<double>(nullable: false),
                    LastBlock = table.Column<int>(nullable: false),
                    Difficulty = table.Column<double>(nullable: false),
                    Difficulty24 = table.Column<double>(nullable: false),
                    Nethash = table.Column<double>(nullable: false),
                    ExchangeRate = table.Column<double>(nullable: false),
                    ExchangeRate24 = table.Column<double>(nullable: false),
                    ExchangeRateVol = table.Column<double>(nullable: false),
                    ExchangeRateCurr = table.Column<string>(nullable: true),
                    MarketCap = table.Column<string>(nullable: true),
                    EstimatedRewards = table.Column<string>(nullable: true),
                    EstimatedRewards24 = table.Column<string>(nullable: true),
                    BtcRevenue = table.Column<string>(nullable: true),
                    BtcRevenue24 = table.Column<string>(nullable: true),
                    Profitability = table.Column<int>(nullable: false),
                    Profitability24 = table.Column<int>(nullable: false),
                    Lagging = table.Column<bool>(nullable: false),
                    ExchangeRateUsd = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coins_Algorithms_AlgorithmId",
                        column: x => x.AlgorithmId,
                        principalTable: "Algorithms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Gpus",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    BrandId = table.Column<string>(nullable: true),
                    ModelId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Asin = table.Column<string>(nullable: true),
                    Ean = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gpus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gpus_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Gpus_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hashrates",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AlogrthimId = table.Column<string>(nullable: true),
                    ModelId = table.Column<string>(nullable: true),
                    HashrateValueMhz = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hashrates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hashrates_Algorithms_AlogrthimId",
                        column: x => x.AlogrthimId,
                        principalTable: "Algorithms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hashrates_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GpuInsightReport",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    GpuId = table.Column<string>(nullable: true),
                    GpusInsightsReportId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GpuInsightReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GpuInsightReport_Gpus_GpuId",
                        column: x => x.GpuId,
                        principalTable: "Gpus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GpuInsightReport_GpusInsightsReports_GpusInsightsReportId",
                        column: x => x.GpusInsightsReportId,
                        principalTable: "GpusInsightsReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GpuPriceSources",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Desc = table.Column<string>(nullable: true),
                    PriceSourceId = table.Column<string>(nullable: true),
                    GpuId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GpuPriceSources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GpuPriceSources_Gpus_GpuId",
                        column: x => x.GpuId,
                        principalTable: "Gpus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GpuPriceSources_PriceSources_PriceSourceId",
                        column: x => x.PriceSourceId,
                        principalTable: "PriceSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PriceSourceItem",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    PriceSourceId = table.Column<string>(nullable: true),
                    ItemName = table.Column<string>(nullable: true),
                    ItemURL = table.Column<string>(nullable: true),
                    ASIN = table.Column<string>(nullable: true),
                    Ean = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    PriceCurrency = table.Column<string>(nullable: true),
                    PriceUSD = table.Column<double>(nullable: false),
                    Merchant = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    ModelYear = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    Manufacturer = table.Column<string>(nullable: true),
                    GpuInsightReportId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceSourceItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceSourceItem_GpuInsightReport_GpuInsightReportId",
                        column: x => x.GpuInsightReportId,
                        principalTable: "GpuInsightReport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PriceSourceItem_PriceSources_PriceSourceId",
                        column: x => x.PriceSourceId,
                        principalTable: "PriceSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coins_AlgorithmId",
                table: "Coins",
                column: "AlgorithmId");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorLogs_UserId",
                table: "ErrorLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GpuInsightReport_GpuId",
                table: "GpuInsightReport",
                column: "GpuId");

            migrationBuilder.CreateIndex(
                name: "IX_GpuInsightReport_GpusInsightsReportId",
                table: "GpuInsightReport",
                column: "GpusInsightsReportId");

            migrationBuilder.CreateIndex(
                name: "IX_GpuPriceSources_GpuId",
                table: "GpuPriceSources",
                column: "GpuId");

            migrationBuilder.CreateIndex(
                name: "IX_GpuPriceSources_PriceSourceId",
                table: "GpuPriceSources",
                column: "PriceSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Gpus_BrandId",
                table: "Gpus",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Gpus_ModelId",
                table: "Gpus",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Hashrates_AlogrthimId",
                table: "Hashrates",
                column: "AlogrthimId");

            migrationBuilder.CreateIndex(
                name: "IX_Hashrates_ModelId",
                table: "Hashrates",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceSourceItem_GpuInsightReportId",
                table: "PriceSourceItem",
                column: "GpuInsightReportId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceSourceItem_PriceSourceId",
                table: "PriceSourceItem",
                column: "PriceSourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coins");

            migrationBuilder.DropTable(
                name: "ErrorLogs");

            migrationBuilder.DropTable(
                name: "FiatCurrencies");

            migrationBuilder.DropTable(
                name: "GpuPriceSources");

            migrationBuilder.DropTable(
                name: "Hashrates");

            migrationBuilder.DropTable(
                name: "PriceSourceItem");

            migrationBuilder.DropTable(
                name: "Algorithms");

            migrationBuilder.DropTable(
                name: "GpuInsightReport");

            migrationBuilder.DropTable(
                name: "PriceSources");

            migrationBuilder.DropTable(
                name: "Gpus");

            migrationBuilder.DropTable(
                name: "GpusInsightsReports");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Models");
        }
    }
}
