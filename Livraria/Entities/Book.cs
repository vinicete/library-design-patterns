using Livraria.Contracts;

namespace Livraria.Entities
{
    public class Book : ISubject
    {
        public string Title { get; set; }
        public string Author { get; set; }  
        public double Price { get; set; }
        public double Weight { get; set; }
        public int Quantity { get; set; }

        private List<IObserver> customers;

        public Book()
        {
            customers = new List<IObserver>();
        }
        public void Notify()
        {

            foreach (var customer in customers)
            {
                customer.Update("Seu livro "+Title+" chegou");
            }
        }

        public void Subscribe(IObserver observer)
        {
            customers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            customers.Remove(observer);
        }
    }
}
