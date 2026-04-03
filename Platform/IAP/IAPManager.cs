using System;
using System.Collections.Generic;
using UnityEngine;

namespace RaiseTheHorse.Platform.IAP
{
    /// <summary>
    /// 결제 Facade. 플랫폼에 관계없이 동일한 API로 결제를 처리한다.
    /// </summary>
    public class IAPManager : MonoBehaviour
    {
        private IIAPProvider _provider;

        public bool IsInitialized => _provider != null && _provider.IsInitialized;

        public event Action<PurchaseResult> OnPurchaseComplete;

        public void Initialize(IIAPProvider provider)
        {
            _provider = provider;
        }

        /// <summary>
        /// 스토어 초기화. 게임 시작 시 한 번 호출한다.
        /// </summary>
        public void InitializeStore(string[] productIds, Action<bool> onComplete = null)
        {
            if (_provider == null)
            {
                Debug.LogError("[IAPManager] Provider not initialized");
                onComplete?.Invoke(false);
                return;
            }

            _provider.InitializeStore(productIds, success =>
            {
                if (success)
                    Debug.Log("[IAPManager] Store initialized successfully");
                else
                    Debug.LogWarning("[IAPManager] Store initialization failed");

                onComplete?.Invoke(success);
            });
        }

        public ProductInfo GetProductInfo(string productId)
        {
            return _provider?.GetProductInfo(productId);
        }

        public void Purchase(string productId, Action<PurchaseResult> onComplete = null)
        {
            if (_provider == null || !_provider.IsInitialized)
            {
                var fail = PurchaseResult.Failure(productId, "Store not initialized");
                onComplete?.Invoke(fail);
                OnPurchaseComplete?.Invoke(fail);
                return;
            }

            _provider.Purchase(productId, result =>
            {
                onComplete?.Invoke(result);
                OnPurchaseComplete?.Invoke(result);
            });
        }

        public void RestorePurchases(Action<List<PurchaseResult>> onComplete = null)
        {
            if (_provider == null || !_provider.IsInitialized)
            {
                Debug.LogWarning("[IAPManager] Cannot restore — store not initialized");
                onComplete?.Invoke(new List<PurchaseResult>());
                return;
            }

            _provider.RestorePurchases(onComplete);
        }
    }
}
