using Domain.Dtos;
using Domain.Entities;

namespace Domain.Intefaces.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> CreateTransactionAsync(string file);
        Task<ApiResponse<IEnumerable<GetTransactionDto>>> SearchAsync(string query, int transactionTypeId);
    }
}
