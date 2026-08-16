using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Restaurant_Management_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRestaurantEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminId = table.Column<int>(type: "int", nullable: false)
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
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
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
                columns: new[] { "AdminId", "Email", "FirstName", "LastName", "Password", "Role" },
                values: new object[,]
                {
                    { 1, "ahmed.admin@gmail.com", "Ahmed", "Ali", "123456", "Admin" },
                    { 2, "mona.admin@gmail.com", "Mona", "Hassan", "123456", "Admin" },
                    { 3, "omar.admin@gmail.com", "Omar", "Mohamed", "123456", "Manager" },
                    { 4, "sara.admin@gmail.com", "Sara", "Ahmed", "123456", "Manager" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerID", "City", "Email", "FirstName", "LastName", "Phone", "Street", "ZipCode" },
                values: new object[,]
                {
                    { 1, "Damietta", "mazen@gmail.com", "Mazen", "Ahmed", "01000000001", "El Bahr Street", "34511" },
                    { 2, "Cairo", "mohamed@gmail.com", "Mohamed", "Ali", "01000000002", "Nile Street", "11511" },
                    { 3, "Damietta", "sara@gmail.com", "Sara", "Hassan", "01000000003", "Port Said Street", "34512" },
                    { 4, "Mansoura", "youssef@gmail.com", "Youssef", "Mahmoud", "01000000004", "El Geish Street", "35511" },
                    { 5, "Cairo", "nour@gmail.com", "Nour", "Khaled", "01000000005", "El Nasr Street", "11765" },
                    { 6, "Mansoura", "omar@gmail.com", "Omar", "Tarek", "01000000006", "University Street", "35516" },
                    { 7, "Damietta", "hana@gmail.com", "Hana", "Samir", "01000000007", "Corniche Street", "34513" },
                    { 8, "Cairo", "karim@gmail.com", "Karim", "Mostafa", "01000000008", "Tahrir Street", "11512" },
                    { 9, "Alexandria", "laila@gmail.com", "Laila", "Ayman", "01000000009", "Gardenia Street", "21500" },
                    { 10, "Alexandria", "adam@gmail.com", "Adam", "Hany", "01000000010", "Saad Zaghloul Street", "21501" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CustomerId", "OrderDate", "Status", "TotalAmount" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Completed", 300m },
                    { 2, 2, new DateTime(2026, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Preparing", 370m },
                    { 3, 3, new DateTime(2026, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Completed", 270m },
                    { 4, 4, new DateTime(2026, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", 400m },
                    { 5, 5, new DateTime(2026, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Completed", 280m },
                    { 6, 6, new DateTime(2026, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Preparing", 330m },
                    { 7, 7, new DateTime(2026, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Completed", 360m },
                    { 8, 8, new DateTime(2026, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cancelled", 220m },
                    { 9, 9, new DateTime(2026, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Completed", 450m },
                    { 10, 10, new DateTime(2026, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pending", 300m },
                    { 11, 1, new DateTime(2026, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Completed", 430m },
                    { 12, 3, new DateTime(2026, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Preparing", 320m }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "AdminId", "Category", "Description", "IsAvailable", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "Burgers", "Beef burger with cheese and fresh vegetables", true, "Classic Burger", 150m },
                    { 2, 1, "Burgers", "Crispy chicken burger with special sauce", true, "Chicken Burger", 140m },
                    { 3, 2, "Burgers", "Double beef burger with double cheese", true, "Double Beef Burger", 220m },
                    { 4, 2, "Pizza", "Pizza with tomato sauce and mozzarella cheese", true, "Margherita Pizza", 180m },
                    { 5, 2, "Pizza", "Pizza topped with chicken and vegetables", true, "Chicken Pizza", 230m },
                    { 6, 3, "Pizza", "Pizza with pepperoni and mozzarella cheese", true, "Pepperoni Pizza", 250m },
                    { 7, 3, "Pasta", "Creamy pasta with grilled chicken", true, "Chicken Pasta", 170m },
                    { 8, 3, "Pasta", "Pasta with creamy Alfredo sauce", true, "Alfredo Pasta", 160m },
                    { 9, 1, "Sandwiches", "Grilled chicken sandwich with vegetables", true, "Chicken Sandwich", 120m },
                    { 10, 1, "Sandwiches", "Crispy chicken with special sauce", true, "Crispy Chicken Sandwich", 130m },
                    { 11, 2, "Sides", "Crispy golden french fries", true, "French Fries", 60m },
                    { 12, 2, "Sides", "French fries topped with melted cheese", true, "Cheese Fries", 85m },
                    { 13, 3, "Meals", "Chicken, fries and soft drink", true, "Chicken Meal", 260m },
                    { 14, 3, "Meals", "Beef burger, fries and soft drink", true, "Beef Meal", 280m },
                    { 15, 4, "Drinks", "Cold soft drink", true, "Cola", 50m },
                    { 16, 4, "Drinks", "Fresh orange juice", true, "Orange Juice", 70m },
                    { 17, 4, "Drinks", "Fresh mango juice", true, "Mango Juice", 80m },
                    { 18, 4, "Desserts", "Chocolate cake with chocolate sauce", true, "Chocolate Cake", 100m },
                    { 19, 4, "Desserts", "Classic creamy cheesecake", true, "Cheesecake", 110m },
                    { 20, 4, "Desserts", "Three scoops of vanilla ice cream", true, "Ice Cream", 90m }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "OrderId", "ProductId", "Quantity", "SubTotal", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, 2, 300m, 150m },
                    { 2, 5, 1, 230m, 230m },
                    { 2, 11, 1, 60m, 60m },
                    { 2, 15, 1, 50m, 50m },
                    { 2, 20, 1, 30m, 30m },
                    { 3, 2, 1, 140m, 140m },
                    { 3, 11, 1, 60m, 60m },
                    { 3, 17, 1, 70m, 70m },
                    { 4, 6, 1, 250m, 250m },
                    { 4, 12, 1, 85m, 85m },
                    { 4, 15, 1, 50m, 50m },
                    { 4, 18, 1, 15m, 15m },
                    { 5, 14, 1, 280m, 280m },
                    { 6, 11, 1, 20m, 20m },
                    { 6, 13, 1, 260m, 260m },
                    { 6, 15, 1, 50m, 50m },
                    { 7, 3, 1, 220m, 220m },
                    { 7, 11, 1, 60m, 60m },
                    { 7, 17, 1, 80m, 80m },
                    { 8, 4, 1, 180m, 180m },
                    { 8, 15, 1, 40m, 40m },
                    { 9, 3, 1, 220m, 220m },
                    { 9, 7, 1, 170m, 170m },
                    { 9, 15, 1, 50m, 50m },
                    { 9, 18, 1, 10m, 10m },
                    { 10, 5, 1, 230m, 230m },
                    { 10, 11, 1, 60m, 60m },
                    { 10, 15, 1, 10m, 10m },
                    { 11, 3, 1, 220m, 220m },
                    { 11, 5, 1, 230m, 230m },
                    { 12, 4, 1, 180m, 180m },
                    { 12, 7, 1, 170m, 170m }
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

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
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
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
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
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
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
        }
    }
}
