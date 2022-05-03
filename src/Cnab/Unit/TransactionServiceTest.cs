using Api.Configurations;
using Api.ViewModels;
using AutoMapper;
using Domain;
using Domain.Dtos;
using Domain.Entities;
using Domain.Intefaces.Repository;
using Domain.Services;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Cnab.Test.Unit
{
    public class TransactionServiceTest 
    {
        private readonly Mock<ITransactionRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        public TransactionServiceTest()
        {
           
            _mockRepository = new Mock<ITransactionRepository>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task SearchAsyncShouldReturnTransactions()
        {
            // Arrange
            var transactions = new List<Transaction>
            {
                new Transaction( 1,"20/10/1992", 180.66M,
                "79813615087", "4753****3153",TimeSpan.MinValue, "JOÃO MACEDO", "BAR DO JOÃO"),
            };

            transactions.ForEach(x => x.SetTransactionType(new TransactionType(1, "Débito", "Entrada", "+")));

            _mockRepository.Setup(x => x.SearchAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(await Task.FromResult(transactions));

            var mapper = CreateMapperConfiguration();
            var service = new TransactionService(_mockRepository.Object, mapper);

            // Act
            var result = service.SearchAsync("BAR DO JOÃO", 1);
            // Assert
            _mockRepository.Verify(x => x.SearchAsync(It.IsAny<string>(), It.IsAny<int>()), Times.Once());
            result.Result.Data.Should().BeAssignableTo<IEnumerable<GetTransactionDto>>();
            result.Result.Data.Should().HaveCountGreaterThanOrEqualTo(1);
            result.Result.Data.Should().OnlyContain(x => x.TransactionType != null);
            result.Result.TotalAmount.Should().BePositive();
        }

        [Fact]
        public async Task SearchAsyncShouldReturnEmpty()
        {
            // Arrange
            _mockRepository.Setup(x => x.SearchAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(await Task.FromResult(new List<Transaction>()));

            var mapper = CreateMapperConfiguration();
            var service = new TransactionService(_mockRepository.Object, mapper);

            // Act
            var result = service.SearchAsync("BAR DO JOÃO", 1);

            // Assert
            _mockRepository.Verify(x => x.SearchAsync(It.IsAny<string>(), It.IsAny<int>()), Times.Once());
            result.Result.Data.Should().BeEmpty();
            result.Result.TotalAmount.Should().Be(0).And.BeOfType(typeof(decimal));
        }


        [Fact]
        public void CreateTransactionAsyncShouldSave()
        {
            // Arrange
            var transactionService = new TransactionService(_mockRepository.Object, _mockMapper.Object) ;

            // Act
            var result =  transactionService.CreateTransactionAsync(Helper.GetContent());

            // Assert
            _mockRepository.Verify(x => x.AddRangeAsync(It.IsAny<IEnumerable<Transaction>>()), Times.Once);
            _mockRepository.Verify(x => x.SaveChangesAsync(), Times.Once());
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IEnumerable<Transaction>>>();
        }


        [Theory]
        [InlineData(14200)]
        [InlineData(11240)]
        [InlineData(552)]
        [InlineData(9860)]
        [InlineData(188960)]
        public void NormalizeAmountShouldReturnDecimalNormalized(decimal value)
        {
            // Arrange
            var transactionService = new TransactionService(_mockRepository.Object, _mockMapper.Object);

            // Act
            var result = transactionService.NormalizeAmount(value);

            // Assert
            result.Should().Be(value / 100).And.BeOfType(typeof(decimal));
        }

        [Theory]
        [MemberData(nameof(FactoryTransaction))]
        public void CalculateTotalAmountShouldReturnDecimalTotalAmount(List<Transaction> transactions)
        {
            // Arrange
            var transactionService = new TransactionService(_mockRepository.Object, _mockMapper.Object);

            // Act
            var result = transactionService.CalculateTotalAmount(transactions);

            // Assert
            result.Should().Be(8030.80M).And.BeOfType(typeof(decimal));
        }

        [Fact]        
        public void CalculateTotalAmountShouldReturn0()
        {
            // Arrange
            var transactionService = new TransactionService(_mockRepository.Object, _mockMapper.Object);
            var transactions = new List<Transaction>();

            // Act
            var result = transactionService.CalculateTotalAmount(transactions);

            // Assert
            result.Should().Be(0).And.BeOfType(typeof(decimal));
        }

        [Fact]
        public void ExtractDataShouldParseStringAsAFileAndReturnObject()
        {
            // Arrange
            var transactionService = new TransactionService(_mockRepository.Object, _mockMapper.Object);
            
            // Act
            var result = transactionService.ExtractData(Helper.GetContent());

            // Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(21);
            result.Should().BeAssignableTo<IEnumerable<CreateTransactionDto>>();
        }

        #region Helpers 
        private static IEnumerable<object> FactoryTransaction() 
        {
            var transactions = new List<Transaction>
            {
                new Transaction( 1,"20/10/1992", 180.66M,
                "79813615087", "4753****3153",TimeSpan.MinValue, "JOÃO MACEDO", "BAR DO JOÃO"),
                new Transaction(1, "11/09/2004", 7850.14M,
                "79813615087", "4753****3153", TimeSpan.MaxValue, "JOÃO MACEDO", "BAR DO JOÃO")
            };

            yield return new object[] {transactions};
        }

        private static IMapper CreateMapperConfiguration()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperConfiguration>());
            return configuration.CreateMapper();
        }

        #endregion


    }
}
