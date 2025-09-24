using UnityEngine;

[CreateAssetMenu(fileName = "Generator", menuName = "Energy/Generator")]
public class GeneratorConfig : ScriptableObject
{
    [SerializeField] public float Production;
}
