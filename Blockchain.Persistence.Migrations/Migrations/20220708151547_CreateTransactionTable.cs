using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blockchain.Persistence.Migrations.Migrations
{
    public partial class CreateTransactionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "Blocks");

            migrationBuilder.EnsureSchema(
                name: "lookup");

            migrationBuilder.AlterColumn<long>(
                name: "Index",
                table: "Blocks",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<long>(
                name: "Nonce",
                table: "Blocks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    BlockId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Blocks_BlockId",
                        column: x => x.BlockId,
                        principalTable: "Blocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionStatus",
                schema: "lookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Blockchain",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionStatusId = table.Column<int>(type: "int", nullable: false),
                    BlockId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blockchain", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blockchain_Blocks_BlockId",
                        column: x => x.BlockId,
                        principalTable: "Blocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Blockchain_TransactionStatus_TransactionStatusId",
                        column: x => x.TransactionStatusId,
                        principalSchema: "lookup",
                        principalTable: "TransactionStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blocks_Index",
                table: "Blocks",
                column: "Index",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blockchain_BlockId",
                table: "Blockchain",
                column: "BlockId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blockchain_TransactionStatusId",
                table: "Blockchain",
                column: "TransactionStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BlockId",
                table: "Transactions",
                column: "BlockId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blockchain");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "TransactionStatus",
                schema: "lookup");

            migrationBuilder.DropIndex(
                name: "IX_Blocks_Index",
                table: "Blocks");

            migrationBuilder.DropColumn(
                name: "Nonce",
                table: "Blocks");

            migrationBuilder.AlterColumn<int>(
                name: "Index",
                table: "Blocks",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "Blocks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
