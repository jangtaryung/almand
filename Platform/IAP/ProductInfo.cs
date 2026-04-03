namespace RaiseTheHorse.Platform.IAP
{
    /// <summary>
    /// 스토어에서 조회한 상품 정보.
    /// </summary>
    public class ProductInfo
    {
        public string ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string CurrencyCode { get; set; }
        /// <summary>소수점 없는 마이크로 단위 가격 (비교/정렬용)</summary>
        public long PriceMicros { get; set; }
    }
}
