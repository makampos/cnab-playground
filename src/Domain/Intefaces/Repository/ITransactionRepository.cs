namespace Domain.Intefaces.Repository
{
    public interface ITransactionRepository : IBaseRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> SearchAsync(string query, int transactionTypeId);
    }
}
