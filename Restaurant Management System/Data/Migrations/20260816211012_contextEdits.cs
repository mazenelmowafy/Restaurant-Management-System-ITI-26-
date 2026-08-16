using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Restaurant_Management_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class contextEdits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 2, 11 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 2, 15 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 2, 20 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 3, 11 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 3, 17 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 4, 6 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 4, 12 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 4, 15 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 4, 18 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 5, 14 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 6, 11 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 6, 13 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 6, 15 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 7, 3 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 7, 11 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 7, 17 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 8, 4 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 8, 15 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 9, 3 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 9, 7 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 9, 15 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 9, 18 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 10, 5 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 10, 11 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 10, 15 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 11, 3 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 11, 5 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 12, 4 });

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 12, 7 });

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 10);

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Admins");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "ImageFileName",
                value: "classic_burger.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "ImageFileName",
                value: "chicken_burger.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "AdminId", "ImageFileName" },
                values: new object[] { 1, "double_beef_burger.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                columns: new[] { "AdminId", "ImageFileName" },
                values: new object[] { 1, "margherita_pizza.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                columns: new[] { "AdminId", "ImageFileName" },
                values: new object[] { 1, "chicken_pizza.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                columns: new[] { "AdminId", "ImageFileName" },
                values: new object[] { 1, "pepperoni_pizza.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7,
                columns: new[] { "AdminId", "ImageFileName" },
                values: new object[] { 1, "chicken_pasta.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8,
                columns: new[] { "AdminId", "ImageFileName" },
                values: new object[] { 1, "alfredo_pasta.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9,
                column: "ImageFileName",
                value: "chicken_sandwich.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10,
                column: "ImageFileName",
                value: "crispy_chicken_sandwich.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11,
                columns: new[] { "AdminId", "ImageFileName" },
                values: new object[] { 1, "french_fries.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12,
                columns: new[] { "AdminId", "ImageFileName" },
                values: new object[] { 1, "cheese_fries.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 13,
                columns: new[] { "AdminId", "ImageFileName" },
                values: new object[] { 1, "chicken_meal.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14,
                columns: new[] { "AdminId", "ImageFileName" },
                values: new object[] { 1, "beef_meal.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 15,
                columns: new[] { "AdminId", "ImageFileName" },
                values: new object[] { 1, "cola.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 16,
                columns: new[] { "AdminId", "ImageFileName" },
                values: new object[] { 1, "orange_juice.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 17,
                columns: new[] { "AdminId", "Description", "ImageFileName", "Name" },
                values: new object[] { 1, "Fresh strawberry juice", "strawberry_juice.jpg", "Strawberry Juice" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 18,
                columns: new[] { "AdminId", "ImageFileName" },
                values: new object[] { 1, "chocolate_cake.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 19,
                columns: new[] { "AdminId", "ImageFileName" },
                values: new object[] { 1, "cheesecake.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 20,
                columns: new[] { "AdminId", "ImageFileName" },
                values: new object[] { 1, "ice_cream.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminId",
                keyValue: 1,
                column: "Role",
                value: "Admin");

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "AdminId", "Email", "FirstName", "LastName", "Password", "Role" },
                values: new object[,]
                {
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

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "ImageFileName",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "ImageFileName",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "AdminId", "ImageFileName" },
                values: new object[] { 2, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                columns: new[] { "AdminId", "ImageFileName" },
                values: new object[] { 2, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                columns: new[] { "AdminId", "ImageFileName" },
                values: new object[] { 2, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                columns: new[] { "AdminId", "ImageFileName" },
                values: new object[] { 3, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7,
                columns: new[] { "AdminId", "ImageFileName" },
                values: new object[] { 3, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8,
                columns: new[] { "AdminId", "ImageFileName" },
                values: new object[] { 3, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9,
                column: "ImageFileName",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10,
                column: "ImageFileName",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11,
                columns: new[] { "AdminId", "ImageFileName" },
                values: new object[] { 2, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12,
                columns: new[] { "AdminId", "ImageFileName" },
                values: new object[] { 2, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 13,
                columns: new[] { "AdminId", "ImageFileName" },
                values: new object[] { 3, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14,
                columns: new[] { "AdminId", "ImageFileName" },
                values: new object[] { 3, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 15,
                columns: new[] { "AdminId", "ImageFileName" },
                values: new object[] { 4, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 16,
                columns: new[] { "AdminId", "ImageFileName" },
                values: new object[] { 4, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 17,
                columns: new[] { "AdminId", "Description", "ImageFileName", "Name" },
                values: new object[] { 4, "Fresh mango juice", null, "Mango Juice" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 18,
                columns: new[] { "AdminId", "ImageFileName" },
                values: new object[] { 4, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 19,
                columns: new[] { "AdminId", "ImageFileName" },
                values: new object[] { 4, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 20,
                columns: new[] { "AdminId", "ImageFileName" },
                values: new object[] { 4, null });

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
        }
    }
}
