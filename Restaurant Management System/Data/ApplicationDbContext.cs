using RestaurantManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace RestaurantManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
  
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.UnitPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.SubTotal)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Product>()
                .HasOne(x => x.Admin)
                .WithMany()
                .HasForeignKey(x => x.AdminId);

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


            // ==================== Seed Data ====================

            // Admins
            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    AdminId = 1,
                    FirstName = "Ahmed",
                    LastName = "Ali",
                    Email = "ahmed.admin@gmail.com",
                    PasswordHash = "AQAAAAIAAYagAAAAEK1cw2qTNWR41LfygMwWJRHTuaeElKrQLaEdV8AIf+aj2B8SSJ2RxUPXIoR9pZIPaA==",
                    
                });
                 
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
                    AdminId = 1,
                    ImageFileName = "classic_burger.png"
                },
                new Product
                {
                    ProductId = 2,
                    Name = "Chicken Burger",
                    Description = "Crispy chicken burger with special sauce",
                    Price = 140,
                    Category = "Burgers",
                    IsAvailable = true,
                    AdminId = 1,
                    ImageFileName = "chicken_burger.png"
                },
                new Product
                {
                    ProductId = 3,
                    Name = "Double Beef Burger",
                    Description = "Double beef burger with double cheese",
                    Price = 220,
                    Category = "Burgers",
                    IsAvailable = true,
                    AdminId = 1,
                    ImageFileName = "double_beef_burger.png"
                },
                new Product
                {
                    ProductId = 4,
                    Name = "Margherita Pizza",
                    Description = "Pizza with tomato sauce and mozzarella cheese",
                    Price = 180,
                    Category = "Pizza",
                    IsAvailable = true,
                    AdminId = 1,
                    ImageFileName = "margherita_pizza.png"
                },
                new Product
                {
                    ProductId = 5,
                    Name = "Chicken Pizza",
                    Description = "Pizza topped with chicken and vegetables",
                    Price = 230,
                    Category = "Pizza",
                    IsAvailable = true,
                    AdminId = 1,
                    ImageFileName = "chicken_pizza.png"
                },
                new Product
                {
                    ProductId = 6,
                    Name = "Pepperoni Pizza",
                    Description = "Pizza with pepperoni and mozzarella cheese",
                    Price = 250,
                    Category = "Pizza",
                    IsAvailable = true,
                    AdminId = 1,
                    ImageFileName = "pepperoni_pizza.png"
                },
                new Product
                {
                    ProductId = 7,
                    Name = "Chicken Pasta",
                    Description = "Creamy pasta with grilled chicken",
                    Price = 170,
                    Category = "Pasta",
                    IsAvailable = true,
                    AdminId = 1,
                    ImageFileName = "chicken_pasta.png"
                },
                new Product
                {
                    ProductId = 8,
                    Name = "Alfredo Pasta",
                    Description = "Pasta with creamy Alfredo sauce",
                    Price = 160,
                    Category = "Pasta",
                    IsAvailable = true,
                    AdminId = 1,
                    ImageFileName = "alfredo_pasta.png"
                },
                new Product
                {
                    ProductId = 9,
                    Name = "Chicken Sandwich",
                    Description = "Grilled chicken sandwich with vegetables",
                    Price = 120,
                    Category = "Sandwiches",
                    IsAvailable = true,
                    AdminId = 1,
                    ImageFileName = "chicken_sandwich.png"
                },
                new Product
                {
                    ProductId = 10,
                    Name = "Crispy Chicken Sandwich",
                    Description = "Crispy chicken with special sauce",
                    Price = 130,
                    Category = "Sandwiches",
                    IsAvailable = true,
                    AdminId = 1,
                    ImageFileName = "crispy_chicken_sandwich.png"
                },
                new Product
                {
                    ProductId = 11,
                    Name = "French Fries",
                    Description = "Crispy golden french fries",
                    Price = 60,
                    Category = "Sides",
                    IsAvailable = true,
                    AdminId = 1,
                    ImageFileName = "french_fries.png"
                },
                new Product
                {
                    ProductId = 12,
                    Name = "Cheese Fries",
                    Description = "French fries topped with melted cheese",
                    Price = 85,
                    Category = "Sides",
                    IsAvailable = true,
                    AdminId = 1,
                    ImageFileName = "cheese_fries.png"
                },
                new Product
                {
                    ProductId = 13,
                    Name = "Chicken Meal",
                    Description = "Chicken, fries and soft drink",
                    Price = 260,
                    Category = "Meals",
                    IsAvailable = true,
                    AdminId = 1,
                    ImageFileName = "chicken_meal.png"
                },
                new Product
                {
                    ProductId = 14,
                    Name = "Beef Meal",
                    Description = "Beef burger, fries and soft drink",
                    Price = 280,
                    Category = "Meals",
                    IsAvailable = true,
                    AdminId = 1,
                    ImageFileName = "beef_meal.png"
                },
                new Product
                {
                    ProductId = 15,
                    Name = "Cola",
                    Description = "Cold soft drink",
                    Price = 50,
                    Category = "Drinks",
                    IsAvailable = true,
                    AdminId = 1,
                    ImageFileName = "cola.png"
                },
                new Product
                {
                    ProductId = 16,
                    Name = "Orange Juice",
                    Description = "Fresh orange juice",
                    Price = 70,
                    Category = "Drinks",
                    IsAvailable = true,
                    AdminId = 1,
                    ImageFileName = "orange_juice.png"
                },
                new Product
                {
                    ProductId = 17,
                    Name = "Strawberry Juice",
                    Description = "Fresh strawberry juice",
                    Price = 80,
                    Category = "Drinks",
                    IsAvailable = true,
                    AdminId = 1,
                    ImageFileName = "strawberry_juice.png"
                },
                new Product
                {
                    ProductId = 18,
                    Name = "Chocolate Cake",
                    Description = "Chocolate cake with chocolate sauce",
                    Price = 100,
                    Category = "Desserts",
                    IsAvailable = true,
                    AdminId = 1,
                    ImageFileName = "chocolate_cake.png"
                },
                new Product
                {
                    ProductId = 19,
                    Name = "Cheesecake",
                    Description = "Classic creamy cheesecake",
                    Price = 110,
                    Category = "Desserts",
                    IsAvailable = true,
                    AdminId = 1,
                    ImageFileName = "cheesecake.png"
                },
                new Product
                {
                    ProductId = 20,
                    Name = "Ice Cream",
                    Description = "Three scoops of vanilla ice cream",
                    Price = 90,
                    Category = "Desserts",
                    IsAvailable = true,
                    AdminId = 1,
                    ImageFileName = "ice_cream.png"
                }
            );                  
                    
        }
    }
}