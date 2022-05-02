namespace Domain.Entities
{
    public class ApiResponse<T>
    {
        public T Data { get; private set; }
        public decimal TotalAmount { get; private set; }

        public static ApiResponse<T> Success(T data, decimal totalAmount)
        {
            return new ApiResponse<T> { Data = data, TotalAmount = totalAmount };
        }

    }
}
