namespace GpuMiningInsights.Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Algorithms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name_Arabic = c.String(nullable: false),
                        Name_English = c.String(nullable: false),
                        Description_Arabic = c.String(),
                        Description_English = c.String(),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        CreatedByUserId = c.String(maxLength: 128),
                        ModifiedByUserId = c.String(maxLength: 128),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedByUserId)
                .ForeignKey("dbo.Users", t => t.ModifiedByUserId)
                .Index(t => t.CreatedByUserId)
                .Index(t => t.ModifiedByUserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(nullable: false, maxLength: 256),
                        CompanyId = c.Int(),
                        CompanyBranchId = c.Int(),
                        CreatedByUserId = c.String(),
                        CreatedDate = c.DateTime(),
                        ModifiedByUserId = c.String(),
                        ModifiedDate = c.DateTime(),
                        FirstName_Arabic = c.String(nullable: false),
                        FirstName_English = c.String(nullable: false),
                        MiddleName_Arabic = c.String(),
                        MiddleName_English = c.String(),
                        LastName_Arabic = c.String(nullable: false),
                        LastName_English = c.String(nullable: false),
                        Birthdate = c.DateTime(),
                        About = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.Claims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name_Arabic = c.String(nullable: false),
                        Name_English = c.String(nullable: false),
                        Description_Arabic = c.String(),
                        Description_English = c.String(),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        CreatedByUserId = c.String(maxLength: 128),
                        ModifiedByUserId = c.String(maxLength: 128),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedByUserId)
                .ForeignKey("dbo.Users", t => t.ModifiedByUserId)
                .Index(t => t.CreatedByUserId)
                .Index(t => t.ModifiedByUserId);
            
            CreateTable(
                "dbo.Coins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        IsDisabled = c.Boolean(nullable: false),
                        WhatToMineId = c.Int(nullable: false),
                        Tag = c.String(),
                        AlgorithmId = c.Int(),
                        BlockTime = c.String(),
                        BlockReward = c.Double(nullable: false),
                        BlockReward24 = c.Double(nullable: false),
                        LastBlock = c.Int(nullable: false),
                        Difficulty = c.Double(nullable: false),
                        Difficulty24 = c.Double(nullable: false),
                        Nethash = c.Double(nullable: false),
                        ExchangeRate = c.Double(nullable: false),
                        ExchangeRate24 = c.Double(nullable: false),
                        ExchangeRateVol = c.Double(nullable: false),
                        ExchangeRateCurr = c.String(),
                        MarketCap = c.String(),
                        EstimatedRewards = c.String(),
                        EstimatedRewards24 = c.String(),
                        BtcRevenue = c.String(),
                        BtcRevenue24 = c.String(),
                        Profitability = c.Int(nullable: false),
                        Profitability24 = c.Int(nullable: false),
                        Lagging = c.Boolean(nullable: false),
                        ExchangeRateUsd = c.Double(),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        CreatedByUserId = c.String(maxLength: 128),
                        ModifiedByUserId = c.String(maxLength: 128),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Algorithms", t => t.AlgorithmId)
                .ForeignKey("dbo.Users", t => t.CreatedByUserId)
                .ForeignKey("dbo.Users", t => t.ModifiedByUserId)
                .Index(t => t.AlgorithmId)
                .Index(t => t.CreatedByUserId)
                .Index(t => t.ModifiedByUserId);
            
            CreateTable(
                "dbo.ErrorLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                        ErrorMessage = c.String(),
                        StackTrace = c.String(),
                        HttpRequest = c.String(),
                        InnerExceptionErrorMessage = c.String(),
                        InnerExceptionStackTrace = c.String(),
                        MethodName = c.String(),
                        Line = c.Int(),
                        ParametersCsv = c.String(),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        CreatedByUserId = c.String(maxLength: 128),
                        ModifiedByUserId = c.String(maxLength: 128),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedByUserId)
                .ForeignKey("dbo.Users", t => t.ModifiedByUserId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CreatedByUserId)
                .Index(t => t.ModifiedByUserId);
            
            CreateTable(
                "dbo.FiatCurrencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                        Description = c.String(),
                        ExchangeRateUSD = c.Double(nullable: false),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        CreatedByUserId = c.String(maxLength: 128),
                        ModifiedByUserId = c.String(maxLength: 128),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedByUserId)
                .ForeignKey("dbo.Users", t => t.ModifiedByUserId)
                .Index(t => t.CreatedByUserId)
                .Index(t => t.ModifiedByUserId);
            
            CreateTable(
                "dbo.GPUPriceSources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Desc = c.String(),
                        PriceSourceId = c.Int(),
                        GpuId = c.Int(),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        CreatedByUserId = c.String(maxLength: 128),
                        ModifiedByUserId = c.String(maxLength: 128),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedByUserId)
                .ForeignKey("dbo.Gpus", t => t.GpuId)
                .ForeignKey("dbo.Users", t => t.ModifiedByUserId)
                .ForeignKey("dbo.PriceSources", t => t.PriceSourceId)
                .Index(t => t.PriceSourceId)
                .Index(t => t.GpuId)
                .Index(t => t.CreatedByUserId)
                .Index(t => t.ModifiedByUserId);
            
            CreateTable(
                "dbo.Gpus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BrandId = c.Int(),
                        ModelId = c.Int(),
                        Name = c.String(),
                        Description = c.String(),
                        Asin = c.String(),
                        Ean = c.String(),
                        ImageUrl = c.String(),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        CreatedByUserId = c.String(maxLength: 128),
                        ModifiedByUserId = c.String(maxLength: 128),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.BrandId)
                .ForeignKey("dbo.Users", t => t.CreatedByUserId)
                .ForeignKey("dbo.Models", t => t.ModelId)
                .ForeignKey("dbo.Users", t => t.ModifiedByUserId)
                .Index(t => t.BrandId)
                .Index(t => t.ModelId)
                .Index(t => t.CreatedByUserId)
                .Index(t => t.ModifiedByUserId);
            
            CreateTable(
                "dbo.Models",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name_Arabic = c.String(nullable: false),
                        Name_English = c.String(nullable: false),
                        Description_Arabic = c.String(),
                        Description_English = c.String(),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        CreatedByUserId = c.String(maxLength: 128),
                        ModifiedByUserId = c.String(maxLength: 128),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedByUserId)
                .ForeignKey("dbo.Users", t => t.ModifiedByUserId)
                .Index(t => t.CreatedByUserId)
                .Index(t => t.ModifiedByUserId);
            
            CreateTable(
                "dbo.Hashrates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AlogrthimId = c.Int(),
                        ModelId = c.Int(),
                        HashrateNumber = c.String(),
                        Description = c.String(),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        CreatedByUserId = c.String(maxLength: 128),
                        ModifiedByUserId = c.String(maxLength: 128),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Algorithms", t => t.AlogrthimId)
                .ForeignKey("dbo.Users", t => t.CreatedByUserId)
                .ForeignKey("dbo.Models", t => t.ModelId)
                .ForeignKey("dbo.Users", t => t.ModifiedByUserId)
                .Index(t => t.AlogrthimId)
                .Index(t => t.ModelId)
                .Index(t => t.CreatedByUserId)
                .Index(t => t.ModifiedByUserId);
            
            CreateTable(
                "dbo.PriceSources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Url = c.String(),
                        PriceSourceType = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        CreatedByUserId = c.String(maxLength: 128),
                        ModifiedByUserId = c.String(maxLength: 128),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedByUserId)
                .ForeignKey("dbo.Users", t => t.ModifiedByUserId)
                .Index(t => t.CreatedByUserId)
                .Index(t => t.ModifiedByUserId);
            
            CreateTable(
                "dbo.GpusInsightsReports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        CreatedByUserId = c.String(maxLength: 128),
                        ModifiedByUserId = c.String(maxLength: 128),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedByUserId)
                .ForeignKey("dbo.Users", t => t.ModifiedByUserId)
                .Index(t => t.CreatedByUserId)
                .Index(t => t.ModifiedByUserId);
            
            CreateTable(
                "dbo.GpuInsightReports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GpuId = c.Int(),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        CreatedByUserId = c.String(maxLength: 128),
                        ModifiedByUserId = c.String(maxLength: 128),
                        IsActive = c.Boolean(nullable: false),
                        GpusInsightsReport_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedByUserId)
                .ForeignKey("dbo.Gpus", t => t.GpuId)
                .ForeignKey("dbo.Users", t => t.ModifiedByUserId)
                .ForeignKey("dbo.GpusInsightsReports", t => t.GpusInsightsReport_Id)
                .Index(t => t.GpuId)
                .Index(t => t.CreatedByUserId)
                .Index(t => t.ModifiedByUserId)
                .Index(t => t.GpusInsightsReport_Id);
            
            CreateTable(
                "dbo.PriceSourceItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PriceSourceId = c.Int(),
                        ItemName = c.String(),
                        ItemURL = c.String(),
                        ASIN = c.String(),
                        Ean = c.String(),
                        Price = c.Double(nullable: false),
                        PriceCurrency = c.String(),
                        PriceUSD = c.Double(nullable: false),
                        Merchant = c.String(),
                        ImageUrl = c.String(),
                        Model = c.String(),
                        ModelYear = c.String(),
                        Brand = c.String(),
                        Manufacturer = c.String(),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        CreatedByUserId = c.String(maxLength: 128),
                        ModifiedByUserId = c.String(maxLength: 128),
                        IsActive = c.Boolean(nullable: false),
                        GpuInsightReport_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedByUserId)
                .ForeignKey("dbo.Users", t => t.ModifiedByUserId)
                .ForeignKey("dbo.PriceSources", t => t.PriceSourceId)
                .ForeignKey("dbo.GpuInsightReports", t => t.GpuInsightReport_Id)
                .Index(t => t.PriceSourceId)
                .Index(t => t.CreatedByUserId)
                .Index(t => t.ModifiedByUserId)
                .Index(t => t.GpuInsightReport_Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Description_Arabic = c.String(),
                        Description_English = c.String(),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.GpusInsightsReports", "ModifiedByUserId", "dbo.Users");
            DropForeignKey("dbo.GpuInsightReports", "GpusInsightsReport_Id", "dbo.GpusInsightsReports");
            DropForeignKey("dbo.PriceSourceItems", "GpuInsightReport_Id", "dbo.GpuInsightReports");
            DropForeignKey("dbo.PriceSourceItems", "PriceSourceId", "dbo.PriceSources");
            DropForeignKey("dbo.PriceSourceItems", "ModifiedByUserId", "dbo.Users");
            DropForeignKey("dbo.PriceSourceItems", "CreatedByUserId", "dbo.Users");
            DropForeignKey("dbo.GpuInsightReports", "ModifiedByUserId", "dbo.Users");
            DropForeignKey("dbo.GpuInsightReports", "GpuId", "dbo.Gpus");
            DropForeignKey("dbo.GpuInsightReports", "CreatedByUserId", "dbo.Users");
            DropForeignKey("dbo.GpusInsightsReports", "CreatedByUserId", "dbo.Users");
            DropForeignKey("dbo.GPUPriceSources", "PriceSourceId", "dbo.PriceSources");
            DropForeignKey("dbo.PriceSources", "ModifiedByUserId", "dbo.Users");
            DropForeignKey("dbo.PriceSources", "CreatedByUserId", "dbo.Users");
            DropForeignKey("dbo.GPUPriceSources", "ModifiedByUserId", "dbo.Users");
            DropForeignKey("dbo.Gpus", "ModifiedByUserId", "dbo.Users");
            DropForeignKey("dbo.Gpus", "ModelId", "dbo.Models");
            DropForeignKey("dbo.Models", "ModifiedByUserId", "dbo.Users");
            DropForeignKey("dbo.Hashrates", "ModifiedByUserId", "dbo.Users");
            DropForeignKey("dbo.Hashrates", "ModelId", "dbo.Models");
            DropForeignKey("dbo.Hashrates", "CreatedByUserId", "dbo.Users");
            DropForeignKey("dbo.Hashrates", "AlogrthimId", "dbo.Algorithms");
            DropForeignKey("dbo.Models", "CreatedByUserId", "dbo.Users");
            DropForeignKey("dbo.GPUPriceSources", "GpuId", "dbo.Gpus");
            DropForeignKey("dbo.Gpus", "CreatedByUserId", "dbo.Users");
            DropForeignKey("dbo.Gpus", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.GPUPriceSources", "CreatedByUserId", "dbo.Users");
            DropForeignKey("dbo.FiatCurrencies", "ModifiedByUserId", "dbo.Users");
            DropForeignKey("dbo.FiatCurrencies", "CreatedByUserId", "dbo.Users");
            DropForeignKey("dbo.ErrorLogs", "UserId", "dbo.Users");
            DropForeignKey("dbo.ErrorLogs", "ModifiedByUserId", "dbo.Users");
            DropForeignKey("dbo.ErrorLogs", "CreatedByUserId", "dbo.Users");
            DropForeignKey("dbo.Coins", "ModifiedByUserId", "dbo.Users");
            DropForeignKey("dbo.Coins", "CreatedByUserId", "dbo.Users");
            DropForeignKey("dbo.Coins", "AlgorithmId", "dbo.Algorithms");
            DropForeignKey("dbo.Brands", "ModifiedByUserId", "dbo.Users");
            DropForeignKey("dbo.Brands", "CreatedByUserId", "dbo.Users");
            DropForeignKey("dbo.Algorithms", "ModifiedByUserId", "dbo.Users");
            DropForeignKey("dbo.Algorithms", "CreatedByUserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Logins", "UserId", "dbo.Users");
            DropForeignKey("dbo.Claims", "UserId", "dbo.Users");
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.PriceSourceItems", new[] { "GpuInsightReport_Id" });
            DropIndex("dbo.PriceSourceItems", new[] { "ModifiedByUserId" });
            DropIndex("dbo.PriceSourceItems", new[] { "CreatedByUserId" });
            DropIndex("dbo.PriceSourceItems", new[] { "PriceSourceId" });
            DropIndex("dbo.GpuInsightReports", new[] { "GpusInsightsReport_Id" });
            DropIndex("dbo.GpuInsightReports", new[] { "ModifiedByUserId" });
            DropIndex("dbo.GpuInsightReports", new[] { "CreatedByUserId" });
            DropIndex("dbo.GpuInsightReports", new[] { "GpuId" });
            DropIndex("dbo.GpusInsightsReports", new[] { "ModifiedByUserId" });
            DropIndex("dbo.GpusInsightsReports", new[] { "CreatedByUserId" });
            DropIndex("dbo.PriceSources", new[] { "ModifiedByUserId" });
            DropIndex("dbo.PriceSources", new[] { "CreatedByUserId" });
            DropIndex("dbo.Hashrates", new[] { "ModifiedByUserId" });
            DropIndex("dbo.Hashrates", new[] { "CreatedByUserId" });
            DropIndex("dbo.Hashrates", new[] { "ModelId" });
            DropIndex("dbo.Hashrates", new[] { "AlogrthimId" });
            DropIndex("dbo.Models", new[] { "ModifiedByUserId" });
            DropIndex("dbo.Models", new[] { "CreatedByUserId" });
            DropIndex("dbo.Gpus", new[] { "ModifiedByUserId" });
            DropIndex("dbo.Gpus", new[] { "CreatedByUserId" });
            DropIndex("dbo.Gpus", new[] { "ModelId" });
            DropIndex("dbo.Gpus", new[] { "BrandId" });
            DropIndex("dbo.GPUPriceSources", new[] { "ModifiedByUserId" });
            DropIndex("dbo.GPUPriceSources", new[] { "CreatedByUserId" });
            DropIndex("dbo.GPUPriceSources", new[] { "GpuId" });
            DropIndex("dbo.GPUPriceSources", new[] { "PriceSourceId" });
            DropIndex("dbo.FiatCurrencies", new[] { "ModifiedByUserId" });
            DropIndex("dbo.FiatCurrencies", new[] { "CreatedByUserId" });
            DropIndex("dbo.ErrorLogs", new[] { "ModifiedByUserId" });
            DropIndex("dbo.ErrorLogs", new[] { "CreatedByUserId" });
            DropIndex("dbo.ErrorLogs", new[] { "UserId" });
            DropIndex("dbo.Coins", new[] { "ModifiedByUserId" });
            DropIndex("dbo.Coins", new[] { "CreatedByUserId" });
            DropIndex("dbo.Coins", new[] { "AlgorithmId" });
            DropIndex("dbo.Brands", new[] { "ModifiedByUserId" });
            DropIndex("dbo.Brands", new[] { "CreatedByUserId" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.Logins", new[] { "UserId" });
            DropIndex("dbo.Claims", new[] { "UserId" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.Algorithms", new[] { "ModifiedByUserId" });
            DropIndex("dbo.Algorithms", new[] { "CreatedByUserId" });
            DropTable("dbo.Roles");
            DropTable("dbo.PriceSourceItems");
            DropTable("dbo.GpuInsightReports");
            DropTable("dbo.GpusInsightsReports");
            DropTable("dbo.PriceSources");
            DropTable("dbo.Hashrates");
            DropTable("dbo.Models");
            DropTable("dbo.Gpus");
            DropTable("dbo.GPUPriceSources");
            DropTable("dbo.FiatCurrencies");
            DropTable("dbo.ErrorLogs");
            DropTable("dbo.Coins");
            DropTable("dbo.Brands");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Logins");
            DropTable("dbo.Claims");
            DropTable("dbo.Users");
            DropTable("dbo.Algorithms");
        }
    }
}
