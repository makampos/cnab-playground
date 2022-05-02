using Api.ViewModels;
using AutoMapper;
using Domain;
using Domain.Dtos;
using Domain.Entities;
using System.Text.Json.Serialization;

namespace Api.Configurations
{
    public class AutoMapperConfiguration: Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Transaction, CreateTransactionDto>().ReverseMap();

            CreateMap<Transaction, GetTransactionDto>();
            CreateMap<ApiResponse<IEnumerable<Transaction>>, ApiResponse<IEnumerable<GetTransactionDto>>>().ReverseMap();
            CreateMap<TransactionType, GetTransactionTypeDto>()
                .ReverseMap()
                .ForPath(x => x.Transactions, opt => opt.Ignore());
        }
    }
}
