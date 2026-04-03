using System;
using System.Collections.Generic;
using UnityEngine;

namespace RaiseTheHorse.Platform.IAP
{
    /// <summary>
    /// Apple App Store IAP 구현 (StoreKit).
    /// Unity IAP(com.unity.purchasing)를 통해 연동한다.
    /// </summary>
    public class AppleIAPProvider : MonoBehaviour, IIAPProvider
    {
        private readonly Dictionary<string, ProductInfo> _products = new Dictionary<string, ProductInfo>();

        public bool IsInitialized { get; private set; }

        public void InitializeStore(string[] productIds, Action<bool> onComplete)
        {
#if UNITY_IOS && !UNITY_EDITOR
            try
            {
                // TODO: Apple StoreKit / Unity IAP 연동
                // 실제 구현 시:
                // 1) ConfigurationBuilder.Instance(StandardPurchasingModule.Instance(AppStore.AppleAppStore))
                // 2) productIds를 builder에 등록
                // 3) UnityPurchasing.Initialize(this, builder)
                // 4) OnInitialized 콜백에서 _products 딕셔너리 채우기

                Debug.Log("[AppleIAPProvider] Initialize requested (SDK 연동 필요)");
                onComplete?.Invoke(false);
            }
            catch (Exception e)
            {
                Debug.LogError($"[AppleIAPProvider] Init error: {e.Message}");
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
#if UNITY_IOS && !UNITY_EDITOR
            // TODO: Apple IAP 구매 요청
            // 1) controller.InitiatePurchaseFlow(product)
            // 2) ProcessPurchase 콜백에서 receipt 추출
            // 3) 서버 영수증 검증 (App Store Server API v2) 후 결과 반환

            Debug.Log($"[AppleIAPProvider] Purchase requested: {productId}");
            onComplete?.Invoke(PurchaseResult.Failure(productId, "Apple IAP SDK not yet integrated"));
#else
            onComplete?.Invoke(PurchaseResult.Failure(productId, "Apple IAP is only available on iOS"));
#endif
        }

        public void RestorePurchases(Action<List<PurchaseResult>> onComplete)
        {
#if UNITY_IOS && !UNITY_EDITOR
            // TODO: Apple 구매 복원
            // Apple 심사 가이드라인: 비소모품은 반드시 '구매 복원' 버튼 필요
            // IAppleExtensions.RestoreTransactions(result => { ... })

            Debug.Log("[AppleIAPProvider] RestorePurchases requested");
            onComplete?.Invoke(new List<PurchaseResult>());
#else
            onComplete?.Invoke(new List<PurchaseResult>());
#endif
        }
    }
}
