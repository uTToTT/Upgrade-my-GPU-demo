using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "GPU", menuName = "GPU") ]
public class GPUConfig : ScriptableObject
{
    [HorizontalLine]
    [BoxGroup("Info")]
    [SerializeField] public string Name;
    [BoxGroup("Info")]
    [SerializeField] public string Tecnology = "100 nm";

    [HorizontalLine]
    [BoxGroup("Settings")]
    [SerializeField, Min(0.01f)] public float RitualModifier =1;
    [BoxGroup("Settings")]
    [SerializeField, Min(0)] public int Clock = 1;
    [BoxGroup("Settings")]
    [SerializeField, Min(1)] public float MaxHeat = 100;
    [BoxGroup("Settings")]
    [SerializeField, Min(0)] public float HeatPerSecond = 1;
    [BoxGroup("Settings")]
    [SerializeField, Range(0, 1f)] public float ThrottleThreshold = 0.8f;
    [BoxGroup("Settings")]
    [SerializeField, Range(0, 1f)] public float MinFactorAtMaxTemp = 0f;
    [BoxGroup("Settings")]
    [SerializeField, Range(0, 1f)] public float CoolingResponseSpeed = 0.05f;
    [BoxGroup("Settings")]
    [SerializeField, Range(5, 30f)] public float FireDelay = 5f;
    [BoxGroup("Settings")]
    [SerializeField, Min(0)] public float Consumption = 50f;
}
