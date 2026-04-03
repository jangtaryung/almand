using System;
using UnityEngine;

namespace RaiseTheHorse.Platform.Firebase
{
    /// <summary>
    /// Firebase 초기화 및 공용 서비스 래퍼.
    /// 실제 SDK: com.google.firebase.app (Firebase Unity SDK)
    /// </summary>
    public class FirebaseManager : MonoBehaviour
    {
        public bool IsInitialized { get; private set; }
        public FirebaseAuthBridge AuthBridge { get; private set; }

        public event Action OnInitialized;

        private void Start()
        {
            AuthBridge = gameObject.AddComponent<FirebaseAuthBridge>();
            InitializeFirebase();
        }

        private void InitializeFirebase()
        {
            // TODO: Firebase SDK 연동
            // 실제 구현 시:
            // FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            // {
            //     if (task.Result == DependencyStatus.Available)
            //     {
            //         IsInitialized = true;
            //         OnInitialized?.Invoke();
            //         Debug.Log("[FirebaseManager] Firebase initialized");
            //     }
            //     else
            //     {
            //         Debug.LogError($"[FirebaseManager] Could not resolve dependencies: {task.Result}");
            //     }
            // });

            // Mock: 에디터에서는 바로 초기화 완료 처리
            IsInitialized = true;
            Debug.Log("[FirebaseManager] Firebase initialized (mock)");
            OnInitialized?.Invoke();
        }
    }
}
