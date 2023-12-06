using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserProfiles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserProfiles_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Stock = table.Column<short>(type: "smallint", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Neighborhood = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AptNo = table.Column<int>(type: "int", nullable: false),
                    Flat = table.Column<byte>(type: "tinyint", nullable: true),
                    AppUserProfileID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_AppUserProfiles_AppUserProfileID",
                        column: x => x.AppUserProfileID,
                        principalTable: "AppUserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserProfileID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AddressID = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_AppUserProfiles_AppUserProfileID",
                        column: x => x.AppUserProfileID,
                        principalTable: "AppUserProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Quentity = table.Column<short>(type: "smallint", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => new { x.OrderID, x.ProductID });
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4d7b3bc1-f3aa-48ce-b587-5e7dc5553134", "3ce18d41-e4b1-4b05-95bd-b793e3ba848e", "Member", "MEMBER" },
                    { "4d7b3bc1-f3aa-48ce-b587-5e7dc5557634", "4fddc2f3-bf4b-4c7b-a818-1ccf1cb8a800", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedDate", "DeletedDate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "ModifiedDate", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "5c8defd5-91f2-4256-9f16-e7fa7546dec4", 0, "d3c88936-f216-4cc5-9479-51736c976087", new DateTime(2023, 12, 6, 13, 49, 26, 153, DateTimeKind.Local).AddTicks(8768), null, "Admin@gmail.com", true, true, null, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAENc/gj/QZzfTPxop3y4Ba9LqyvCetW3lNczRvDifVIbKITeEBboy6Yss4EHSti76IA==", "5312292928", true, "1275b19e-fed2-4443-a14f-ed83ed0fe010", 1, false, "Admin" },
                    { "5c8defd5-91f2-4256-9f16-e7fa7546fec5", 0, "6b69e06f-18d5-4466-9cf8-6cd6dbb913a1", new DateTime(2023, 12, 6, 13, 49, 26, 153, DateTimeKind.Local).AddTicks(8807), null, "Member@gmail.com", true, true, null, null, "MEMBER@GMAIL.COM", "MEMBER", "AQAAAAEAACcQAAAAED5b+67kz8MN9FBzqjKL//DLrHMtI9Rke4xYh/Nb7uan96D9hbn3HaeXNzC8zIfDcA==", "5446340539", true, "3abe98a2-5f1a-4f52-9323-9d0e2dbed3e2", 1, false, "Member" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Description", "ModifiedDate", "Name", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 6, 13, 49, 26, 156, DateTimeKind.Local).AddTicks(2876), null, null, null, "Oda Kokusu & Mum", 1 },
                    { 2, new DateTime(2023, 12, 6, 13, 49, 26, 156, DateTimeKind.Local).AddTicks(2879), null, null, null, "Difüzör", 1 },
                    { 3, new DateTime(2023, 12, 6, 13, 49, 26, 156, DateTimeKind.Local).AddTicks(2881), null, null, null, "Ev Parfümü", 1 }
                });

            migrationBuilder.InsertData(
                table: "AppUserProfiles",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "FirstName", "LastName", "ModifiedDate", "Status" },
                values: new object[,]
                {
                    { "5c8defd5-91f2-4256-9f16-e7fa7546dec4", new DateTime(2023, 12, 6, 13, 49, 26, 156, DateTimeKind.Local).AddTicks(2305), null, "Tuğberk", "Mehdioğlu", null, 1 },
                    { "5c8defd5-91f2-4256-9f16-e7fa7546fec5", new DateTime(2023, 12, 6, 13, 49, 26, 156, DateTimeKind.Local).AddTicks(2310), null, "Dilan", "Polat", null, 1 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "4d7b3bc1-f3aa-48ce-b587-5e7dc5557634", "5c8defd5-91f2-4256-9f16-e7fa7546dec4" },
                    { "4d7b3bc1-f3aa-48ce-b587-5e7dc5553134", "5c8defd5-91f2-4256-9f16-e7fa7546fec5" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryID", "CreatedDate", "DeletedDate", "Description", "ImagePath", "ModifiedDate", "Name", "Price", "Status", "Stock" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 12, 6, 13, 49, 26, 156, DateTimeKind.Local).AddTicks(4447), null, "Oud Nobile Sprey Cam Şişe Koku 100 ml", "Dr.VranjesFirenze.jpg", null, "Dr. Vranjes Firenze", 1199m, 1, (short)60 },
                    { 2, 1, new DateTime(2023, 12, 6, 13, 49, 26, 156, DateTimeKind.Local).AddTicks(4453), null, "Bereket Çubuklu Oda Kokusu 200ml", "Dr.VranjesFirenze.jpg", null, "Atelier Rebul", 1249m, 1, (short)5 },
                    { 3, 2, new DateTime(2023, 12, 6, 13, 49, 26, 156, DateTimeKind.Local).AddTicks(4455), null, "Leather Oud 100 ml Difüzör", "LeatherOud100mlDifüzör.jpg", null, "Dr. Vranjes Firenze", 1199m, 1, (short)30 },
                    { 4, 2, new DateTime(2023, 12, 6, 13, 49, 26, 156, DateTimeKind.Local).AddTicks(4456), null, "Ginger Lime 2500 ml Difüzör", "5pnyh3oo.g0a_IMG_01_2110095020048.jpg", null, "Dr. Vranjes Firenze", 8950m, 1, (short)80 },
                    { 5, 3, new DateTime(2023, 12, 6, 13, 49, 26, 156, DateTimeKind.Local).AddTicks(4458), null, "Penelope Refill 500 ml Oda Kokusu", "qsbkczrj.lq0_IMG_01_2110089386044.jpg", null, "Etro", 1249m, 1, (short)43 },
                    { 6, 3, new DateTime(2023, 12, 6, 13, 49, 26, 156, DateTimeKind.Local).AddTicks(4460), null, "Eos 100 ml Oda Kokusu", "kh1gwhxa.2uh_IMG_01_2110089385948.jpg", null, "Etro", 1449m, 1, (short)100 },
                    { 7, 1, new DateTime(2023, 12, 6, 13, 49, 26, 156, DateTimeKind.Local).AddTicks(4461), null, "Bubble Mor Pembe ve Su Yeşili 3'lü Mum Seti", "ptowbl3w.zkq_MP_1b565ca5-4923-4c99-9313-e81502939778_1_43487206954810324050030543535_563.jpg", null, "Lagom Candle", 1280m, 1, (short)8 },
                    { 8, 1, new DateTime(2023, 12, 6, 13, 49, 26, 156, DateTimeKind.Local).AddTicks(4463), null, "Baccarat Rouge 540 Kokulu Mum 280 gr", "x5p2g5gu.4fw_IMG_01_3700559608067.jpg", null, "Maison Francis Kurkdjian", 4590m, 1, (short)24 },
                    { 9, 1, new DateTime(2023, 12, 6, 13, 49, 26, 156, DateTimeKind.Local).AddTicks(4464), null, "İstanbul Çubuklu Oda Kokusu 2500ml", "ihlf3u2z.vwy_IMG_01_8691226631783.jpg", null, "Atelier Rebul", 10250m, 1, (short)18 }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "AppUserProfileID", "AptNo", "City", "Country", "CreatedDate", "DeletedDate", "District", "Flat", "ModifiedDate", "Name", "Neighborhood", "Status", "Street" },
                values: new object[] { 1, "5c8defd5-91f2-4256-9f16-e7fa7546dec4", 11, "İstanbul", "Türkiye", new DateTime(2023, 12, 6, 13, 49, 26, 153, DateTimeKind.Local).AddTicks(7533), null, "Kağıthane", (byte)8, null, "Ev", "Çeliktepe", 1, "ŞaşatuŞalat" });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "AppUserProfileID", "AptNo", "City", "Country", "CreatedDate", "DeletedDate", "District", "Flat", "ModifiedDate", "Name", "Neighborhood", "Status", "Street" },
                values: new object[] { 2, "5c8defd5-91f2-4256-9f16-e7fa7546dec4", 7, "İstanbul", "Türkiye", new DateTime(2023, 12, 6, 13, 49, 26, 153, DateTimeKind.Local).AddTicks(7540), null, "Beşiktaş", null, null, "İş Yeri", "Nispetiye", 1, "Aydın" });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "AppUserProfileID", "AptNo", "City", "Country", "CreatedDate", "DeletedDate", "District", "Flat", "ModifiedDate", "Name", "Neighborhood", "Status", "Street" },
                values: new object[] { 3, "5c8defd5-91f2-4256-9f16-e7fa7546fec5", 9, "İstanbul", "Türkiye", new DateTime(2023, 12, 6, 13, 49, 26, 153, DateTimeKind.Local).AddTicks(7542), null, "Ataşehir", null, null, "Ev", "Küçükbakkalköy", 1, "Rüya" });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_AppUserProfileID",
                table: "Addresses",
                column: "AppUserProfileID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductID",
                table: "OrderDetails",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AddressID",
                table: "Orders",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AppUserProfileID",
                table: "Orders",
                column: "AppUserProfileID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "AppUserProfiles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
