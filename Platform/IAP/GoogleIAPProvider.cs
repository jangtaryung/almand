using System;
using System.Collections.Generic;
using UnityEngine;

namespace RaiseTheHorse.Platform.IAP
{
    /// <summary>
    /// Google Play Billing 구현.
    /// Unity IAP(com.unity.purchasing)를 사용하거나,
    /// Google Play Billing Library(com.android.billingclient)를 직접 래핑할 수 있다.
    /// </summary>
    public class GoogleIAPProvider : MonoBehaviour, IIAPProvider
    {
        private readonly Dictionary<string, ProductInfo> _products = new Dictionary<string, ProductInfo>();

        public bool IsInitialized { get; private set; }

        public void InitializeStore(string[] productIds, Action<bool> onComplete)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                // TODO: Google Play Billing 연동
                // 실제 구현 시 (Unity IAP 사용 예):
                // 1) ConfigurationBuilder.Instance(StandardPurchasingModule.Instance(AppStore.GooglePlay))
                // 2) productIds를 builder에 등록
                // 3) UnityPurchasing.Initialize(this, builder)
                // 4) OnInitialized 콜백에서 _products 딕셔너리 채우기

                Debug.Log("[GoogleIAPProvider] Initialize requested (SDK 연동 필요)");
                onComplete?.Invoke(false);
            }
            catch (Exception e)
            {
                Debug.LogError($"[GoogleIAPProvider] Init error: {e.Message}");
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
            // TODO: Google Play Billing 구매 요청
            // 1) controller.InitiatePurchaseFlow(product)
            // 2) ProcessPurchase 콜백에서 receipt, transactionId 추출
            // 3) 서버 영수증 검증 후 PurchaseResult 반환

            Debug.Log($"[GoogleIAPProvider] Purchase requested: {productId}");
            onComplete?.Invoke(PurchaseResult.Failure(productId, "Google IAP SDK not yet integrated"));
#else
            onComplete?.Invoke(PurchaseResult.Failure(productId, "Google IAP is only available on Android"));
#endif
        }

        public void RestorePurchases(Action<List<PurchaseResult>> onComplete)
        {
            // Google Play는 자동으로 미소비 구매를 복원하므로
            // InitializeStore 시점에 처리된다.
            Debug.Log("[GoogleIAPProvider] RestorePurchases — handled at initialization");
            onComplete?.Invoke(new List<PurchaseResult>());
        }
    }
}
