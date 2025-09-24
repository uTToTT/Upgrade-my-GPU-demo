#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(VoodooFactory))]
public class VoodooFactoryEditor : Editor
{
    private void OnEnable()
    {
        VoodooFactory factory = (VoodooFactory)target;

        Voodoos[] enumValues = (Voodoos[])Enum.GetValues(typeof(Voodoos));

        Dictionary<Voodoos, VoodooFactory.VoodooTypeDataPair> existingPairs =
            new Dictionary<Voodoos, VoodooFactory.VoodooTypeDataPair>();
        if (factory.VoodooConfigs != null)
        {
            foreach (var pair in factory.VoodooConfigs)
            {
                existingPairs[pair.Type] = pair;
            }
        }

        factory.VoodooConfigs = new VoodooFactory.VoodooTypeDataPair[enumValues.Length];
        for (int i = 0; i < enumValues.Length; i++)
        {
            var type = enumValues[i];
            if (existingPairs.TryGetValue(type, out var oldPair))
                factory.VoodooConfigs[i] = oldPair;
            else
                factory.VoodooConfigs[i] = new VoodooFactory.VoodooTypeDataPair
                {
                    Name = type.ToString(),
                    Type = type,
                    Config = null
                };
        }

        EditorUtility.SetDirty(factory);
    }
}
#endif