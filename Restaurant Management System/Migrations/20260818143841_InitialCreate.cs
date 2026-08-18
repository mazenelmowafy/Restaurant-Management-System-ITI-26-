using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Restaurant_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminId = table.Column<int>(type: "int", nullable: false),
                    ImageFileName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Admins_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admins",
                        principalColumn: "AdminId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "AdminId", "Email", "FirstName", "LastName", "Password" },
                values: new object[] { 1, "ahmed.admin@gmail.com", "Ahmed", "Ali", "123456" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "AdminId", "Category", "Description", "ImageFileName", "IsAvailable", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "Burgers", "Beef burger with cheese and fresh vegetables", "classic_burger.png", true, "Classic Burger", 150m },
                    { 2, 1, "Burgers", "Crispy chicken burger with special sauce", "chicken_burger.png", true, "Chicken Burger", 140m },
                    { 3, 1, "Burgers", "Double beef burger with double cheese", "double_beef_burger.png", true, "Double Beef Burger", 220m },
                    { 4, 1, "Pizza", "Pizza with tomato sauce and mozzarella cheese", "margherita_pizza.png", true, "Margherita Pizza", 180m },
                    { 5, 1, "Pizza", "Pizza topped with chicken and vegetables", "chicken_pizza.png", true, "Chicken Pizza", 230m },
                    { 6, 1, "Pizza", "Pizza with pepperoni and mozzarella cheese", "pepperoni_pizza.png", true, "Pepperoni Pizza", 250m },
                    { 7, 1, "Pasta", "Creamy pasta with grilled chicken", "chicken_pasta.png", true, "Chicken Pasta", 170m },
                    { 8, 1, "Pasta", "Pasta with creamy Alfredo sauce", "alfredo_pasta.png", true, "Alfredo Pasta", 160m },
                    { 9, 1, "Sandwiches", "Grilled chicken sandwich with vegetables", "chicken_sandwich.png", true, "Chicken Sandwich", 120m },
                    { 10, 1, "Sandwiches", "Crispy chicken with special sauce", "crispy_chicken_sandwich.png", true, "Crispy Chicken Sandwich", 130m },
                    { 11, 1, "Sides", "Crispy golden french fries", "french_fries.png", true, "French Fries", 60m },
                    { 12, 1, "Sides", "French fries topped with melted cheese", "cheese_fries.png", true, "Cheese Fries", 85m },
                    { 13, 1, "Meals", "Chicken, fries and soft drink", "chicken_meal.png", true, "Chicken Meal", 260m },
                    { 14, 1, "Meals", "Beef burger, fries and soft drink", "beef_meal.png", true, "Beef Meal", 280m },
                    { 15, 1, "Drinks", "Cold soft drink", "cola.png", true, "Cola", 50m },
                    { 16, 1, "Drinks", "Fresh orange juice", "orange_juice.png", true, "Orange Juice", 70m },
                    { 17, 1, "Drinks", "Fresh strawberry juice", "strawberry_juice.png", true, "Strawberry Juice", 80m },
                    { 18, 1, "Desserts", "Chocolate cake with chocolate sauce", "chocolate_cake.png", true, "Chocolate Cake", 100m },
                    { 19, 1, "Desserts", "Classic creamy cheesecake", "cheesecake.png", true, "Cheesecake", 110m },
                    { 20, 1, "Desserts", "Three scoops of vanilla ice cream", "ice_cream.png", true, "Ice Cream", 90m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_AdminId",
                table: "Products",
                column: "AdminId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Admins");
        }
    }
}
