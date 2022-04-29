using Domain.Entities;

namespace Domain
{
    public class Transaction
    {
        public Transaction() { }
        public Transaction(int transactionTypeId, string date,
            decimal amount, string cpf, string cardNumber, TimeSpan hour,string storeOwner,string storeName)
        => (TransactionTypeId, Date, Amount, CPF, CardNumber, Hour, StoreOwner, StoreName)
        = (transactionTypeId, date, amount, cpf, cardNumber, hour, storeOwner, storeName);
        public int Id { get; private set; }
        public int TransactionTypeId { get;  set; }
        public string Date { get; private set; }
        public decimal Amount { get; private set; }
        public string CPF { get; private set; }
        public string CardNumber { get; private set; }
        public TimeSpan Hour { get; private set; }
        public string StoreOwner { get; private set; }
        public string StoreName { get; private set; }
        public virtual TransactionType TransactionType { get;  set; }

        /// <summary>
        /// Only For Testing Purporse
        /// </summary>
        /// <param name="transactionType"></param>
        public void SetTransactionType(TransactionType transactionType)
            => TransactionType = transactionType;   
    }
}