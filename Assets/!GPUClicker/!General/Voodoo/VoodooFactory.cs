using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "VoodooFactory", menuName = "Voodoo/VoodooFactory")]
public class VoodooFactory : ScriptableObject
{
    [Serializable]
    public struct VoodooTypeDataPair
    {
        [HideInInspector] public string Name;
        [HideInInspector] public Voodoos Type;
        public VoodooConfig Config;
    }

    public VoodooTypeDataPair[] VoodooConfigs;

    public VoodooConfig Get(Voodoos voodooType)
    {
        var pair = VoodooConfigs.FirstOrDefault(p => p.Type == voodooType);
        return pair.Config;
    }
}
