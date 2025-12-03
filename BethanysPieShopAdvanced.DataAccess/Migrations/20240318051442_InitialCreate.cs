// Todo: Use nullable in the project file
#nullable disable

namespace BethanysPieShop.DataAccess.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Categories",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Categories", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Orders",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                OrderStatus = table.Column<int>(type: "int", nullable: false),
                FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                AddressLine1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                AddressLine2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                ZipCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                PhoneNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                OrderTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                OrderPlaced = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Orders", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Pies",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                ShortDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                LongDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                AllergyInformation = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ImageThumbnailUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IsPieOfTheWeek = table.Column<bool>(type: "bit", nullable: false),
                InStock = table.Column<bool>(type: "bit", nullable: false),
                CategoryId = table.Column<int>(type: "int", nullable: false),
                RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Pies", x => x.Id);
                table.ForeignKey(
                    name: "FK_Pies_Categories_CategoryId",
                    column: x => x.CategoryId,
                    principalTable: "Categories",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Ingredient",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Amount = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                PieId = table.Column<int>(type: "int", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Ingredient", x => x.Id);
                table.ForeignKey(
                    name: "FK_Ingredient_Pies_PieId",
                    column: x => x.PieId,
                    principalTable: "Pies",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "OrderLines",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                OrderId = table.Column<int>(type: "int", nullable: false),
                PieId = table.Column<int>(type: "int", nullable: false),
                Amount = table.Column<int>(type: "int", nullable: false),
                Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_OrderLines", x => x.Id);
                table.ForeignKey(
                    name: "FK_OrderLines_Orders_OrderId",
                    column: x => x.OrderId,
                    principalTable: "Orders",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_OrderLines_Pies_PieId",
                    column: x => x.PieId,
                    principalTable: "Pies",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Ingredient_PieId",
            table: "Ingredient",
            column: "PieId");

        migrationBuilder.CreateIndex(
            name: "IX_OrderLines_OrderId",
            table: "OrderLines",
            column: "OrderId");

        migrationBuilder.CreateIndex(
            name: "IX_OrderLines_PieId",
            table: "OrderLines",
            column: "PieId");

        migrationBuilder.CreateIndex(
            name: "IX_Pies_CategoryId",
            table: "Pies",
            column: "CategoryId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Ingredient");

        migrationBuilder.DropTable(
            name: "OrderLines");

        migrationBuilder.DropTable(
            name: "Orders");

        migrationBuilder.DropTable(
            name: "Pies");

        migrationBuilder.DropTable(
            name: "Categories");
    }
}
