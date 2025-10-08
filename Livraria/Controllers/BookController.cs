using Livraria.Data;
using Livraria.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly LivrariaContext _context;
        public BooksController(LivrariaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _context.Books.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Book book)
        {
            if (id != book.Id) return BadRequest();
            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("{bookId}/subscribe/{customerId}")]
        public async Task<IActionResult> Subscribe(int bookId, int customerId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null) return NotFound("Livro não encontrado.");

            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null) return NotFound("Cliente não encontrado.");

            var existingSubscription = await _context.BookSubscriptions
                .FirstOrDefaultAsync(bs => bs.BookId == bookId && bs.CustomerId == customerId);

            if (existingSubscription != null)
            {
                return BadRequest($"Cliente {customer.Name} já está inscrito para receber notificações do livro '{book.Title}'.");
            }
            var subscription = new BookSubscription
            {
                BookId = bookId,
                CustomerId = customerId,
                SubscribedAt = DateTime.Now
            };

            _context.BookSubscriptions.Add(subscription);
            await _context.SaveChangesAsync();

            return Ok($"Cliente {customer.Name} inscrito para receber notificações do livro '{book.Title}'.");
        }

        [HttpPost("{bookId}/unsubscribe/{customerId}")]
        public async Task<IActionResult> Unsubscribe(int bookId, int customerId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null) return NotFound("Livro não encontrado.");

            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null) return NotFound("Cliente não encontrado.");

            var subscription = await _context.BookSubscriptions
                .FirstOrDefaultAsync(bs => bs.BookId == bookId && bs.CustomerId == customerId);

            if (subscription == null)
            {
                return BadRequest($"Cliente {customer.Name} não está inscrito para receber notificações do livro '{book.Title}'.");
            }

            _context.BookSubscriptions.Remove(subscription);
            await _context.SaveChangesAsync();

            return Ok($"Cliente {customer.Name} removido das notificações do livro '{book.Title}'.");
        }

        [HttpPost("{bookId}/test-notification")]
        public async Task<IActionResult> TestNotification(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null) return NotFound("Livro não encontrado.");

            var originalQuantity = book.Quantity;
            book.Quantity = 0;
            await _context.SaveChangesAsync();
            
            book.Quantity = originalQuantity;
            await _context.SaveChangesAsync();
            
            book.Notify(_context);

            return Ok($"Notificação de teste enviada para os inscritos no livro '{book.Title}'.");
        }
    }
}
