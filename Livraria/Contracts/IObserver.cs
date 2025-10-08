namespace Livraria.Contracts
{
    public interface IObserver
    {
        public void Update(ISubject subject);
    }
}
