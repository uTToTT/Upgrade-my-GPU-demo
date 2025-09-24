#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(GPUFactory))]
public class GPUFactoryEditor : Editor
{
    private void OnEnable()
    {
        GPUFactory factory = (GPUFactory)target;

        GPUs[] enumValues = (GPUs[])Enum.GetValues(typeof(GPUs));

        Dictionary<GPUs, GPUFactory.GPUTypeDataPair> existingPairs =
            new Dictionary<GPUs, GPUFactory.GPUTypeDataPair>();
        if (factory.GPUs != null)
        {
            foreach (var pair in factory.GPUs)
            {
                existingPairs[pair.Type] = pair;
            }
        }

        factory.GPUs = new GPUFactory.GPUTypeDataPair[enumValues.Length];
        for (int i = 0; i < enumValues.Length; i++)
        {
            var type = enumValues[i];
            if (existingPairs.TryGetValue(type, out var oldPair))
                factory.GPUs[i] = oldPair;
            else
                factory.GPUs[i] = new GPUFactory.GPUTypeDataPair 
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
