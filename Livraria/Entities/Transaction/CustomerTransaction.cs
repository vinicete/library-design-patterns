using Livraria.Data;

namespace Livraria.Entities.Transactions
{
    public class CustomerTransaction : TransactionTemplate
    {
        private readonly int _bookId;
        private readonly int _quantidade;
        private double _valorTotal;

        public CustomerTransaction(LivrariaContext context, int bookId, int quantidade)
            : base(context)
        {
            _bookId = bookId;
            _quantidade = quantidade;
        }

        protected override bool Validar()
        {
            var livro = _context.Books.Find(_bookId);
            return livro != null && livro.Quantity >= _quantidade;
        }

        protected override void CalcularValor()
        {
            var livro = _context.Books.Find(_bookId)!;
            _valorTotal = livro.Price * _quantidade;
        }

        protected override void AtualizarEstoque()
        {
            var livro = _context.Books.Find(_bookId)!;
            livro.Quantity -= _quantidade;
        }

        protected override void RegistrarHistorico()
        {
            _context.TransactionHistories.Add(new TransactionHistory
            {
                Type = "VENDA",
                BookId = _bookId,
                Quantity = _quantidade,
                TotalValue = _valorTotal
            });
        }
    }
}
