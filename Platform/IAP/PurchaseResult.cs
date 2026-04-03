namespace RaiseTheHorse.Platform.IAP
{
    public class PurchaseResult
    {
        public bool IsSuccess { get; private set; }
        public string ProductId { get; private set; }
        public string TransactionId { get; private set; }
        public string Receipt { get; private set; }
        public string ErrorMessage { get; private set; }

        public static PurchaseResult Success(string productId, string transactionId, string receipt)
        {
            return new PurchaseResult
            {
                IsSuccess = true,
                ProductId = productId,
                TransactionId = transactionId,
                Receipt = receipt
            };
        }

        public static PurchaseResult Failure(string productId, string errorMessage)
        {
            return new PurchaseResult
            {
                IsSuccess = false,
                ProductId = productId,
                ErrorMessage = errorMessage
            };
        }
    }
}
