namespace RaiseTheHorse.Platform.Auth
{
    public class AuthResult
    {
        public bool IsSuccess { get; private set; }
        public string UserId { get; private set; }
        public string DisplayName { get; private set; }
        public string Email { get; private set; }
        public string IdToken { get; private set; }
        public string ErrorMessage { get; private set; }

        public static AuthResult Success(string userId, string displayName, string email, string idToken)
        {
            return new AuthResult
            {
                IsSuccess = true,
                UserId = userId,
                DisplayName = displayName,
                Email = email,
                IdToken = idToken
            };
        }

        public static AuthResult Failure(string errorMessage)
        {
            return new AuthResult
            {
                IsSuccess = false,
                ErrorMessage = errorMessage
            };
        }
    }
}
