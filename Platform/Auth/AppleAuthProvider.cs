using System;
using UnityEngine;

namespace RaiseTheHorse.Platform.Auth
{
    /// <summary>
    /// Apple Sign In 구현.
    /// 실제 SDK: com.lupidan.apple-signin-unity (Sign in with Apple Unity Plugin)
    /// </summary>
    public class AppleAuthProvider : MonoBehaviour, IAuthProvider
    {
        private string _idToken;

        public bool IsSignedIn { get; private set; }

        public void SignIn(Action<AuthResult> onComplete)
        {
#if UNITY_IOS && !UNITY_EDITOR
            try
            {
                // TODO: Apple Sign In SDK 연동
                // 실제 구현 시:
                // 1) IAppleAuthManager 생성
                // 2) AppleAuthLoginArgs로 로그인 요청 (Email, FullName scope)
                // 3) ICredential에서 identityToken 추출 → UTF8 디코딩
                //    → _idToken = decodedToken (Firebase credential용)
                // 4) AuthResult.Success(userId, fullName, email, idToken) 콜백

                Debug.Log("[AppleAuthProvider] Apple Sign-In requested (SDK 연동 필요)");
                onComplete?.Invoke(AuthResult.Failure("Apple Sign-In SDK not yet integrated"));
            }
            catch (Exception e)
            {
                Debug.LogError($"[AppleAuthProvider] SignIn error: {e.Message}");
                onComplete?.Invoke(AuthResult.Failure(e.Message));
            }
#else
            onComplete?.Invoke(AuthResult.Failure("Apple Sign-In is only available on iOS"));
#endif
        }

        public void SignOut()
        {
            IsSignedIn = false;
            _idToken = null;
            Debug.Log("[AppleAuthProvider] Signed out");
        }

        public string GetCredentialToken()
        {
            return _idToken;
        }
    }
}
