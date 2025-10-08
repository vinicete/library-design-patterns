using Livraria.Entities;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Data
{
    public class LivrariaContext : DbContext
    {
        public LivrariaContext(DbContextOptions<LivrariaContext> options) : base(options) { }

        public DbSet<Book> Books => Set<Book>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Supplier> Suppliers => Set<Supplier>();
        public DbSet<Bookstore> Bookstores => Set<Bookstore>();
        public DbSet<TransactionHistory> TransactionHistories => Set<TransactionHistory>();
        public DbSet<BookSubscription> BookSubscriptions => Set<BookSubscription>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Clean Code", Author = "Robert C. Martin", Price = 99.90, Weight = 0.8, Quantity = 10 },
                new Book { Id = 2, Title = "The Pragmatic Programmer", Author = "Andrew Hunt", Price = 120.00, Weight = 0.7, Quantity = 5 },
                new Book { Id = 3, Title = "Design Patterns", Author = "Erich Gamma", Price = 150.00, Weight = 1.2, Quantity = 3 }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "Alice", Email = "alice@email.com" },
                new Customer { Id = 2, Name = "Bob", Email = "bob@email.com" }
            );

            modelBuilder.Entity<Supplier>().HasData(
                new Supplier { Id = 1, Name = "TechBooks Distribuidora", Contact = "contato@techbooks.com" },
                new Supplier { Id = 2, Name = "Editora Global", Contact = "vendas@editoraglobal.com" }
            );

            modelBuilder.Entity<Bookstore>().HasData(
                new Bookstore { Id = 1, Name = "Future Books" }
            );
        }
    }
}
