using System;

namespace RaiseTheHorse.Platform.Auth
{
    /// <summary>
    /// 플랫폼별 인증 제공자 인터페이스.
    /// Google/Apple/OneStore 각각 이 인터페이스를 구현한다.
    /// </summary>
    public interface IAuthProvider
    {
        /// <summary>현재 로그인 상태인지</summary>
        bool IsSignedIn { get; }

        /// <summary>로그인 시도</summary>
        void SignIn(Action<AuthResult> onComplete);

        /// <summary>로그아웃</summary>
        void SignOut();

        /// <summary>Firebase 연동용 credential 토큰 (ID Token 등)</summary>
        string GetCredentialToken();
    }
}
