using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShopMvc.Inf.Migrations
{
    /// <inheritdoc />
    public partial class Final2vBeta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adresses_Clients_ClientId",
                table: "Adresses");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_Orders_OrdersId",
                table: "OrderProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_Products_ProductsId",
                table: "OrderProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTag_Products_ProductsId",
                table: "ProductTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTag_Tags_TagsId",
                table: "ProductTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Adresses",
                table: "Adresses");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Tags");

            migrationBuilder.RenameTable(
                name: "Adresses",
                newName: "Addresses");

            migrationBuilder.RenameColumn(
                name: "TagsId",
                table: "ProductTag",
                newName: "TagId");

            migrationBuilder.RenameColumn(
                name: "ProductsId",
                table: "ProductTag",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTag_TagsId",
                table: "ProductTag",
                newName: "IX_ProductTag_TagId");

            migrationBuilder.RenameColumn(
                name: "ProductsId",
                table: "OrderProduct",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "OrdersId",
                table: "OrderProduct",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProduct_ProductsId",
                table: "OrderProduct",
                newName: "IX_OrderProduct_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Adresses_ClientId",
                table: "Addresses",
                newName: "IX_Addresses_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Clients_ClientId",
                table: "Addresses",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_Orders_OrderId",
                table: "OrderProduct",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_Products_ProductId",
                table: "OrderProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTag_Products_ProductId",
                table: "ProductTag",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTag_Tags_TagId",
                table: "ProductTag",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Clients_ClientId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_Orders_OrderId",
                table: "OrderProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_Products_ProductId",
                table: "OrderProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTag_Products_ProductId",
                table: "ProductTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTag_Tags_TagId",
                table: "ProductTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "Adresses");

            migrationBuilder.RenameColumn(
                name: "TagId",
                table: "ProductTag",
                newName: "TagsId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductTag",
                newName: "ProductsId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTag_TagId",
                table: "ProductTag",
                newName: "IX_ProductTag_TagsId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "OrderProduct",
                newName: "ProductsId");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrderProduct",
                newName: "OrdersId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProduct_ProductId",
                table: "OrderProduct",
                newName: "IX_OrderProduct_ProductsId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_ClientId",
                table: "Adresses",
                newName: "IX_Adresses_ClientId");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Tags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adresses",
                table: "Adresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Adresses_Clients_ClientId",
                table: "Adresses",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_Orders_OrdersId",
                table: "OrderProduct",
                column: "OrdersId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_Products_ProductsId",
                table: "OrderProduct",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTag_Products_ProductsId",
                table: "ProductTag",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTag_Tags_TagsId",
                table: "ProductTag",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
