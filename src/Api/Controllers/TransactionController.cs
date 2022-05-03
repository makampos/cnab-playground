using Domain;
using Domain.Dtos;
using Domain.Entities;
using Domain.Intefaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<Transaction>>> CreateTransactionAsync(IFormFile file)
        {
            if(!Utils.Helper.IsTxtExtension(file))
                throw new Exception("Invalid Extension");

            var textFile = await Utils.Helper.ReadFormFileAsync(file);

            var response = await _transactionService.CreateTransactionAsync(textFile);

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ApiResponse<IEnumerable<GetTransactionDto>>>> GetTransactionAsync(
            [FromQuery] string query, [FromQuery] int transactionTypeId)
        {
            var response = await _transactionService.SearchAsync(query, transactionTypeId);
            return Ok(response);
        }
    }
}
