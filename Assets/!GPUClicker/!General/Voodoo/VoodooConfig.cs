using UnityEngine;

[CreateAssetMenu(fileName = "Voodoo", menuName = "Voodoo/Voodoo")]
public class VoodooConfig : ScriptableObject
{
    [field: SerializeField, Range(1f, 2f)] public float MaxVoodooFactor;
    [field: SerializeField, Range(0f, 1f)] public float FactorByClick;
    [field: SerializeField, Range(1f, 30f)] public float Duration;
    [field: SerializeField, Range(1f, 20f)] public float Radius;
}
