using Livraria.Contracts;

namespace Livraria.Entities
{
    public class Customer : IObserver
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public void Update(ISubject subject)
        {
            if (subject is Book book)
            {
                Console.WriteLine($"Olá {Name}! O livro '{book.Title}' por {book.Author} está disponível por R${book.Price}.");
            }

        }
    }
}
