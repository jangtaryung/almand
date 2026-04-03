using System;
using System.Collections.Generic;
using UnityEngine;

namespace RaiseTheHorse.Platform.IAP
{
    /// <summary>
    /// ONE Store 인앱 결제 구현.
    /// 실제 SDK: ONE Store SDK v21+ (com.onestorecorp.sdk.unity.iap)
    /// </summary>
    public class OneStoreIAPProvider : MonoBehaviour, IIAPProvider
    {
        private readonly Dictionary<string, ProductInfo> _products = new Dictionary<string, ProductInfo>();

        public bool IsInitialized { get; private set; }

        public void InitializeStore(string[] productIds, Action<bool> onComplete)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                // TODO: ONE Store IAP SDK 연동
                // 실제 구현 시:
                // 1) PurchaseClient.Initialize(base64PublicKey)
                // 2) PurchaseClient.QueryProductDetails(productIds, ProductType.INAPP, callback)
                // 3) 콜백에서 _products 딕셔너리 채우기

                Debug.Log("[OneStoreIAPProvider] Initialize requested (SDK 연동 필요)");
                onComplete?.Invoke(false);
            }
            catch (Exception e)
            {
                Debug.LogError($"[OneStoreIAPProvider] Init error: {e.Message}");
                onComplete?.Invoke(false);
            }
#else
            onComplete?.Invoke(false);
#endif
        }

        public ProductInfo GetProductInfo(string productId)
        {
            _products.TryGetValue(productId, out var info);
            return info;
        }

        public void Purchase(string productId, Action<PurchaseResult> onComplete)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            // TODO: ONE Store 구매 요청
            // 1) PurchaseClient.Purchase(productId, ProductType.INAPP, developerPayload)
            // 2) 콜백에서 purchaseData, signature 추출
            // 3) 서버 영수증 검증 후 PurchaseResult 반환
            // 4) 소비: PurchaseClient.Consume(purchaseData, callback)

            Debug.Log($"[OneStoreIAPProvider] Purchase requested: {productId}");
            onComplete?.Invoke(PurchaseResult.Failure(productId, "ONE Store IAP SDK not yet integrated"));
#else
            onComplete?.Invoke(PurchaseResult.Failure(productId, "ONE Store IAP is only available on Android"));
#endif
        }

        public void RestorePurchases(Action<List<PurchaseResult>> onComplete)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            // TODO: ONE Store 미소비 구매 조회
            // PurchaseClient.QueryPurchases(ProductType.INAPP, callback)

            Debug.Log("[OneStoreIAPProvider] RestorePurchases requested");
            onComplete?.Invoke(new List<PurchaseResult>());
#else
            onComplete?.Invoke(new List<PurchaseResult>());
#endif
        }
    }
}
