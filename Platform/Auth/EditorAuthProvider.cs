using System;
using UnityEngine;

namespace RaiseTheHorse.Platform.Auth
{
    /// <summary>
    /// Unity Editor용 Mock 인증 제공자.
    /// 실제 SDK 없이 로그인 플로우를 테스트할 수 있다.
    /// </summary>
    public class EditorAuthProvider : MonoBehaviour, IAuthProvider
    {
        public bool IsSignedIn { get; private set; }

        public void SignIn(Action<AuthResult> onComplete)
        {
            Debug.Log("[EditorAuthProvider] Mock sign-in success");
            IsSignedIn = true;
            onComplete?.Invoke(AuthResult.Success(
                userId: "editor_test_user",
                displayName: "Editor Test User",
                email: "test@editor.local",
                idToken: "mock_id_token"
            ));
        }

        public void SignOut()
        {
            IsSignedIn = false;
            Debug.Log("[EditorAuthProvider] Mock sign-out");
        }

        public string GetCredentialToken()
        {
            return IsSignedIn ? "mock_id_token" : null;
        }
    }
}
