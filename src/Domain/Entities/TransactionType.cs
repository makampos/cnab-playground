namespace Domain.Entities
{
    public class TransactionType
    {
        public TransactionType(int id,string description, string nature, string signal)
        {
            Id = id;
            Description = description;
            Nature = nature;
            Signal = signal;
        }
        public int Id { get; private set; }
        public string Description { get; private set; }
        public string Nature { get; private set; }
        public string Signal { get; private set; }
        public virtual List<Transaction> Transactions { get;  private set; }
    }
}
