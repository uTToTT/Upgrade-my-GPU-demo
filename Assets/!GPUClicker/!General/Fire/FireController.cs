using UnityEngine;

public class FireController
{
    private readonly float _maxTemperature;
    private readonly float _fireDuration;
    private float _timeAtMaxTemp;

    public bool IsOnFire { get; private set; }

    public FireController(float maxTemperature, float fireDuration = 5f)
    {
        _maxTemperature = maxTemperature;
        _fireDuration = fireDuration;
    }

    public void Update(float currentTemperature, float deltaTime)
    {
        if (IsOnFire) return;

        float dangerThreshold = _maxTemperature * 0.99f;
        if (currentTemperature >= dangerThreshold)
        {
            _timeAtMaxTemp += deltaTime;
            if (_timeAtMaxTemp >= _fireDuration)
                StartFire();
        }

        else
        {
            _timeAtMaxTemp = 0f;
        }
    }

    private void StartFire()
    {
        IsOnFire = true;
        Debug.Log("FIRE! GPU is burning!");
    }

    public void ExtinguishFire()
    {
        IsOnFire = false;
        _timeAtMaxTemp = 0f;
        Debug.Log("Fire extinguished, GPU restored!");
    }
}
