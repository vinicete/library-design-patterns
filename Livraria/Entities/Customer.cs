using Livraria.Contracts;

namespace Livraria.Entities
{
    public class Customer : IObserver
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public void Update(string message)
        {
            Console.WriteLine(Name + ", " + message);
            
        }
    }
}
