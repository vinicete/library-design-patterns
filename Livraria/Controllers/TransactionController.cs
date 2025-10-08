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
            var transacao = new SupplierTransaction(_context, bookId, quantidade);
            transacao.Execute();
            return Ok("Compra do fornecedor registrada com sucesso!");
        }

        [HttpGet("history")]
        public IActionResult GetHistory()
        {
            return Ok(_context.TransactionHistories.ToList());
        }
    }
}
