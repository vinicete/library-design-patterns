using Livraria.Contracts;
using System.Text.Json.Serialization;

namespace Livraria.Entities
{
    public class Customer : IObserver
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public ICollection<BookSubscription> Subscriptions { get; set; } = new List<BookSubscription>();

        public void Update(ISubject subject)
        {
            if (subject is Book book)
            {
                Console.WriteLine($"Olá {Name}! O livro '{book.Title}' por {book.Author} está disponível por R${book.Price}.");
            }

        }
    }
}
