namespace UTToTTGames.Debug
{
    using UnityEngine;

    public static class DebugUtils
    {
        public static void LogIfDebug(IDebuggable source, string message)
        {
            if (source.Debugging)
                Debug.Log(message);
        }

        public static void LogWarningIfDebug(IDebuggable source, string message)
        {
            if (source.Debugging)
                Debug.LogWarning(message);
        }

        public static void LogErrorIfDebug(IDebuggable source, string message)
        {
            if (source.Debugging)
                Debug.LogError(message);
        }
    }
}

