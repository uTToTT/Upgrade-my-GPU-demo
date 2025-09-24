using UnityEngine;

public class CoolingConfig : ScriptableObject
{
    [SerializeField] private Coolings Type;
    [SerializeField] private float CoolingPerSecond;
}
