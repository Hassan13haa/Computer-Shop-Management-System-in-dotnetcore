using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RenewApplication.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "items",
                columns: table => new
                {
                    i_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    itemname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    gen = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
                    harddisk = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
                    ram = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    purchprice = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    sellprice = table.Column<decimal>(type: "numeric(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_items", x => x.i_id);
                });

            migrationBuilder.CreateTable(
                name: "logins",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    role = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_logins", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "repair",
                columns: table => new
                {
                    rep_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cust_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    item = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    descp = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    rep_cost = table.Column<int>(type: "int", nullable: true),
                    rep_date = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__repair__DC905AF408636631", x => x.rep_id);
                });

            migrationBuilder.CreateTable(
                name: "bill",
                columns: table => new
                {
                    bill_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customer_id = table.Column<int>(type: "int", nullable: false),
                    total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    paid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    due = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    c_phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    b_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    net_total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    discount = table.Column<decimal>(type: "decimal(18,2)", nullable: true, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__bill__D706DDB308A693AB", x => x.bill_id);
                });

            migrationBuilder.CreateTable(
                name: "bill_det",
                columns: table => new
                {
                    bd_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    i_id = table.Column<int>(type: "int", nullable: false),
                    i_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    unit_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    sub_total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    bill_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__bill_det__EC38960AD103E02F", x => x.bd_id);
                    table.ForeignKey(
                        name: "FK_bill_det_bill",
                        column: x => x.bill_id,
                        principalTable: "bill",
                        principalColumn: "bill_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    customer_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customer_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    addres = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    phone = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    email = table.Column<string>(type: "varchar(320)", unicode: false, maxLength: 320, nullable: true),
                    bill_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.customer_id);
                    table.ForeignKey(
                        name: "fk_bill_id",
                        column: x => x.bill_id,
                        principalTable: "bill",
                        principalColumn: "bill_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_bill_customer_id",
                table: "bill",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_bill_det_bill_id",
                table: "bill_det",
                column: "bill_id");

            migrationBuilder.CreateIndex(
                name: "IX_customers_bill_id",
                table: "customers",
                column: "bill_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bill_Customer",
                table: "bill",
                column: "customer_id",
                principalTable: "customers",
                principalColumn: "customer_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bill_Customer",
                table: "bill");

            migrationBuilder.DropTable(
                name: "bill_det");

            migrationBuilder.DropTable(
                name: "items");

            migrationBuilder.DropTable(
                name: "logins");

            migrationBuilder.DropTable(
                name: "repair");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "bill");
        }
    }
}
