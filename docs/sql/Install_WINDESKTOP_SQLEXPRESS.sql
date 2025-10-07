/*
    Capco Jordan Almonds database installation script for WINDESKTOP\\SQLEXPRESS.
    This script recreates the database, builds all tables with keys, and seeds baseline data.
    WARNING: Existing CapcoJordan database (if any) will be dropped.
*/

USE [master];
GO

IF DB_ID(N'CapcoJordan') IS NOT NULL
BEGIN
    ALTER DATABASE [CapcoJordan] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [CapcoJordan];
END
GO

CREATE DATABASE [CapcoJordan];
GO

ALTER DATABASE [CapcoJordan] SET MULTI_USER;
GO

USE [CapcoJordan];
GO

-- Identity core tables ----------------------------------------------------
CREATE TABLE [dbo].[AspNetRoles]
(
    [Id] NVARCHAR(450) NOT NULL,
    [Name] NVARCHAR(256) NULL,
    [NormalizedName] NVARCHAR(256) NULL,
    [ConcurrencyStamp] NVARCHAR(MAX) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE TABLE [dbo].[AspNetUsers]
(
    [Id] NVARCHAR(450) NOT NULL,
    [FirstName] NVARCHAR(256) NULL,
    [LastName] NVARCHAR(256) NULL,
    [UserName] NVARCHAR(256) NULL,
    [NormalizedUserName] NVARCHAR(256) NULL,
    [Email] NVARCHAR(256) NULL,
    [NormalizedEmail] NVARCHAR(256) NULL,
    [EmailConfirmed] BIT NOT NULL,
    [PasswordHash] NVARCHAR(MAX) NULL,
    [SecurityStamp] NVARCHAR(MAX) NULL,
    [ConcurrencyStamp] NVARCHAR(MAX) NULL,
    [PhoneNumber] NVARCHAR(MAX) NULL,
    [PhoneNumberConfirmed] BIT NOT NULL,
    [TwoFactorEnabled] BIT NOT NULL,
    [LockoutEnd] DATETIMEOFFSET NULL,
    [LockoutEnabled] BIT NOT NULL,
    [AccessFailedCount] INT NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE TABLE [dbo].[AspNetRoleClaims]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [RoleId] NVARCHAR(450) NOT NULL,
    [ClaimType] NVARCHAR(MAX) NULL,
    [ClaimValue] NVARCHAR(MAX) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId])
        REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [dbo].[AspNetUserClaims]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [UserId] NVARCHAR(450) NOT NULL,
    [ClaimType] NVARCHAR(MAX) NULL,
    [ClaimValue] NVARCHAR(MAX) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId])
        REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [dbo].[AspNetUserLogins]
(
    [LoginProvider] NVARCHAR(128) NOT NULL,
    [ProviderKey] NVARCHAR(128) NOT NULL,
    [ProviderDisplayName] NVARCHAR(MAX) NULL,
    [UserId] NVARCHAR(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId])
        REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [dbo].[AspNetUserRoles]
(
    [UserId] NVARCHAR(450) NOT NULL,
    [RoleId] NVARCHAR(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId])
        REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId])
        REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [dbo].[AspNetUserTokens]
(
    [UserId] NVARCHAR(450) NOT NULL,
    [LoginProvider] NVARCHAR(128) NOT NULL,
    [Name] NVARCHAR(128) NOT NULL,
    [Value] NVARCHAR(MAX) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId])
        REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

-- Domain tables -----------------------------------------------------------
CREATE TABLE [dbo].[ContentBlocks]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Key] NVARCHAR(80) NOT NULL,
    [Html] NVARCHAR(MAX) NOT NULL,
    CONSTRAINT [PK_ContentBlocks] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE TABLE [dbo].[Products]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Name] NVARCHAR(160) NOT NULL,
    [Slug] NVARCHAR(160) NOT NULL,
    [Description] NVARCHAR(4000) NULL,
    [Collection] NVARCHAR(80) NULL,
    [IsActive] BIT NOT NULL,
    [CreatedAt] DATETIME2 NOT NULL CONSTRAINT [DF_Products_CreatedAt] DEFAULT (GETUTCDATE()),
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE TABLE [dbo].[Customers]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [UserId] NVARCHAR(450) NOT NULL,
    [Email] NVARCHAR(256) NOT NULL,
    [FirstName] NVARCHAR(80) NULL,
    [LastName] NVARCHAR(80) NULL,
    [Phone] NVARCHAR(MAX) NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Customers_AspNetUsers_UserId] FOREIGN KEY ([UserId])
        REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [dbo].[ProductImages]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [ProductId] INT NOT NULL,
    [Url] NVARCHAR(512) NOT NULL,
    [Alt] NVARCHAR(160) NULL,
    [SortOrder] INT NOT NULL,
    [IsPrimary] BIT NOT NULL,
    CONSTRAINT [PK_ProductImages] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductImages_Products_ProductId] FOREIGN KEY ([ProductId])
        REFERENCES [dbo].[Products] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [dbo].[ProductVariants]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [ProductId] INT NOT NULL,
    [Color] NVARCHAR(40) NOT NULL,
    [SizeLabel] NVARCHAR(60) NOT NULL,
    [Sku] NVARCHAR(60) NOT NULL,
    [Price] DECIMAL(18,2) NOT NULL,
    [CompareAtPrice] DECIMAL(18,2) NULL,
    [WeightGrams] INT NOT NULL,
    [UPC] NVARCHAR(32) NULL,
    [InventoryQty] INT NOT NULL,
    [AllowBackorder] BIT NOT NULL,
    [IsActive] BIT NOT NULL,
    CONSTRAINT [PK_ProductVariants] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductVariants_Products_ProductId] FOREIGN KEY ([ProductId])
        REFERENCES [dbo].[Products] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [dbo].[Addresses]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [CustomerId] INT NULL,
    [Line1] NVARCHAR(160) NOT NULL,
    [Line2] NVARCHAR(160) NULL,
    [City] NVARCHAR(80) NOT NULL,
    [State] NVARCHAR(60) NOT NULL,
    [Zip] NVARCHAR(20) NOT NULL,
    [Country] NVARCHAR(60) NOT NULL,
    [Type] NVARCHAR(40) NOT NULL,
    CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Addresses_Customers_CustomerId] FOREIGN KEY ([CustomerId])
        REFERENCES [dbo].[Customers] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [dbo].[Carts]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [CustomerId] INT NULL,
    [GuestToken] NVARCHAR(64) NULL,
    [CreatedAt] DATETIME2 NOT NULL,
    [UpdatedAt] DATETIME2 NOT NULL,
    CONSTRAINT [PK_Carts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Carts_Customers_CustomerId] FOREIGN KEY ([CustomerId])
        REFERENCES [dbo].[Customers] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [dbo].[Orders]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [OrderNumber] NVARCHAR(32) NOT NULL,
    [CustomerId] INT NULL,
    [Email] NVARCHAR(256) NOT NULL,
    [Status] NVARCHAR(32) NOT NULL,
    [Subtotal] DECIMAL(18,2) NOT NULL,
    [Shipping] DECIMAL(18,2) NOT NULL,
    [Tax] DECIMAL(18,2) NOT NULL,
    [Total] DECIMAL(18,2) NOT NULL,
    [StripePaymentIntentId] NVARCHAR(160) NULL,
    [CreatedAt] DATETIME2 NOT NULL,
    [ShippingAddressId] INT NULL,
    [BillingAddressId] INT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Orders_Customers_CustomerId] FOREIGN KEY ([CustomerId])
        REFERENCES [dbo].[Customers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Orders_Addresses_ShippingAddressId] FOREIGN KEY ([ShippingAddressId])
        REFERENCES [dbo].[Addresses] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Orders_Addresses_BillingAddressId] FOREIGN KEY ([BillingAddressId])
        REFERENCES [dbo].[Addresses] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [dbo].[CartItems]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [CartId] INT NOT NULL,
    [ProductVariantId] INT NOT NULL,
    [Qty] INT NOT NULL,
    [UnitPriceSnapshot] DECIMAL(18,2) NOT NULL,
    CONSTRAINT [PK_CartItems] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CartItems_Carts_CartId] FOREIGN KEY ([CartId])
        REFERENCES [dbo].[Carts] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CartItems_ProductVariants_ProductVariantId] FOREIGN KEY ([ProductVariantId])
        REFERENCES [dbo].[ProductVariants] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [dbo].[OrderItems]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [OrderId] INT NOT NULL,
    [ProductVariantId] INT NOT NULL,
    [NameSnapshot] NVARCHAR(160) NOT NULL,
    [ColorSnapshot] NVARCHAR(40) NULL,
    [SizeSnapshot] NVARCHAR(60) NULL,
    [Qty] INT NOT NULL,
    [UnitPriceSnapshot] DECIMAL(18,2) NOT NULL,
    CONSTRAINT [PK_OrderItems] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OrderItems_Orders_OrderId] FOREIGN KEY ([OrderId])
        REFERENCES [dbo].[Orders] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_OrderItems_ProductVariants_ProductVariantId] FOREIGN KEY ([ProductVariantId])
        REFERENCES [dbo].[ProductVariants] ([Id]) ON DELETE NO ACTION
);
GO

-- Indexes -----------------------------------------------------------------
CREATE UNIQUE INDEX [IX_ContentBlocks_Key] ON [dbo].[ContentBlocks] ([Key]);
CREATE UNIQUE INDEX [IX_Products_Slug] ON [dbo].[Products] ([Slug]);
CREATE UNIQUE INDEX [IX_ProductVariants_Sku] ON [dbo].[ProductVariants] ([Sku]);
GO

CREATE INDEX [IX_ProductImages_ProductId] ON [dbo].[ProductImages] ([ProductId]);
CREATE INDEX [IX_ProductVariants_ProductId] ON [dbo].[ProductVariants] ([ProductId]);
CREATE INDEX [IX_Addresses_CustomerId] ON [dbo].[Addresses] ([CustomerId]);
CREATE INDEX [IX_Carts_CustomerId] ON [dbo].[Carts] ([CustomerId]);
CREATE INDEX [IX_CartItems_CartId] ON [dbo].[CartItems] ([CartId]);
CREATE INDEX [IX_CartItems_ProductVariantId] ON [dbo].[CartItems] ([ProductVariantId]);
CREATE INDEX [IX_Orders_CustomerId] ON [dbo].[Orders] ([CustomerId]);
CREATE UNIQUE INDEX [IX_Orders_OrderNumber] ON [dbo].[Orders] ([OrderNumber]);
CREATE INDEX [IX_Orders_ShippingAddressId] ON [dbo].[Orders] ([ShippingAddressId]);
CREATE INDEX [IX_Orders_BillingAddressId] ON [dbo].[Orders] ([BillingAddressId]);
CREATE INDEX [IX_OrderItems_OrderId] ON [dbo].[OrderItems] ([OrderId]);
CREATE INDEX [IX_OrderItems_ProductVariantId] ON [dbo].[OrderItems] ([ProductVariantId]);
GO

CREATE UNIQUE INDEX [IX_Customers_UserId] ON [dbo].[Customers] ([UserId]);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims] ([RoleId]);
CREATE INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims] ([UserId]);
CREATE INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins] ([UserId]);
CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles] ([RoleId]);
CREATE INDEX [EmailIndex] ON [dbo].[AspNetUsers] ([NormalizedEmail]);
CREATE UNIQUE INDEX [RoleNameIndex] ON [dbo].[AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
CREATE UNIQUE INDEX [UserNameIndex] ON [dbo].[AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

-- Seed identity data ------------------------------------------------------
DECLARE @AdminRoleId NVARCHAR(450) = N'8f62185f-3c39-47e4-a7a4-87797965d4cf';
DECLARE @CustomerRoleId NVARCHAR(450) = N'7c1c6d84-9398-4d7a-aeb4-685c0de45d8a';
DECLARE @AdminUserId NVARCHAR(450) = N'c62bce4d-6c6d-4b3f-87bd-0ca5cf1709ae';

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp])
VALUES
    (@AdminRoleId, N'Admin', N'ADMIN', N'a3d0f980-2c3f-4cfe-a153-3f92f813d7e5'),
    (@CustomerRoleId, N'Customer', N'CUSTOMER', N'dd6ef317-7c4f-44e9-9b80-0f6456cdfe74');

INSERT INTO [dbo].[AspNetUsers]
([
    Id, FirstName, LastName, UserName, NormalizedUserName, Email, NormalizedEmail,
    EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber,
    PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount
])
VALUES
(
    @AdminUserId,
    N'Capco',
    N'Admin',
    N'admin@capco.local',
    N'ADMIN@CAPCO.LOCAL',
    N'admin@capco.local',
    N'ADMIN@CAPCO.LOCAL',
    1,
    N'AQEAAAAQJwAAEAAAADpBtYbPNCN30GtUp/WKOcGuYIj42Ql5QWJGUyHn95cDD5lmdsOZkYzHoQf1lKhjXw==',
    N'4f7a9c9f-7e43-4f2f-9f19-53f46c7a9aab',
    N'8bd6a964-8fb7-4ce0-8b36-0e17881f7a75',
    NULL,
    0,
    0,
    NULL,
    1,
    0
);

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId])
VALUES
    (@AdminUserId, @AdminRoleId);
GO

-- Seed content blocks -----------------------------------------------------
INSERT INTO [dbo].[ContentBlocks] ([Key], [Html])
VALUES
    (N'about', N'<p>Capco Enterprises Inc. crafts premium Jordan Almonds in East Hanover, New Jersey.</p>'),
    (N'wholesale', N'<p>Contact our wholesale concierge for custom palettes and packaging.</p>'),
    (N'contact', N'<p>Call us at (973) 555-0110 or visit 12 Almond Way, East Hanover, NJ.</p>');
GO

-- Seed product catalog ----------------------------------------------------
DECLARE @CreatedAt DATETIME2 = SYSUTCDATETIME();
DECLARE @WhiteProductId INT;
DECLARE @PinkProductId INT;
DECLARE @BlueProductId INT;
DECLARE @YellowProductId INT;
DECLARE @GreenProductId INT;

INSERT INTO [dbo].[Products] ([Name], [Slug], [Description], [Collection], [IsActive], [CreatedAt])
VALUES
    (N'Jordan Almonds – White', N'jordan-almonds-white', N'Classic white Jordan almonds perfected by Capco Confectionery.', N'Pastel', 1, @CreatedAt);
SET @WhiteProductId = SCOPE_IDENTITY();

INSERT INTO [dbo].[Products] ([Name], [Slug], [Description], [Collection], [IsActive], [CreatedAt])
VALUES
    (N'Jordan Almonds – Pink', N'jordan-almonds-pink', N'Classic pink Jordan almonds perfected by Capco Confectionery.', N'Pastel', 1, @CreatedAt);
SET @PinkProductId = SCOPE_IDENTITY();

INSERT INTO [dbo].[Products] ([Name], [Slug], [Description], [Collection], [IsActive], [CreatedAt])
VALUES
    (N'Jordan Almonds – Blue', N'jordan-almonds-blue', N'Classic blue Jordan almonds perfected by Capco Confectionery.', N'Pastel', 1, @CreatedAt);
SET @BlueProductId = SCOPE_IDENTITY();

INSERT INTO [dbo].[Products] ([Name], [Slug], [Description], [Collection], [IsActive], [CreatedAt])
VALUES
    (N'Jordan Almonds – Yellow', N'jordan-almonds-yellow', N'Classic yellow Jordan almonds perfected by Capco Confectionery.', N'Pastel', 1, @CreatedAt);
SET @YellowProductId = SCOPE_IDENTITY();

INSERT INTO [dbo].[Products] ([Name], [Slug], [Description], [Collection], [IsActive], [CreatedAt])
VALUES
    (N'Jordan Almonds – Green', N'jordan-almonds-green', N'Classic green Jordan almonds perfected by Capco Confectionery.', N'Pastel', 1, @CreatedAt);
SET @GreenProductId = SCOPE_IDENTITY();

-- Images
INSERT INTO [dbo].[ProductImages] ([ProductId], [Url], [Alt], [SortOrder], [IsPrimary])
VALUES
    (@WhiteProductId, N'/images/almonds-white.jpg', N'White Jordan Almonds', 1, 1),
    (@PinkProductId, N'/images/almonds-pink.jpg', N'Pink Jordan Almonds', 1, 1),
    (@BlueProductId, N'/images/almonds-blue.jpg', N'Blue Jordan Almonds', 1, 1),
    (@YellowProductId, N'/images/almonds-yellow.jpg', N'Yellow Jordan Almonds', 1, 1),
    (@GreenProductId, N'/images/almonds-green.jpg', N'Green Jordan Almonds', 1, 1);

-- Variants helper
DECLARE @VariantTable TABLE
(
    ProductId INT,
    Color NVARCHAR(40),
    SizeLabel NVARCHAR(60),
    Sku NVARCHAR(60),
    Price DECIMAL(18,2),
    WeightGrams INT,
    Upc NVARCHAR(32),
    InventoryQty INT
);

INSERT INTO @VariantTable (ProductId, Color, SizeLabel, Sku, Price, WeightGrams, Upc, InventoryQty)
VALUES
    (@WhiteProductId, N'White', N'1 lb pouch', N'CAPCO-WHI-1LB', 12.99, 454,  N'0000000000001', 190),
    (@WhiteProductId, N'White', N'5 lb box',  N'CAPCO-WHI-5LB', 49.99, 2268, N'0000000000002', 180),
    (@WhiteProductId, N'White', N'10 lb box', N'CAPCO-WHI-10LB', 89.99, 4536, N'0000000000003', 170),

    (@PinkProductId, N'Pink', N'1 lb pouch', N'CAPCO-PIN-1LB', 12.99, 454,  N'0000000000001', 190),
    (@PinkProductId, N'Pink', N'5 lb box',  N'CAPCO-PIN-5LB', 49.99, 2268, N'0000000000002', 180),
    (@PinkProductId, N'Pink', N'10 lb box', N'CAPCO-PIN-10LB', 89.99, 4536, N'0000000000003', 170),

    (@BlueProductId, N'Blue', N'1 lb pouch', N'CAPCO-BLU-1LB', 12.99, 454,  N'0000000000001', 190),
    (@BlueProductId, N'Blue', N'5 lb box',  N'CAPCO-BLU-5LB', 49.99, 2268, N'0000000000002', 180),
    (@BlueProductId, N'Blue', N'10 lb box', N'CAPCO-BLU-10LB', 89.99, 4536, N'0000000000003', 170),

    (@YellowProductId, N'Yellow', N'1 lb pouch', N'CAPCO-YEL-1LB', 12.99, 454,  N'0000000000001', 190),
    (@YellowProductId, N'Yellow', N'5 lb box',  N'CAPCO-YEL-5LB', 49.99, 2268, N'0000000000002', 180),
    (@YellowProductId, N'Yellow', N'10 lb box', N'CAPCO-YEL-10LB', 89.99, 4536, N'0000000000003', 170),

    (@GreenProductId, N'Green', N'1 lb pouch', N'CAPCO-GRE-1LB', 12.99, 454,  N'0000000000001', 190),
    (@GreenProductId, N'Green', N'5 lb box',  N'CAPCO-GRE-5LB', 49.99, 2268, N'0000000000002', 180),
    (@GreenProductId, N'Green', N'10 lb box', N'CAPCO-GRE-10LB', 89.99, 4536, N'0000000000003', 170);

INSERT INTO [dbo].[ProductVariants]
([ProductId], [Color], [SizeLabel], [Sku], [Price], [CompareAtPrice], [WeightGrams], [UPC], [InventoryQty], [AllowBackorder], [IsActive])
SELECT ProductId, Color, SizeLabel, Sku, Price, NULL, WeightGrams, Upc, InventoryQty, 1, 1
FROM @VariantTable;
GO

PRINT 'CapcoJordan database successfully provisioned on WINDESKTOP\SQLEXPRESS.';
GO
