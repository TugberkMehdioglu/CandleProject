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
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
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
                    { "4d7b3bc1-f3aa-48ce-b587-5e7dc5553134", "20b31218-037a-4e4b-a692-cdbb0a0f39d0", "Member", "MEMBER" },
                    { "4d7b3bc1-f3aa-48ce-b587-5e7dc5557634", "205ef380-37cc-4485-89b9-992edfc4d505", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedDate", "DeletedDate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "ModifiedDate", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "5c8defd5-91f2-4256-9f16-e7fa7546dec4", 0, "11a75701-187b-4944-873a-0db49d99d66c", new DateTime(2024, 1, 29, 13, 37, 8, 79, DateTimeKind.Local).AddTicks(4531), null, "Admin@gmail.com", true, true, null, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEEn/RAVdV8626ZngvMzCG2+uZErFrA41JKuDyx5UorxRezONkHnwEb0vGwTMKWVhwg==", "5312292928", true, "d0827e8e-79bf-4230-bf72-3ab2933657c8", 1, false, "Admin" },
                    { "5c8defd5-91f2-4256-9f16-e7fa7546fec5", 0, "9c83dfd7-fe0e-494a-be52-090335450e0d", new DateTime(2024, 1, 29, 13, 37, 8, 79, DateTimeKind.Local).AddTicks(4554), null, "Member@gmail.com", true, true, null, null, "MEMBER@GMAIL.COM", "MEMBER", "AQAAAAEAACcQAAAAEB/sXA/np5YJMAY+pSCt5oUP7kSfNcweSV0ay4XATmzdsGAIUJbp67lhEm2U5Lv0Og==", "5446340539", true, "9a0aab12-74cb-4275-b9f9-8fdf2f028b1c", 1, false, "Member" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Description", "ModifiedDate", "Name", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 29, 13, 37, 8, 81, DateTimeKind.Local).AddTicks(4938), null, null, null, "Oda Kokusu & Mum", 1 },
                    { 2, new DateTime(2024, 1, 29, 13, 37, 8, 81, DateTimeKind.Local).AddTicks(4940), null, null, null, "Difüzör", 1 },
                    { 3, new DateTime(2024, 1, 29, 13, 37, 8, 81, DateTimeKind.Local).AddTicks(4941), null, null, null, "Ev Parfümü", 1 }
                });

            migrationBuilder.InsertData(
                table: "AppUserProfiles",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "FirstName", "LastName", "ModifiedDate", "Status" },
                values: new object[,]
                {
                    { "5c8defd5-91f2-4256-9f16-e7fa7546dec4", new DateTime(2024, 1, 29, 13, 37, 8, 81, DateTimeKind.Local).AddTicks(4541), null, "Tuğberk", "Mehdioğlu", null, 1 },
                    { "5c8defd5-91f2-4256-9f16-e7fa7546fec5", new DateTime(2024, 1, 29, 13, 37, 8, 81, DateTimeKind.Local).AddTicks(4545), null, "Dilan", "Polat", null, 1 }
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
                columns: new[] { "Id", "CategoryID", "CreatedDate", "DeletedDate", "Description", "ModifiedDate", "Name", "Price", "Status", "Stock" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 1, 29, 13, 37, 8, 81, DateTimeKind.Local).AddTicks(6794), null, "Oud Nobile Sprey Cam Şişe Koku 100 ml", null, "Dr. Vranjes Firenze", 1199m, 1, (short)60 },
                    { 2, 1, new DateTime(2024, 1, 29, 13, 37, 8, 81, DateTimeKind.Local).AddTicks(6800), null, "Bereket Çubuklu Oda Kokusu 200ml", null, "Atelier Rebul", 1249m, 1, (short)5 },
                    { 3, 2, new DateTime(2024, 1, 29, 13, 37, 8, 81, DateTimeKind.Local).AddTicks(6802), null, "Leather Oud 100 ml Difüzör", null, "Dr. Vranjes Firenze", 1199m, 1, (short)30 },
                    { 4, 2, new DateTime(2024, 1, 29, 13, 37, 8, 81, DateTimeKind.Local).AddTicks(6803), null, "Ginger Lime 2500 ml Difüzör", null, "Dr. Vranjes Firenze", 8950m, 1, (short)80 },
                    { 5, 3, new DateTime(2024, 1, 29, 13, 37, 8, 81, DateTimeKind.Local).AddTicks(6804), null, "Penelope Refill 500 ml Oda Kokusu", null, "Etro", 1249m, 1, (short)43 },
                    { 6, 3, new DateTime(2024, 1, 29, 13, 37, 8, 81, DateTimeKind.Local).AddTicks(6806), null, "Eos 100 ml Oda Kokusu", null, "Etro", 1449m, 1, (short)100 },
                    { 7, 1, new DateTime(2024, 1, 29, 13, 37, 8, 81, DateTimeKind.Local).AddTicks(6808), null, "Bubble Mor Pembe ve Su Yeşili 3'lü Mum Seti", null, "Lagom Candle", 1280m, 1, (short)8 },
                    { 8, 1, new DateTime(2024, 1, 29, 13, 37, 8, 81, DateTimeKind.Local).AddTicks(6809), null, "Baccarat Rouge 540 Kokulu Mum 280 gr", null, "Maison Francis Kurkdjian", 4590m, 1, (short)24 },
                    { 9, 1, new DateTime(2024, 1, 29, 13, 37, 8, 81, DateTimeKind.Local).AddTicks(6810), null, "İstanbul Çubuklu Oda Kokusu 2500ml", null, "Atelier Rebul", 10250m, 1, (short)18 }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "AppUserProfileID", "AptNo", "City", "Country", "CreatedDate", "DeletedDate", "District", "Flat", "ModifiedDate", "Name", "Neighborhood", "Status", "Street" },
                values: new object[,]
                {
                    { 1, "5c8defd5-91f2-4256-9f16-e7fa7546dec4", 11, "İstanbul", "Türkiye", new DateTime(2024, 1, 29, 13, 37, 8, 79, DateTimeKind.Local).AddTicks(3191), null, "Kağıthane", (byte)8, null, "Ev", "Çeliktepe", 1, "ŞaşatuŞalat" },
                    { 2, "5c8defd5-91f2-4256-9f16-e7fa7546dec4", 7, "İstanbul", "Türkiye", new DateTime(2024, 1, 29, 13, 37, 8, 79, DateTimeKind.Local).AddTicks(3196), null, "Beşiktaş", null, null, "İş Yeri", "Nispetiye", 1, "Aydın" },
                    { 3, "5c8defd5-91f2-4256-9f16-e7fa7546fec5", 9, "İstanbul", "Türkiye", new DateTime(2024, 1, 29, 13, 37, 8, 79, DateTimeKind.Local).AddTicks(3198), null, "Ataşehir", null, null, "Ev", "Küçükbakkalköy", 1, "Rüya" }
                });

            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "ImagePath", "ModifiedDate", "ProductId", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 29, 13, 37, 8, 81, DateTimeKind.Local).AddTicks(6241), null, "Dr.VranjesFirenze.jpg", null, 1, 1 },
                    { 2, new DateTime(2024, 1, 29, 13, 37, 8, 81, DateTimeKind.Local).AddTicks(6244), null, "AtelierRebul.jpg", null, 2, 1 },
                    { 3, new DateTime(2024, 1, 29, 13, 37, 8, 81, DateTimeKind.Local).AddTicks(6245), null, "LeatherOud100mlDifüzör.jpg", null, 3, 1 },
                    { 4, new DateTime(2024, 1, 29, 13, 37, 8, 81, DateTimeKind.Local).AddTicks(6246), null, "5pnyh3oo.g0a_IMG_01_2110095020048.jpg", null, 4, 1 },
                    { 5, new DateTime(2024, 1, 29, 13, 37, 8, 81, DateTimeKind.Local).AddTicks(6248), null, "qsbkczrj.lq0_IMG_01_2110089386044.jpg", null, 5, 1 },
                    { 6, new DateTime(2024, 1, 29, 13, 37, 8, 81, DateTimeKind.Local).AddTicks(6271), null, "kh1gwhxa.2uh_IMG_01_2110089385948.jpg", null, 6, 1 },
                    { 7, new DateTime(2024, 1, 29, 13, 37, 8, 81, DateTimeKind.Local).AddTicks(6272), null, "ptowbl3w.zkq_MP_1b565ca5-4923-4c99-9313-e81502939778_1_43487206954810324050030543535_563.jpg", null, 7, 1 },
                    { 8, new DateTime(2024, 1, 29, 13, 37, 8, 81, DateTimeKind.Local).AddTicks(6273), null, "x5p2g5gu.4fw_IMG_01_3700559608067.jpg", null, 8, 1 },
                    { 9, new DateTime(2024, 1, 29, 13, 37, 8, 81, DateTimeKind.Local).AddTicks(6274), null, "ihlf3u2z.vwy_IMG_01_8691226631783.jpg", null, 9, 1 },
                    { 10, new DateTime(2024, 1, 29, 13, 37, 8, 81, DateTimeKind.Local).AddTicks(6275), null, "rblnyed2.jvf_IMG_02_2110095020048.jpg", null, 4, 1 },
                    { 11, new DateTime(2024, 1, 29, 13, 37, 8, 81, DateTimeKind.Local).AddTicks(6276), null, "vb0cwnjy.qfj_IMG_03_2110095020048.jpg", null, 4, 1 },
                    { 12, new DateTime(2024, 1, 29, 13, 37, 8, 81, DateTimeKind.Local).AddTicks(6277), null, "0fbp5mpz.kbk_IMG_05_8691226631783.jpg", null, 9, 1 },
                    { 13, new DateTime(2024, 1, 29, 13, 37, 8, 81, DateTimeKind.Local).AddTicks(6278), null, "q5hq1lxg.y3y_IMG_03_8691226631783.jpg", null, 9, 1 },
                    { 14, new DateTime(2024, 1, 29, 13, 37, 8, 81, DateTimeKind.Local).AddTicks(6279), null, "3334.jpg", null, 3, 1 },
                    { 15, new DateTime(2024, 1, 29, 13, 37, 8, 81, DateTimeKind.Local).AddTicks(6280), null, "fpzkb4bw.di1_IMG_03_3700559608067.jpg", null, 8, 1 },
                    { 16, new DateTime(2024, 1, 29, 13, 37, 8, 81, DateTimeKind.Local).AddTicks(6281), null, "iz2behpe.ci3_IMG_02_3700559608067.jpg", null, 8, 1 }
                });

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
                name: "IX_Photos_ProductId",
                table: "Photos",
                column: "ProductId");

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
                name: "Photos");

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
