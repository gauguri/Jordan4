-- Schema initialization for Capco Jordan Almonds Store
CREATE TABLE AspNetRoles (
    Id nvarchar(450) NOT NULL PRIMARY KEY,
    Name nvarchar(256) NULL,
    NormalizedName nvarchar(256) NULL,
    ConcurrencyStamp nvarchar(max) NULL
);

CREATE UNIQUE INDEX IX_AspNetRoles_NormalizedName ON AspNetRoles(NormalizedName) WHERE NormalizedName IS NOT NULL;

CREATE TABLE AspNetUsers (
    Id nvarchar(450) NOT NULL PRIMARY KEY,
    FirstName nvarchar(256) NULL,
    LastName nvarchar(256) NULL,
    UserName nvarchar(256) NULL,
    NormalizedUserName nvarchar(256) NULL,
    Email nvarchar(256) NULL,
    NormalizedEmail nvarchar(256) NULL,
    EmailConfirmed bit NOT NULL DEFAULT 0,
    PasswordHash nvarchar(max) NULL,
    SecurityStamp nvarchar(max) NULL,
    ConcurrencyStamp nvarchar(max) NULL,
    PhoneNumber nvarchar(max) NULL,
    PhoneNumberConfirmed bit NOT NULL DEFAULT 0,
    TwoFactorEnabled bit NOT NULL DEFAULT 0,
    LockoutEnd datetimeoffset NULL,
    LockoutEnabled bit NOT NULL DEFAULT 0,
    AccessFailedCount int NOT NULL DEFAULT 0
);

CREATE INDEX IX_AspNetUsers_NormalizedEmail ON AspNetUsers(NormalizedEmail);
CREATE UNIQUE INDEX IX_AspNetUsers_NormalizedUserName ON AspNetUsers(NormalizedUserName) WHERE NormalizedUserName IS NOT NULL;

CREATE TABLE AspNetRoleClaims (
    Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    RoleId nvarchar(450) NOT NULL,
    ClaimType nvarchar(max) NULL,
    ClaimValue nvarchar(max) NULL,
    CONSTRAINT FK_AspNetRoleClaims_Roles FOREIGN KEY(RoleId) REFERENCES AspNetRoles(Id) ON DELETE CASCADE
);

CREATE TABLE AspNetUserClaims (
    Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    UserId nvarchar(450) NOT NULL,
    ClaimType nvarchar(max) NULL,
    ClaimValue nvarchar(max) NULL,
    CONSTRAINT FK_AspNetUserClaims_Users FOREIGN KEY(UserId) REFERENCES AspNetUsers(Id) ON DELETE CASCADE
);

CREATE TABLE AspNetUserLogins (
    LoginProvider nvarchar(128) NOT NULL,
    ProviderKey nvarchar(128) NOT NULL,
    ProviderDisplayName nvarchar(max) NULL,
    UserId nvarchar(450) NOT NULL,
    PRIMARY KEY(LoginProvider, ProviderKey),
    CONSTRAINT FK_AspNetUserLogins_Users FOREIGN KEY(UserId) REFERENCES AspNetUsers(Id) ON DELETE CASCADE
);

CREATE TABLE AspNetUserRoles (
    UserId nvarchar(450) NOT NULL,
    RoleId nvarchar(450) NOT NULL,
    PRIMARY KEY(UserId, RoleId),
    CONSTRAINT FK_AspNetUserRoles_Users FOREIGN KEY(UserId) REFERENCES AspNetUsers(Id) ON DELETE CASCADE,
    CONSTRAINT FK_AspNetUserRoles_Roles FOREIGN KEY(RoleId) REFERENCES AspNetRoles(Id) ON DELETE CASCADE
);

CREATE TABLE AspNetUserTokens (
    UserId nvarchar(450) NOT NULL,
    LoginProvider nvarchar(128) NOT NULL,
    Name nvarchar(128) NOT NULL,
    Value nvarchar(max) NULL,
    PRIMARY KEY(UserId, LoginProvider, Name),
    CONSTRAINT FK_AspNetUserTokens_Users FOREIGN KEY(UserId) REFERENCES AspNetUsers(Id) ON DELETE CASCADE
);

CREATE TABLE ContentBlocks (
    Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Key] nvarchar(80) NOT NULL,
    Html nvarchar(max) NOT NULL
);
CREATE UNIQUE INDEX IX_ContentBlocks_Key ON ContentBlocks([Key]);

CREATE TABLE Products (
    Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Name nvarchar(160) NOT NULL,
    Slug nvarchar(160) NOT NULL,
    Description nvarchar(4000) NULL,
    Collection nvarchar(80) NULL,
    IsActive bit NOT NULL DEFAULT 1,
    CreatedAt datetime2 NOT NULL DEFAULT SYSUTCDATETIME()
);
CREATE UNIQUE INDEX IX_Products_Slug ON Products(Slug);

CREATE TABLE ProductImages (
    Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    ProductId int NOT NULL,
    Url nvarchar(512) NOT NULL,
    Alt nvarchar(160) NULL,
    SortOrder int NOT NULL DEFAULT 0,
    IsPrimary bit NOT NULL DEFAULT 0,
    CONSTRAINT FK_ProductImages_Products FOREIGN KEY(ProductId) REFERENCES Products(Id) ON DELETE CASCADE
);

CREATE TABLE ProductVariants (
    Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    ProductId int NOT NULL,
    Color nvarchar(40) NOT NULL,
    SizeLabel nvarchar(60) NOT NULL,
    Sku nvarchar(60) NOT NULL,
    Price decimal(18,2) NOT NULL,
    CompareAtPrice decimal(18,2) NULL,
    WeightGrams int NOT NULL,
    UPC nvarchar(32) NULL,
    InventoryQty int NOT NULL,
    AllowBackorder bit NOT NULL DEFAULT 0,
    IsActive bit NOT NULL DEFAULT 1,
    CONSTRAINT FK_ProductVariants_Products FOREIGN KEY(ProductId) REFERENCES Products(Id) ON DELETE CASCADE
);
CREATE UNIQUE INDEX IX_ProductVariants_Sku ON ProductVariants(Sku);

CREATE TABLE Customers (
    Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    UserId nvarchar(450) NOT NULL,
    Email nvarchar(256) NOT NULL,
    FirstName nvarchar(80) NULL,
    LastName nvarchar(80) NULL,
    Phone nvarchar(max) NULL,
    CONSTRAINT FK_Customers_Users FOREIGN KEY(UserId) REFERENCES AspNetUsers(Id) ON DELETE CASCADE
);
CREATE UNIQUE INDEX IX_Customers_UserId ON Customers(UserId);

CREATE TABLE Addresses (
    Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    CustomerId int NULL,
    Line1 nvarchar(160) NOT NULL,
    Line2 nvarchar(160) NULL,
    City nvarchar(80) NOT NULL,
    State nvarchar(60) NOT NULL,
    Zip nvarchar(20) NOT NULL,
    Country nvarchar(60) NOT NULL,
    [Type] nvarchar(40) NOT NULL,
    CONSTRAINT FK_Addresses_Customers FOREIGN KEY(CustomerId) REFERENCES Customers(Id) ON DELETE NO ACTION
);

CREATE TABLE Carts (
    Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    CustomerId int NULL,
    GuestToken nvarchar(64) NULL,
    CreatedAt datetime2 NOT NULL DEFAULT SYSUTCDATETIME(),
    UpdatedAt datetime2 NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_Carts_Customers FOREIGN KEY(CustomerId) REFERENCES Customers(Id) ON DELETE NO ACTION
);

CREATE TABLE CartItems (
    Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    CartId int NOT NULL,
    ProductVariantId int NOT NULL,
    Qty int NOT NULL,
    UnitPriceSnapshot decimal(18,2) NOT NULL,
    CONSTRAINT FK_CartItems_Carts FOREIGN KEY(CartId) REFERENCES Carts(Id) ON DELETE CASCADE,
    CONSTRAINT FK_CartItems_ProductVariants FOREIGN KEY(ProductVariantId) REFERENCES ProductVariants(Id) ON DELETE NO ACTION
);

CREATE TABLE Orders (
    Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    OrderNumber nvarchar(32) NOT NULL,
    CustomerId int NULL,
    Email nvarchar(256) NOT NULL,
    Status nvarchar(32) NOT NULL,
    Subtotal decimal(18,2) NOT NULL,
    Shipping decimal(18,2) NOT NULL,
    Tax decimal(18,2) NOT NULL,
    Total decimal(18,2) NOT NULL,
    StripePaymentIntentId nvarchar(160) NULL,
    CreatedAt datetime2 NOT NULL DEFAULT SYSUTCDATETIME(),
    ShippingAddressId int NULL,
    BillingAddressId int NULL,
    CONSTRAINT FK_Orders_Customers FOREIGN KEY(CustomerId) REFERENCES Customers(Id) ON DELETE NO ACTION,
    CONSTRAINT FK_Orders_ShippingAddress FOREIGN KEY(ShippingAddressId) REFERENCES Addresses(Id) ON DELETE NO ACTION,
    CONSTRAINT FK_Orders_BillingAddress FOREIGN KEY(BillingAddressId) REFERENCES Addresses(Id) ON DELETE NO ACTION
);
CREATE UNIQUE INDEX IX_Orders_OrderNumber ON Orders(OrderNumber);

CREATE TABLE OrderItems (
    Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    OrderId int NOT NULL,
    ProductVariantId int NOT NULL,
    NameSnapshot nvarchar(160) NOT NULL,
    ColorSnapshot nvarchar(40) NULL,
    SizeSnapshot nvarchar(60) NULL,
    Qty int NOT NULL,
    UnitPriceSnapshot decimal(18,2) NOT NULL,
    CONSTRAINT FK_OrderItems_Orders FOREIGN KEY(OrderId) REFERENCES Orders(Id) ON DELETE CASCADE,
    CONSTRAINT FK_OrderItems_ProductVariants FOREIGN KEY(ProductVariantId) REFERENCES ProductVariants(Id) ON DELETE NO ACTION
);
