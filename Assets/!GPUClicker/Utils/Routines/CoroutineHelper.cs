namespace UTToTTGames.Routines
{
    using System.Collections;
    using UnityEngine;

    public static class CoroutineHelper
    {
        public static Coroutine StartControlledRoutine
            (this MonoBehaviour mb, ref Coroutine routine, IEnumerator coroutine, string coroutineName, bool debug = true)
        {
            if (routine != null)
            {
                if (debug)
                {
                    Debug.LogWarning($"[{mb.GetType().Name}][{coroutineName}] Is already running.");
                }
                return routine;
            }

            routine = mb.StartCoroutine(coroutine);
            return routine;
        }

        public static void StopControlledRoutine
            (this MonoBehaviour mb, ref Coroutine routine, string coroutineName, bool debug = true)
        {
            if (routine != null)
            {
                mb.StopCoroutine(routine);
                routine = null;
            }
            else
            {
                if (!debug)
                {
                    Debug.LogWarning($"[{mb.GetType().Name}][{coroutineName}] Is not running.");
                }
            }
        }
    }
}

