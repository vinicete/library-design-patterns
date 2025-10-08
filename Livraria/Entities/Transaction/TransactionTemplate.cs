using Livraria.Contracts;
using Livraria.Data;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Entities.Transactions
{
    public abstract class TransactionTemplate : ITransaction
    {
        protected readonly LivrariaContext _context;

        protected TransactionTemplate(LivrariaContext context)
        {
            _context = context;
        }

        public void Execute()
        {
            if (!Validar())
                throw new InvalidOperationException("Transação inválida.");

            CalcularValor();
            AtualizarEstoque();
            RegistrarHistorico();

            _context.SaveChanges();
        }

        protected abstract bool Validar();
        protected abstract void CalcularValor();
        protected abstract void AtualizarEstoque();
        protected abstract void RegistrarHistorico();
    }
}
