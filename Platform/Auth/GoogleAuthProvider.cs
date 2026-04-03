using System;
using UnityEngine;

namespace RaiseTheHorse.Platform.Auth
{
    /// <summary>
    /// Google Play Games Sign-In 구현.
    /// 실제 SDK: com.google.play.games (Google Play Games plugin for Unity)
    /// </summary>
    public class GoogleAuthProvider : MonoBehaviour, IAuthProvider
    {
        private string _idToken;

        public bool IsSignedIn { get; private set; }

        public void SignIn(Action<AuthResult> onComplete)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            try
            {
                // TODO: Google Play Games SDK 연동
                // PlayGamesPlatform.Instance.Authenticate(status => { ... });
                //
                // 실제 구현 시:
                // 1) PlayGamesPlatform.Activate()
                // 2) PlayGamesPlatform.Instance.Authenticate(status => ...)
                // 3) PlayGamesPlatform.Instance.RequestServerSideAccess(false, token => ...)
                //    → _idToken = token (Firebase credential용)

                Debug.Log("[GoogleAuthProvider] Google Sign-In requested (SDK 연동 필요)");
                onComplete?.Invoke(AuthResult.Failure("Google Play Games SDK not yet integrated"));
            }
            catch (Exception e)
            {
                Debug.LogError($"[GoogleAuthProvider] SignIn error: {e.Message}");
                onComplete?.Invoke(AuthResult.Failure(e.Message));
            }
#else
            onComplete?.Invoke(AuthResult.Failure("Google Sign-In is only available on Android"));
#endif
        }

        public void SignOut()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            // PlayGamesPlatform.Instance.SignOut();
#endif
            IsSignedIn = false;
            _idToken = null;
            Debug.Log("[GoogleAuthProvider] Signed out");
        }

        public string GetCredentialToken()
        {
            return _idToken;
        }
    }
}
