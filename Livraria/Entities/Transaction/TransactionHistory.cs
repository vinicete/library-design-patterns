namespace Livraria.Entities
{
    public class TransactionHistory
    {
        public int Id { get; set; }
        public string Type { get; set; } = ""; // "COMPRA" ou "VENDA"
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public double TotalValue { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}