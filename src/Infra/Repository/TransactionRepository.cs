using Domain;
using Domain.Intefaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository
{
    public class TransacoRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        private readonly AppDbContext _appDbContext;
        public TransacoRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public async Task<IEnumerable<Transaction>> SearchAsync(string query, int transactionTypeId)
        {
            var response = await _appDbContext
               .Transactions
               .Include(s => s.TransactionType)
               .Where(s => s.TransactionTypeId == transactionTypeId && EF.Functions.Like(s.StoreName, $"%{query}%"))
               .AsNoTracking()
               .ToListAsync();

            return response;
        }
    }
}
