using Api.ViewModels;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Intefaces.Repository;
using Domain.Intefaces.Services;

namespace Domain.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Transaction>> CreateTransactionAsync(string file)
        {
            var transactions = _mapper.Map<IEnumerable<Transaction>>(ExtractData(file));            
            await _transactionRepository.AddRangeAsync(transactions);
            await _transactionRepository.SaveChangesAsync();
            return transactions;
        }

        public async Task<ApiResponse<IEnumerable<GetTransactionDto>>> SearchAsync(string query, int transactionTypeId)
        {
            var response =  await _transactionRepository.SearchAsync(query.Trim(), transactionTypeId);
            return ApiResponse<IEnumerable<GetTransactionDto>>.Success(
                _mapper.Map<IEnumerable<GetTransactionDto>>(response), CalculateTotalAmount(response));
        }

        public IEnumerable<CreateTransactionDto> ExtractData(string file)
        {
            var dto = file.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
             .Select(x => new CreateTransactionDto(
                     Shared.ParseExtension.ParseInt(x.Split('=')[0].Substring(0, 1)), //TransactionTypeId
                     Shared.ParseExtension.ParseDate(x.Split('=')[0].Substring(1, 8)), // Date
                     NormalizeAmount(Shared.ParseExtension.ParseToDecimal(x.Split('=')[0].Substring(9, 10))), // Amount
                     x.Split('=')[0].Substring(19, 11), //CPF
                     x.Split('=')[0].Substring(30, 12), // CardNumber
                     Shared.ParseExtension.ParseHour(x.Split('=')[0].Substring(42, 6)), //Hour
                     x.Split('=')[0].Substring(48, 14).Trim(), //StoreOwner
                     x.Split('=')[0].Substring(62).Trim())); // StoreName
            return dto;
        }

        public decimal NormalizeAmount(decimal amount) => amount / 100;

        public decimal CalculateTotalAmount(IEnumerable<Transaction> data) => data.Sum(x => x.Amount);
    }
}
