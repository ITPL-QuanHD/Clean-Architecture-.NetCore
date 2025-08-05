using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopChain.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addtableInventoryStocktakes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InventoryStocktakes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StocktakeNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StocktakeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StoreID = table.Column<int>(type: "int", nullable: false),
                    PerformedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    ConfirmedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConfirmedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryStocktakes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InventoryStocktakes_Stores_StoreID",
                        column: x => x.StoreID,
                        principalTable: "Stores",
                        principalColumn: "StoreID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryStocktakeDetails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StocktakeID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    SystemQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ActualQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryStocktakeDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InventoryStocktakeDetails_InventoryStocktakes_StocktakeID",
                        column: x => x.StocktakeID,
                        principalTable: "InventoryStocktakes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryStocktakeDetails_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryStocktakeDetails_ProductID",
                table: "InventoryStocktakeDetails",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryStocktakeDetails_StocktakeID",
                table: "InventoryStocktakeDetails",
                column: "StocktakeID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryStocktakes_StoreID",
                table: "InventoryStocktakes",
                column: "StoreID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryStocktakeDetails");

            migrationBuilder.DropTable(
                name: "InventoryStocktakes");
        }
    }
}
