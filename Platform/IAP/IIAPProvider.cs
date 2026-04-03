using System;
using System.Collections.Generic;

namespace RaiseTheHorse.Platform.IAP
{
    /// <summary>
    /// 플랫폼별 인앱 결제 제공자 인터페이스.
    /// Google/Apple/OneStore 각각 이 인터페이스를 구현한다.
    /// </summary>
    public interface IIAPProvider
    {
        /// <summary>스토어 초기화 완료 여부</summary>
        bool IsInitialized { get; }

        /// <summary>
        /// 스토어 연결 및 상품 목록 조회.
        /// productIds: 조회할 상품 ID 배열.
        /// </summary>
        void InitializeStore(string[] productIds, Action<bool> onComplete);

        /// <summary>상품 정보 조회 (InitializeStore 이후)</summary>
        ProductInfo GetProductInfo(string productId);

        /// <summary>구매 요청</summary>
        void Purchase(string productId, Action<PurchaseResult> onComplete);

        /// <summary>미처리(pending) 구매 복원/확인</summary>
        void RestorePurchases(Action<List<PurchaseResult>> onComplete);
    }
}
