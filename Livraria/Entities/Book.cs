using Livraria.Contracts;

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
                    Notify();
                }
                else
                {
                    _quantity = value;
                }
            }
        }

        private List<IObserver> customers;

        public Book()
        {
            customers = new List<IObserver>();
        }
        public void Notify()
        {

            foreach (var customer in customers)
            {
                customer.Update(this);
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
