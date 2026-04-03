using UnityEngine;
using RaiseTheHorse.Platform.Auth;
using RaiseTheHorse.Platform.IAP;
using RaiseTheHorse.Platform.Firebase;

namespace RaiseTheHorse.Platform
{
    /// <summary>
    /// 플랫폼별 서비스를 자동 감지하여 등록하는 중앙 서비스 로케이터.
    /// 씬에 하나만 배치한다 (DontDestroyOnLoad).
    /// </summary>
    public class PlatformServiceLocator : MonoBehaviour
    {
        [SerializeField] private PlatformType platformOverride = PlatformType.Editor;
        [SerializeField] private bool useOverride;

        public static PlatformServiceLocator Instance { get; private set; }
        public PlatformType CurrentPlatform { get; private set; }
        public AuthManager Auth { get; private set; }
        public IAPManager IAP { get; private set; }
        public FirebaseManager Firebase { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            CurrentPlatform = DetectPlatform();
            Debug.Log($"[PlatformServiceLocator] Platform detected: {CurrentPlatform}");

            InitializeServices();
        }

        private PlatformType DetectPlatform()
        {
            if (useOverride)
                return platformOverride;

#if UNITY_EDITOR
            return PlatformType.Editor;
#elif UNITY_IOS
            return PlatformType.AppleAppStore;
#elif UNITY_ANDROID
            // ONE Store 빌드인지 Google Play 빌드인지 scripting define으로 구분
            #if ONE_STORE
                return PlatformType.OneStore;
            #else
                return PlatformType.GooglePlay;
            #endif
#else
            return PlatformType.Editor;
#endif
        }

        private void InitializeServices()
        {
            // Firebase (공통)
            Firebase = gameObject.AddComponent<FirebaseManager>();

            // Auth
            IAuthProvider authProvider = CreateAuthProvider();
            Auth = gameObject.AddComponent<AuthManager>();
            Auth.Initialize(authProvider, Firebase);

            // IAP
            IIAPProvider iapProvider = CreateIAPProvider();
            IAP = gameObject.AddComponent<IAPManager>();
            IAP.Initialize(iapProvider);
        }

        private IAuthProvider CreateAuthProvider()
        {
            switch (CurrentPlatform)
            {
                case PlatformType.GooglePlay:
                    return gameObject.AddComponent<GoogleAuthProvider>();
                case PlatformType.AppleAppStore:
                    return gameObject.AddComponent<AppleAuthProvider>();
                case PlatformType.OneStore:
                    return gameObject.AddComponent<OneStoreAuthProvider>();
                default:
                    return gameObject.AddComponent<EditorAuthProvider>();
            }
        }

        private IIAPProvider CreateIAPProvider()
        {
            switch (CurrentPlatform)
            {
                case PlatformType.GooglePlay:
                    return gameObject.AddComponent<GoogleIAPProvider>();
                case PlatformType.AppleAppStore:
                    return gameObject.AddComponent<AppleIAPProvider>();
                case PlatformType.OneStore:
                    return gameObject.AddComponent<OneStoreIAPProvider>();
                default:
                    return gameObject.AddComponent<EditorIAPProvider>();
            }
        }
    }
}
