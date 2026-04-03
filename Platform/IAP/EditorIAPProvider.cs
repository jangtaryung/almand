using System;
using System.Collections.Generic;
using UnityEngine;

namespace RaiseTheHorse.Platform.IAP
{
    /// <summary>
    /// Unity Editor용 Mock IAP 제공자.
    /// 실제 결제 없이 구매 플로우를 테스트할 수 있다.
    /// </summary>
    public class EditorIAPProvider : MonoBehaviour, IIAPProvider
    {
        private readonly Dictionary<string, ProductInfo> _products = new Dictionary<string, ProductInfo>();
        private int _mockTransactionCounter;

        public bool IsInitialized { get; private set; }

        public void InitializeStore(string[] productIds, Action<bool> onComplete)
        {
            _products.Clear();
            foreach (string id in productIds)
            {
                _products[id] = new ProductInfo
                {
                    ProductId = id,
                    Title = $"[Mock] {id}",
                    Description = $"Mock product {id}",
                    Price = "\\1,000",
                    CurrencyCode = "KRW",
                    PriceMicros = 1000000000L
                };
            }

            IsInitialized = true;
            Debug.Log($"[EditorIAPProvider] Mock store initialized with {productIds.Length} products");
            onComplete?.Invoke(true);
        }

        public ProductInfo GetProductInfo(string productId)
        {
            _products.TryGetValue(productId, out var info);
            return info;
        }

        public void Purchase(string productId, Action<PurchaseResult> onComplete)
        {
            _mockTransactionCounter++;
            string txId = $"mock_tx_{_mockTransactionCounter}";

            Debug.Log($"[EditorIAPProvider] Mock purchase success: {productId} (tx: {txId})");
            onComplete?.Invoke(PurchaseResult.Success(productId, txId, "mock_receipt_data"));
        }

        public void RestorePurchases(Action<List<PurchaseResult>> onComplete)
        {
            Debug.Log("[EditorIAPProvider] Mock restore — no purchases to restore");
            onComplete?.Invoke(new List<PurchaseResult>());
        }
    }
}
