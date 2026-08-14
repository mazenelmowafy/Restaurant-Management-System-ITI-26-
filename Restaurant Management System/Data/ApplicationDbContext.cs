using RestaurantManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace RestaurantManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\MSSQLLocalDB;Database=RestaurantManagementSystem;Trusted_Connection=True;TrustServerCertificate=True;"
            );
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<OrderItem>()
                .HasKey(x => new { x.OrderId, x.ProductId });

            modelBuilder.Entity<Order>()
                .HasOne(x => x.Customer)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.CustomerId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(x => x.Order)
                .WithMany(x => x.OrderItems)
                .HasForeignKey(x => x.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId);

            modelBuilder.Entity<Product>()
                .HasOne(x => x.Admin)
                .WithMany()
                .HasForeignKey(x => x.AdminId);
            modelBuilder.Entity<Product>()
    .HasOne(x => x.Admin)
    .WithMany()
    .HasForeignKey(x => x.AdminId);


            // ==================== Seed Data ====================

            // Admins
            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    AdminId = 1,
                    FirstName = "Ahmed",
                    LastName = "Ali",
                    Email = "ahmed.admin@gmail.com",
                    Password = "123456",
                    Role = "Admin"
                },
                new Admin
                {
                    AdminId = 2,
                    FirstName = "Mona",
                    LastName = "Hassan",
                    Email = "mona.admin@gmail.com",
                    Password = "123456",
                    Role = "Admin"
                },
                new Admin
                {
                    AdminId = 3,
                    FirstName = "Omar",
                    LastName = "Mohamed",
                    Email = "omar.admin@gmail.com",
                    Password = "123456",
                    Role = "Manager"
                },
                new Admin
                {
                    AdminId = 4,
                    FirstName = "Sara",
                    LastName = "Ahmed",
                    Email = "sara.admin@gmail.com",
                    Password = "123456",
                    Role = "Manager"
                }
            );


            // Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    CustomerID = 1,
                    FirstName = "Mazen",
                    LastName = "Ahmed",
                    Email = "mazen@gmail.com",
                    Phone = "01000000001",
                    Street = "El Bahr Street",
                    City = "Damietta",
                    ZipCode = "34511"
                },
                new Customer
                {
                    CustomerID = 2,
                    FirstName = "Mohamed",
                    LastName = "Ali",
                    Email = "mohamed@gmail.com",
                    Phone = "01000000002",
                    Street = "Nile Street",
                    City = "Cairo",
                    ZipCode = "11511"
                },
                new Customer
                {
                    CustomerID = 3,
                    FirstName = "Sara",
                    LastName = "Hassan",
                    Email = "sara@gmail.com",
                    Phone = "01000000003",
                    Street = "Port Said Street",
                    City = "Damietta",
                    ZipCode = "34512"
                },
                new Customer
                {
                    CustomerID = 4,
                    FirstName = "Youssef",
                    LastName = "Mahmoud",
                    Email = "youssef@gmail.com",
                    Phone = "01000000004",
                    Street = "El Geish Street",
                    City = "Mansoura",
                    ZipCode = "35511"
                },
                new Customer
                {
                    CustomerID = 5,
                    FirstName = "Nour",
                    LastName = "Khaled",
                    Email = "nour@gmail.com",
                    Phone = "01000000005",
                    Street = "El Nasr Street",
                    City = "Cairo",
                    ZipCode = "11765"
                },
                new Customer
                {
                    CustomerID = 6,
                    FirstName = "Omar",
                    LastName = "Tarek",
                    Email = "omar@gmail.com",
                    Phone = "01000000006",
                    Street = "University Street",
                    City = "Mansoura",
                    ZipCode = "35516"
                },
                new Customer
                {
                    CustomerID = 7,
                    FirstName = "Hana",
                    LastName = "Samir",
                    Email = "hana@gmail.com",
                    Phone = "01000000007",
                    Street = "Corniche Street",
                    City = "Damietta",
                    ZipCode = "34513"
                },
                new Customer
                {
                    CustomerID = 8,
                    FirstName = "Karim",
                    LastName = "Mostafa",
                    Email = "karim@gmail.com",
                    Phone = "01000000008",
                    Street = "Tahrir Street",
                    City = "Cairo",
                    ZipCode = "11512"
                },
                new Customer
                {
                    CustomerID = 9,
                    FirstName = "Laila",
                    LastName = "Ayman",
                    Email = "laila@gmail.com",
                    Phone = "01000000009",
                    Street = "Gardenia Street",
                    City = "Alexandria",
                    ZipCode = "21500"
                },
                new Customer
                {
                    CustomerID = 10,
                    FirstName = "Adam",
                    LastName = "Hany",
                    Email = "adam@gmail.com",
                    Phone = "01000000010",
                    Street = "Saad Zaghloul Street",
                    City = "Alexandria",
                    ZipCode = "21501"
                }
            );


            // Products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    Name = "Classic Burger",
                    Description = "Beef burger with cheese and fresh vegetables",
                    Price = 150,
                    Category = "Burgers",
                    IsAvailable = true,
                    AdminId = 1
                },
                new Product
                {
                    ProductId = 2,
                    Name = "Chicken Burger",
                    Description = "Crispy chicken burger with special sauce",
                    Price = 140,
                    Category = "Burgers",
                    IsAvailable = true,
                    AdminId = 1
                },
                new Product
                {
                    ProductId = 3,
                    Name = "Double Beef Burger",
                    Description = "Double beef burger with double cheese",
                    Price = 220,
                    Category = "Burgers",
                    IsAvailable = true,
                    AdminId = 2
                },
                new Product
                {
                    ProductId = 4,
                    Name = "Margherita Pizza",
                    Description = "Pizza with tomato sauce and mozzarella cheese",
                    Price = 180,
                    Category = "Pizza",
                    IsAvailable = true,
                    AdminId = 2
                },
                new Product
                {
                    ProductId = 5,
                    Name = "Chicken Pizza",
                    Description = "Pizza topped with chicken and vegetables",
                    Price = 230,
                    Category = "Pizza",
                    IsAvailable = true,
                    AdminId = 2
                },
                new Product
                {
                    ProductId = 6,
                    Name = "Pepperoni Pizza",
                    Description = "Pizza with pepperoni and mozzarella cheese",
                    Price = 250,
                    Category = "Pizza",
                    IsAvailable = true,
                    AdminId = 3
                },
                new Product
                {
                    ProductId = 7,
                    Name = "Chicken Pasta",
                    Description = "Creamy pasta with grilled chicken",
                    Price = 170,
                    Category = "Pasta",
                    IsAvailable = true,
                    AdminId = 3
                },
                new Product
                {
                    ProductId = 8,
                    Name = "Alfredo Pasta",
                    Description = "Pasta with creamy Alfredo sauce",
                    Price = 160,
                    Category = "Pasta",
                    IsAvailable = true,
                    AdminId = 3
                },
                new Product
                {
                    ProductId = 9,
                    Name = "Chicken Sandwich",
                    Description = "Grilled chicken sandwich with vegetables",
                    Price = 120,
                    Category = "Sandwiches",
                    IsAvailable = true,
                    AdminId = 1
                },
                new Product
                {
                    ProductId = 10,
                    Name = "Crispy Chicken Sandwich",
                    Description = "Crispy chicken with special sauce",
                    Price = 130,
                    Category = "Sandwiches",
                    IsAvailable = true,
                    AdminId = 1
                },
                new Product
                {
                    ProductId = 11,
                    Name = "French Fries",
                    Description = "Crispy golden french fries",
                    Price = 60,
                    Category = "Sides",
                    IsAvailable = true,
                    AdminId = 2
                },
                new Product
                {
                    ProductId = 12,
                    Name = "Cheese Fries",
                    Description = "French fries topped with melted cheese",
                    Price = 85,
                    Category = "Sides",
                    IsAvailable = true,
                    AdminId = 2
                },
                new Product
                {
                    ProductId = 13,
                    Name = "Chicken Meal",
                    Description = "Chicken, fries and soft drink",
                    Price = 260,
                    Category = "Meals",
                    IsAvailable = true,
                    AdminId = 3
                },
                new Product
                {
                    ProductId = 14,
                    Name = "Beef Meal",
                    Description = "Beef burger, fries and soft drink",
                    Price = 280,
                    Category = "Meals",
                    IsAvailable = true,
                    AdminId = 3
                },
                new Product
                {
                    ProductId = 15,
                    Name = "Cola",
                    Description = "Cold soft drink",
                    Price = 50,
                    Category = "Drinks",
                    IsAvailable = true,
                    AdminId = 4
                },
                new Product
                {
                    ProductId = 16,
                    Name = "Orange Juice",
                    Description = "Fresh orange juice",
                    Price = 70,
                    Category = "Drinks",
                    IsAvailable = true,
                    AdminId = 4
                },
                new Product
                {
                    ProductId = 17,
                    Name = "Mango Juice",
                    Description = "Fresh mango juice",
                    Price = 80,
                    Category = "Drinks",
                    IsAvailable = true,
                    AdminId = 4
                },
                new Product
                {
                    ProductId = 18,
                    Name = "Chocolate Cake",
                    Description = "Chocolate cake with chocolate sauce",
                    Price = 100,
                    Category = "Desserts",
                    IsAvailable = true,
                    AdminId = 4
                },
                new Product
                {
                    ProductId = 19,
                    Name = "Cheesecake",
                    Description = "Classic creamy cheesecake",
                    Price = 110,
                    Category = "Desserts",
                    IsAvailable = true,
                    AdminId = 4
                },
                new Product
                {
                    ProductId = 20,
                    Name = "Ice Cream",
                    Description = "Three scoops of vanilla ice cream",
                    Price = 90,
                    Category = "Desserts",
                    IsAvailable = true,
                    AdminId = 4
                }
            );


            // Orders
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    OrderId = 1,
                    OrderDate = new DateTime(2026, 8, 1),
                    Status = "Completed",
                    TotalAmount = 300,
                    CustomerId = 1
                },
                new Order
                {
                    OrderId = 2,
                    OrderDate = new DateTime(2026, 8, 2),
                    Status = "Preparing",
                    TotalAmount = 370,
                    CustomerId = 2
                },
                new Order
                {
                    OrderId = 3,
                    OrderDate = new DateTime(2026, 8, 3),
                    Status = "Completed",
                    TotalAmount = 270,
                    CustomerId = 3
                },
                new Order
                {
                    OrderId = 4,
                    OrderDate = new DateTime(2026, 8, 4),
                    Status = "Pending",
                    TotalAmount = 400,
                    CustomerId = 4
                },
                new Order
                {
                    OrderId = 5,
                    OrderDate = new DateTime(2026, 8, 5),
                    Status = "Completed",
                    TotalAmount = 280,
                    CustomerId = 5
                },
                new Order
                {
                    OrderId = 6,
                    OrderDate = new DateTime(2026, 8, 6),
                    Status = "Preparing",
                    TotalAmount = 330,
                    CustomerId = 6
                },
                new Order
                {
                    OrderId = 7,
                    OrderDate = new DateTime(2026, 8, 7),
                    Status = "Completed",
                    TotalAmount = 360,
                    CustomerId = 7
                },
                new Order
                {
                    OrderId = 8,
                    OrderDate = new DateTime(2026, 8, 8),
                    Status = "Cancelled",
                    TotalAmount = 220,
                    CustomerId = 8
                },
                new Order
                {
                    OrderId = 9,
                    OrderDate = new DateTime(2026, 8, 9),
                    Status = "Completed",
                    TotalAmount = 450,
                    CustomerId = 9
                },
                new Order
                {
                    OrderId = 10,
                    OrderDate = new DateTime(2026, 8, 10),
                    Status = "Pending",
                    TotalAmount = 300,
                    CustomerId = 10
                },
                new Order
                {
                    OrderId = 11,
                    OrderDate = new DateTime(2026, 8, 11),
                    Status = "Completed",
                    TotalAmount = 430,
                    CustomerId = 1
                },
                new Order
                {
                    OrderId = 12,
                    OrderDate = new DateTime(2026, 8, 12),
                    Status = "Preparing",
                    TotalAmount = 320,
                    CustomerId = 3
                }
            );


            // Order Items
            modelBuilder.Entity<OrderItem>().HasData(
                // Order 1 = 300
                new OrderItem
                {
                    OrderId = 1,
                    ProductId = 1,
                    Quantity = 2,
                    UnitPrice = 150,
                    SubTotal = 300
                },

                // Order 2 = 370
                new OrderItem
                {
                    OrderId = 2,
                    ProductId = 5,
                    Quantity = 1,
                    UnitPrice = 230,
                    SubTotal = 230
                },
                new OrderItem
                {
                    OrderId = 2,
                    ProductId = 11,
                    Quantity = 1,
                    UnitPrice = 60,
                    SubTotal = 60
                },
                new OrderItem
                {
                    OrderId = 2,
                    ProductId = 15,
                    Quantity = 1,
                    UnitPrice = 50,
                    SubTotal = 50
                },
                new OrderItem
                {
                    OrderId = 2,
                    ProductId = 20,
                    Quantity = 1,
                    UnitPrice = 30,
                    SubTotal = 30
                },

                // Order 3 = 270
                new OrderItem
                {
                    OrderId = 3,
                    ProductId = 2,
                    Quantity = 1,
                    UnitPrice = 140,
                    SubTotal = 140
                },
                new OrderItem
                {
                    OrderId = 3,
                    ProductId = 11,
                    Quantity = 1,
                    UnitPrice = 60,
                    SubTotal = 60
                },
                new OrderItem
                {
                    OrderId = 3,
                    ProductId = 17,
                    Quantity = 1,
                    UnitPrice = 70,
                    SubTotal = 70
                },

                // Order 4 = 400
                new OrderItem
                {
                    OrderId = 4,
                    ProductId = 6,
                    Quantity = 1,
                    UnitPrice = 250,
                    SubTotal = 250
                },
                new OrderItem
                {
                    OrderId = 4,
                    ProductId = 12,
                    Quantity = 1,
                    UnitPrice = 85,
                    SubTotal = 85
                },
                new OrderItem
                {
                    OrderId = 4,
                    ProductId = 15,
                    Quantity = 1,
                    UnitPrice = 50,
                    SubTotal = 50
                },
                new OrderItem
                {
                    OrderId = 4,
                    ProductId = 18,
                    Quantity = 1,
                    UnitPrice = 15,
                    SubTotal = 15
                },

                // Order 5 = 280
                new OrderItem
                {
                    OrderId = 5,
                    ProductId = 14,
                    Quantity = 1,
                    UnitPrice = 280,
                    SubTotal = 280
                },

                // Order 6 = 330
                new OrderItem
                {
                    OrderId = 6,
                    ProductId = 13,
                    Quantity = 1,
                    UnitPrice = 260,
                    SubTotal = 260
                },
                new OrderItem
                {
                    OrderId = 6,
                    ProductId = 15,
                    Quantity = 1,
                    UnitPrice = 50,
                    SubTotal = 50
                },
                new OrderItem
                {
                    OrderId = 6,
                    ProductId = 11,
                    Quantity = 1,
                    UnitPrice = 20,
                    SubTotal = 20
                },

                // Order 7 = 360
                new OrderItem
                {
                    OrderId = 7,
                    ProductId = 3,
                    Quantity = 1,
                    UnitPrice = 220,
                    SubTotal = 220
                },
                new OrderItem
                {
                    OrderId = 7,
                    ProductId = 11,
                    Quantity = 1,
                    UnitPrice = 60,
                    SubTotal = 60
                },
                new OrderItem
                {
                    OrderId = 7,
                    ProductId = 17,
                    Quantity = 1,
                    UnitPrice = 80,
                    SubTotal = 80
                },

                // Order 8 = 220
                new OrderItem
                {
                    OrderId = 8,
                    ProductId = 4,
                    Quantity = 1,
                    UnitPrice = 180,
                    SubTotal = 180
                },
                new OrderItem
                {
                    OrderId = 8,
                    ProductId = 15,
                    Quantity = 1,
                    UnitPrice = 40,
                    SubTotal = 40
                },

                // Order 9 = 450
                new OrderItem
                {
                    OrderId = 9,
                    ProductId = 3,
                    Quantity = 1,
                    UnitPrice = 220,
                    SubTotal = 220
                },
                new OrderItem
                {
                    OrderId = 9,
                    ProductId = 7,
                    Quantity = 1,
                    UnitPrice = 170,
                    SubTotal = 170
                },
                new OrderItem
                {
                    OrderId = 9,
                    ProductId = 15,
                    Quantity = 1,
                    UnitPrice = 50,
                    SubTotal = 50
                },
                new OrderItem
                {
                    OrderId = 9,
                    ProductId = 18,
                    Quantity = 1,
                    UnitPrice = 10,
                    SubTotal = 10
                },

                // Order 10 = 300
                new OrderItem
                {
                    OrderId = 10,
                    ProductId = 5,
                    Quantity = 1,
                    UnitPrice = 230,
                    SubTotal = 230
                },
                new OrderItem
                {
                    OrderId = 10,
                    ProductId = 11,
                    Quantity = 1,
                    UnitPrice = 60,
                    SubTotal = 60
                },
                new OrderItem
                {
                    OrderId = 10,
                    ProductId = 15,
                    Quantity = 1,
                    UnitPrice = 10,
                    SubTotal = 10
                },

                // Order 11 = 430
                new OrderItem
                {
                    OrderId = 11,
                    ProductId = 3,
                    Quantity = 1,
                    UnitPrice = 220,
                    SubTotal = 220
                },
                new OrderItem
                {
                    OrderId = 11,
                    ProductId = 5,
                    Quantity = 1,
                    UnitPrice = 230,
                    SubTotal = 230
                },

                // Order 12 = 320
                new OrderItem
                {
                    OrderId = 12,
                    ProductId = 7,
                    Quantity = 1,
                    UnitPrice = 170,
                    SubTotal = 170
                },
                new OrderItem
                {
                    OrderId = 12,
                    ProductId = 4,
                    Quantity = 1,
                    UnitPrice = 180,
                    SubTotal = 180
                }
            );
        }
    }
}
