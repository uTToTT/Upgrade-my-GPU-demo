using System;
using System.Collections;
using UnityEngine;

public enum TimeType
{
    Scaled,
    UnScaled,
}

public class DelayedExecutor : MonoBehaviour
{
    private static DelayedExecutor _instance;

    public static DelayedExecutor Instance
    {
        get
        {
            if (_instance == null)
            {
                var go = new GameObject("DelayedExecutor");
                _instance = go.AddComponent<DelayedExecutor>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    public IEnumerator Wait(float delaySeconds, TimeType timeType)
    {
        switch (timeType)
        {
            case TimeType.Scaled:
                float scaledElapsed = 0f;
                while (scaledElapsed < delaySeconds)
                {
                    scaledElapsed += Time.deltaTime;

                    yield return null;
                }
                break;

            case TimeType.UnScaled:
                float unscaledElapsed = 0f;
                while (unscaledElapsed < delaySeconds)
                {
                    unscaledElapsed += Time.unscaledDeltaTime;
                    yield return null;
                }
                break;
        }
    }


    public DelayedCallHandle Execute(float delaySeconds, TimeType timeType, Action action)
    {
        var handle = new DelayedCallHandle();

        if (delaySeconds <= 0f)
        {
            action?.Invoke();
        }
        else
        {
            handle.coroutine = StartCoroutine
                (ExecuteDelayed(delaySeconds, timeType, action, handle));
        }

        return handle;
    }

    private IEnumerator ExecuteDelayed
        (float delay, TimeType timeType, Action action, DelayedCallHandle handle)
    {
        switch (timeType)
        {
            case TimeType.Scaled:
                float scaledElapseTime = 0f;

                while (scaledElapseTime < delay)
                {
                    scaledElapseTime += Time.deltaTime;

                    yield return null;
                }

                break;
            case TimeType.UnScaled:
                float elapseTime = 0f;

                while (elapseTime < delay)
                {
                    elapseTime += Time.unscaledDeltaTime;
                    yield return null;
                }

                break;
        }

        if (!handle.IsCanceled)
        {
            action?.Invoke();
        }
    }

    public void Cancel(DelayedCallHandle handle)
    {
        if (handle?.coroutine != null)
        {
            StopCoroutine(handle.coroutine);
            handle.IsCanceled = true;
        }
    }
}

public class DelayedCallHandle
{
    internal Coroutine coroutine;
    public bool IsCanceled { get; internal set; } = false;

    public void Cancel()
    {
        DelayedExecutor.Instance.Cancel(this);
    }
}