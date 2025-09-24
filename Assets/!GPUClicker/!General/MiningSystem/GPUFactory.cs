using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "GPUFactory", menuName = "GPU/GPUFactory")]
public class GPUFactory : ScriptableObject
{
    [Serializable]
    public struct GPUTypeDataPair
    {
        [HideInInspector] public string Name;
        [HideInInspector] public GPUs Type;
        public GPUConfig Config;
    }

    public GPUTypeDataPair[] GPUs;

    public GPUConfig Get(GPUs GPUType)
    {
        var pair = GPUs.FirstOrDefault(p => p.Type == GPUType);
        return pair.Config;
    }
}
