namespace ISEBank.Flags
{
    internal abstract class LoginFlag(LoginStatuses loginStatus)
    {
        public LoginStatuses LoginStatus { get; init; } = loginStatus;

        public virtual bool IsLoggedIn() => (LoginStatus == LoginStatuses.Sucessful);
    }

    public enum LoginStatuses
    {
        None,
        Locked,
        Sucessful
    }
}
