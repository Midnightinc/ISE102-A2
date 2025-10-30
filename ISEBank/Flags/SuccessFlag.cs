namespace ISEBank.Flags
{
    internal class SuccessFlag(string usernameUsed) : LoginFlag(LoginStatuses.Sucessful)
    {
        public string UsernameUsed { get; init; } = usernameUsed;
    }
}
