namespace ISEBank.Flags
{
    internal class LockedFlag : LoginFlag
    {
        public DateTime TimeLocked { get; init; }

        public LockedFlag() : base(LoginStatuses.Locked)
        {
            TimeLocked = DateTime.UtcNow;
        }

        public bool IsCleared()
        {
            DateTime now = DateTime.UtcNow;
            return now >= EndDateTime();
        }

        public int TimeRemaining()
        {
            DateTime now = DateTime.UtcNow;
            TimeSpan timeRemaining = EndDateTime().Subtract(now);
            return timeRemaining.Minutes;
        }

        private DateTime EndDateTime() => TimeLocked.AddMinutes(Bank.lockOutMinutes);
    }
}
