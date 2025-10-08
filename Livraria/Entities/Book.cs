using Livraria.Contracts;
using Livraria.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Livraria.Entities
{
    public class Book : ISubject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }  
        public double Price { get; set; }
        public double Weight { get; set; }
        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (_quantity == 0 && value > 0)
                {
                    _quantity = value;
                    Console.WriteLine($"\n--- O livro '{Title}' voltou ao estoque! Notificando clientes... ---\n");
                }
                else
                {
                    _quantity = value;
                }
            }
        }

        [JsonIgnore]
        public ICollection<BookSubscription> Subscriptions { get; set; } = new List<BookSubscription>();

        public void Notify()
        {
            Console.WriteLine($"Notificando clientes sobre o livro '{Title}'...");
        }

        public void Notify(LivrariaContext context)
        {
            var subscribedCustomers = context.BookSubscriptions
                .Where(bs => bs.BookId == this.Id)
                .Include(bs => bs.Customer)
                .Select(bs => bs.Customer)
                .ToList();

            Console.WriteLine($"Notificando {subscribedCustomers.Count} clientes sobre o livro '{Title}'...");

            foreach (var customer in subscribedCustomers)
            {
                customer.Update(this);
            }
        }

        public void Subscribe(IObserver observer)
        {
        }

        public void Unsubscribe(IObserver observer)
        {
        }
    }
}