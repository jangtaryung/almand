using System;
using UnityEngine;
using RaiseTheHorse.Platform.Auth;

namespace RaiseTheHorse.Platform.Firebase
{
    /// <summary>
    /// 플랫폼 로그인 credential을 Firebase Auth에 연동하는 브릿지.
    /// Google/Apple → Firebase credential 직접 연동.
    /// ONE Store → 서버에서 custom token 발급 후 signInWithCustomToken.
    /// </summary>
    public class FirebaseAuthBridge : MonoBehaviour
    {
        public bool IsLinked { get; private set; }
        public string FirebaseUserId { get; private set; }

        /// <summary>
        /// 플랫폼 credential 토큰으로 Firebase Auth에 로그인/연동한다.
        /// </summary>
        public void LinkWithCredential(string credentialToken, Action<AuthResult> onComplete)
        {
            if (string.IsNullOrEmpty(credentialToken))
            {
                onComplete?.Invoke(AuthResult.Failure("Credential token is null or empty"));
                return;
            }

            // TODO: Firebase Auth SDK 연동
            // 실제 구현 시 (Google 예):
            //   var credential = GoogleAuthProvider.GetCredential(credentialToken, null);
            //   FirebaseAuth.DefaultInstance.SignInWithCredentialAsync(credential)
            //       .ContinueWithOnMainThread(task =>
            //       {
            //           if (task.IsFaulted)
            //               onComplete(AuthResult.Failure(task.Exception.Message));
            //           else
            //           {
            //               var user = task.Result;
            //               FirebaseUserId = user.UserId;
            //               IsLinked = true;
            //               onComplete(AuthResult.Success(user.UserId, user.DisplayName, user.Email, credentialToken));
            //           }
            //       });
            //
            // Apple 예:
            //   var credential = OAuthProvider.GetCredential("apple.com", credentialToken, nonce, null);
            //   → 이후 동일
            //
            // ONE Store 예 (custom token):
            //   FirebaseAuth.DefaultInstance.SignInWithCustomTokenAsync(customTokenFromServer)
            //   → 이후 동일

            // Mock
            FirebaseUserId = "firebase_mock_uid";
            IsLinked = true;
            Debug.Log($"[FirebaseAuthBridge] Linked with credential (mock). UID: {FirebaseUserId}");
            onComplete?.Invoke(AuthResult.Success(FirebaseUserId, "MockUser", "mock@firebase.local", credentialToken));
        }

        public void SignOut()
        {
            // FirebaseAuth.DefaultInstance.SignOut();
            IsLinked = false;
            FirebaseUserId = null;
            Debug.Log("[FirebaseAuthBridge] Firebase signed out");
        }
    }
}
