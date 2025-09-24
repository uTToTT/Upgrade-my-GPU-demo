using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopContent", menuName = "Shop/ShopContent")]
public class ShopContent : ScriptableObject
{
    [SerializeField] private List<GPUItem> _gpuItems = new();
    [SerializeField] private List<GeneratorItem> _generatorItems = new();
    [SerializeField] private List<CoolingItem> _coolingItems = new();
    [SerializeField] private List<VoodooItem> _voodooItems = new();

    public IEnumerable<GPUItem> GPUItems => _gpuItems;
    public IEnumerable<GeneratorItem> GeneratorItems => _generatorItems;
    public IEnumerable<CoolingItem> CoolingsItems => _coolingItems;
    public IEnumerable<VoodooItem> VoodooItems => _voodooItems;

    private void OnValidate()
    {
        var gpuItemsDuplicates = _gpuItems.GroupBy(item => item.GPUType)
            .Where(array => array.Count() > 1);

        if (gpuItemsDuplicates.Count() > 0)
            throw new InvalidOperationException(nameof(_gpuItems));

        //

        var genItemsDuplicates = _generatorItems.GroupBy(item => item.GeneratorType)
            .Where(array => array.Count() > 1);

        if (genItemsDuplicates.Count() > 0)
            throw new InvalidOperationException(nameof(_generatorItems));

        //

        var coolItemsDuplicates = _coolingItems.GroupBy(item => item.CoolingType)
            .Where(array => array.Count() > 1);

        if (coolItemsDuplicates.Count() > 0)
            throw new InvalidOperationException(nameof(_coolingItems));

        //

        var voodooItemsDuplicates = _voodooItems.GroupBy(item => item.VoodooType)
            .Where(array => array.Count() > 1);

        if (voodooItemsDuplicates.Count() > 0)
            throw new InvalidOperationException(nameof(_voodooItems));
    }
}
