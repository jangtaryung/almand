using System;
using UnityEngine;

namespace RaiseTheHorse.Platform.Auth
{
    /// <summary>
    /// ONE Store 로그인 구현.
    /// 실제 SDK: ONE Store SDK v21+ (com.onestorecorp.sdk.unity)
    /// ONE Store는 자체 계정 체계가 있으며, OAuth 토큰을 발급받아 Firebase custom token과 연동.
    /// </summary>
    public class OneStoreAuthProvider : MonoBehaviour, IAuthProvider
    {
        private string _accessToken;

        public bool IsSignedIn { get; private set; }

        public void SignIn(Action<AuthResult> onComplete)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                // TODO: ONE Store SDK 연동
                // 실제 구현 시:
                // 1) OneStoreAuthClient.launchSignInFlow()
                // 2) 콜백에서 accessToken 수령
                //    → _accessToken = token
                // 3) 서버에서 accessToken 검증 후 Firebase custom token 발급
                //    → AuthResult.Success(...) 콜백

                Debug.Log("[OneStoreAuthProvider] ONE Store Sign-In requested (SDK 연동 필요)");
                onComplete?.Invoke(AuthResult.Failure("ONE Store SDK not yet integrated"));
            }
            catch (Exception e)
            {
                Debug.LogError($"[OneStoreAuthProvider] SignIn error: {e.Message}");
                onComplete?.Invoke(AuthResult.Failure(e.Message));
            }
#else
            onComplete?.Invoke(AuthResult.Failure("ONE Store Sign-In is only available on Android"));
#endif
        }

        public void SignOut()
        {
            IsSignedIn = false;
            _accessToken = null;
            Debug.Log("[OneStoreAuthProvider] Signed out");
        }

        /// <summary>
        /// ONE Store는 Firebase 직접 credential이 없으므로,
        /// 서버에서 custom token을 발급받아야 한다.
        /// 이 메서드는 서버 검증용 access token을 반환한다.
        /// </summary>
        public string GetCredentialToken()
        {
            return _accessToken;
        }
    }
}
