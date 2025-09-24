namespace UTToTTGames.Debug
{
    public interface IDebuggable
    {
        bool Debugging { get; }
        void SetDebug(bool state);
    }
}

