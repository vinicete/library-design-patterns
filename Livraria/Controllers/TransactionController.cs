using Livraria.Data;
using Livraria.Entities.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly LivrariaContext _context;

        public TransactionController(LivrariaContext context)
        {
            _context = context;
        }

        [HttpPost("customer")]
        public IActionResult BuyFromCustomer(int bookId, int quantidade)
        {
            var transacao = new CustomerTransaction(_context, bookId, quantidade);
            transacao.Execute();
            return Ok("Compra do cliente realizada com sucesso!");
        }

        [HttpPost("supplier")]
        public IActionResult BuyFromSupplier(int bookId, int quantidade)
        {
            // Get the book before transaction to check if it was out of stock
            var book = _context.Books.Find(bookId);
            bool wasOutOfStock = book?.Quantity == 0;
            
            var transacao = new SupplierTransaction(_context, bookId, quantidade);
            transacao.Execute();
            
            // If book was out of stock and now has inventory, notify subscribers
            if (wasOutOfStock && book != null && book.Quantity > 0)
            {
                book.Notify(_context);
            }
            
            return Ok("Compra do fornecedor registrada com sucesso!");
        }

        [HttpGet("history")]
        public IActionResult GetHistory()
        {
            return Ok(_context.TransactionHistories.ToList());
        }
    }
}
