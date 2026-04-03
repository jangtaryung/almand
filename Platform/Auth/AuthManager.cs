using System;
using UnityEngine;
using RaiseTheHorse.Platform.Firebase;

namespace RaiseTheHorse.Platform.Auth
{
    /// <summary>
    /// 인증 Facade. 플랫폼 로그인 → Firebase Auth 연동까지 한 번에 처리.
    /// </summary>
    public class AuthManager : MonoBehaviour
    {
        private IAuthProvider _provider;
        private FirebaseManager _firebase;

        public bool IsSignedIn => _provider != null && _provider.IsSignedIn;

        public event Action<AuthResult> OnSignInComplete;
        public event Action OnSignOutComplete;

        public void Initialize(IAuthProvider provider, FirebaseManager firebase)
        {
            _provider = provider;
            _firebase = firebase;
        }

        /// <summary>
        /// 플랫폼 로그인 후 Firebase Auth에 credential을 연동한다.
        /// </summary>
        public void SignIn(Action<AuthResult> onComplete = null)
        {
            if (_provider == null)
            {
                var fail = AuthResult.Failure("AuthProvider not initialized");
                onComplete?.Invoke(fail);
                OnSignInComplete?.Invoke(fail);
                return;
            }

            _provider.SignIn(platformResult =>
            {
                if (!platformResult.IsSuccess)
                {
                    onComplete?.Invoke(platformResult);
                    OnSignInComplete?.Invoke(platformResult);
                    return;
                }

                // Firebase Auth에 credential 연동
                string credentialToken = _provider.GetCredentialToken();
                if (_firebase != null && !string.IsNullOrEmpty(credentialToken))
                {
                    _firebase.AuthBridge.LinkWithCredential(credentialToken, firebaseResult =>
                    {
                        onComplete?.Invoke(firebaseResult);
                        OnSignInComplete?.Invoke(firebaseResult);
                    });
                }
                else
                {
                    onComplete?.Invoke(platformResult);
                    OnSignInComplete?.Invoke(platformResult);
                }
            });
        }

        public void SignOut()
        {
            _provider?.SignOut();
            _firebase?.AuthBridge.SignOut();
            OnSignOutComplete?.Invoke();
        }
    }
}
