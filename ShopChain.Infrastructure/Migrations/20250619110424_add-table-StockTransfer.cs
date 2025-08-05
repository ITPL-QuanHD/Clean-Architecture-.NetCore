using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopChain.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addtableStockTransfer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockTransfers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransferNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransferDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StoreID_From = table.Column<int>(type: "int", nullable: false),
                    StoreID_To = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    ConfirmedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConfirmedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoreID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTransfers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StockTransfers_Stores_StoreID",
                        column: x => x.StoreID,
                        principalTable: "Stores",
                        principalColumn: "StoreID");
                    table.ForeignKey(
                        name: "FK_StockTransfers_Stores_StoreID_From",
                        column: x => x.StoreID_From,
                        principalTable: "Stores",
                        principalColumn: "StoreID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockTransfers_Stores_StoreID_To",
                        column: x => x.StoreID_To,
                        principalTable: "Stores",
                        principalColumn: "StoreID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockTransferDetails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransferID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTransferDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StockTransferDetails_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockTransferDetails_StockTransfers_TransferID",
                        column: x => x.TransferID,
                        principalTable: "StockTransfers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockTransferDetails_ProductID",
                table: "StockTransferDetails",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransferDetails_TransferID",
                table: "StockTransferDetails",
                column: "TransferID");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransfers_StoreID",
                table: "StockTransfers",
                column: "StoreID");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransfers_StoreID_From",
                table: "StockTransfers",
                column: "StoreID_From");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransfers_StoreID_To",
                table: "StockTransfers",
                column: "StoreID_To");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockTransferDetails");

            migrationBuilder.DropTable(
                name: "StockTransfers");
        }
    }
}
