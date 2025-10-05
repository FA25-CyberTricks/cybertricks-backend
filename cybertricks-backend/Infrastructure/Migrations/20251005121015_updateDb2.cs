using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace ct.backend.Migrations
{
    /// <inheritdoc />
    public partial class updateDb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Visited",
                table: "Stores",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "favorites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_favorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_favorites_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_favorites_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "pricing_rules",
                columns: table => new
                {
                    PricingRuleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    RoomType = table.Column<string>(type: "varchar(255)", nullable: true),
                    BasePricePerHour = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    StartHour = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    EndHour = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    HourlyMultiplier = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    DayOfWeek = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "varchar(255)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pricing_rules", x => x.PricingRuleId);
                    table.ForeignKey(
                        name: "FK_pricing_rules_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "vouchers",
                columns: table => new
                {
                    VoucherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    DiscountPercent = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    MinOrderAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    MaxDiscountAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    UsageLimit = table.Column<int>(type: "int", nullable: true),
                    UsedCount = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "varchar(255)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vouchers", x => x.VoucherId);
                    table.CheckConstraint("ck_voucher_dates", "(EndDate >= StartDate)");
                    table.CheckConstraint("ck_voucher_discount_one", "(\r\n                      (DiscountAmount IS NOT NULL AND DiscountAmount > 0 AND (DiscountPercent IS NULL OR DiscountPercent = 0))\r\n                      OR (DiscountPercent IS NOT NULL AND DiscountPercent > 0 AND DiscountAmount IS NULL)\r\n                    )");
                    table.CheckConstraint("ck_voucher_percent_range", "(DiscountPercent IS NULL OR (DiscountPercent >= 0 AND DiscountPercent <= 100))");
                    table.CheckConstraint("ck_voucher_usage_limit", "(UsageLimit IS NULL OR UsageLimit >= UsedCount)");
                    table.ForeignKey(
                        name: "FK_vouchers_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "voucher_usages",
                columns: table => new
                {
                    VoucherUsageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    VoucherId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    UsedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_voucher_usages", x => x.VoucherUsageId);
                    table.ForeignKey(
                        name: "FK_voucher_usages_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_voucher_usages_vouchers_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "vouchers",
                        principalColumn: "VoucherId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_favorites_StoreId",
                table: "favorites",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_favorites_UserId_StoreId",
                table: "favorites",
                columns: new[] { "UserId", "StoreId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_pricing_rules_StoreId_RoomType_DayOfWeek_StartHour_EndHour_S~",
                table: "pricing_rules",
                columns: new[] { "StoreId", "RoomType", "DayOfWeek", "StartHour", "EndHour", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_voucher_usages_UserId",
                table: "voucher_usages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_voucher_usages_VoucherId_UserId",
                table: "voucher_usages",
                columns: new[] { "VoucherId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vouchers_Code",
                table: "vouchers",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vouchers_StoreId_Status_StartDate_EndDate",
                table: "vouchers",
                columns: new[] { "StoreId", "Status", "StartDate", "EndDate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "favorites");

            migrationBuilder.DropTable(
                name: "pricing_rules");

            migrationBuilder.DropTable(
                name: "voucher_usages");

            migrationBuilder.DropTable(
                name: "vouchers");

            migrationBuilder.DropColumn(
                name: "Visited",
                table: "Stores");
        }
    }
}
