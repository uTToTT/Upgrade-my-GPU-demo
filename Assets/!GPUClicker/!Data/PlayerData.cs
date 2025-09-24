using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class PlayerData
{
    private Locations _lastLocation;

    private GPUs _selectedGPU;
    private Generators _selectedGenerator;
    private Coolings _selectedCooling;
    private Voodoos _selectedVoodoo;

    private List<GPUs> _openGPUs;
    private List<Generators> _openGenerators;
    private List<Coolings> _openCoolings;
    private List<Voodoos> _openVoodoos;

    private float _money;

    public PlayerData()
    {
        _money = 1;

        _lastLocation = Locations.Home;

        _selectedGPU = GPUs.Potato;
        _selectedGenerator = Generators.None;
        _selectedCooling = Coolings.None;
        _selectedVoodoo = Voodoos.None;

        _openGPUs = new List<GPUs>() { _selectedGPU };
        _openGenerators = new List<Generators>() { _selectedGenerator };
        _openCoolings = new List<Coolings> { _selectedCooling };
        _openVoodoos = new List<Voodoos> { _selectedVoodoo };
    }

    [JsonConstructor]
    public PlayerData(
        float money,

        Locations lastlocation,

        GPUs selectedGPU,
        Generators selectedGenerator,
        Coolings selectedCooling,
        Voodoos selectedVoodoo,

        List<GPUs> openGPUs,
        List<Generators> openGenerators,
        List<Coolings> openCoolings,
        List<Voodoos> openVoodoos)
    {
        _money = money;

        _lastLocation = lastlocation;

        _selectedGPU = selectedGPU;
        _selectedGenerator = selectedGenerator;
        _selectedCooling = selectedCooling;
        _selectedVoodoo = selectedVoodoo;

        _openGPUs = new List<GPUs>(openGPUs);
        _openGenerators = new List<Generators>(openGenerators);
        _openCoolings = new List<Coolings>(openCoolings);
        _openVoodoos = new List<Voodoos>(openVoodoos);
    }

    public float Money
    {
        get => _money;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            _money = value;
        }
    }

    public Locations LastLocation
    {
        get => _lastLocation;
        set => _lastLocation = value;
    }

    public GPUs SelectedGPU
    {
        get => _selectedGPU;
        set
        {
            if (_openGPUs.Contains(_selectedGPU) == false)
                throw new ArgumentException(nameof(_selectedGPU));

            _selectedGPU = value;
        }
    }

    public Generators SelectedGenerator
    {
        get => _selectedGenerator;
        set
        {
            if (_openGenerators.Contains(_selectedGenerator) == false)
                throw new ArgumentException(nameof(_selectedGenerator));

            _selectedGenerator = value;
        }
    }

    public Coolings SelectedCooling
    {
        get => _selectedCooling;
        set
        {
            if (_openCoolings.Contains(_selectedCooling) == false)
                throw new ArgumentException(nameof(_selectedCooling));

            _selectedCooling = value;
        }
    }

    public Voodoos SelectedVoodoo
    {
        get => _selectedVoodoo;
        set
        {
            if (_openVoodoos.Contains(_selectedVoodoo) == false)
                throw new ArgumentException(nameof(_selectedVoodoo));

            _selectedVoodoo = value;
        }
    }

    public IEnumerable<GPUs> OpenGPUs => _openGPUs;
    public IEnumerable<Generators> OpenGenerators => _openGenerators;
    public IEnumerable<Coolings> OpenCoolings => _openCoolings;
    public IEnumerable<Voodoos> OpenVoodoos => _openVoodoos;

    public void OpenGPU(GPUs GPU)
    {
        if (_openGPUs.Contains(GPU))
            throw new ArgumentException(nameof(GPU));

        _openGPUs.Add(GPU);
    }

    public void OpenGenerator(Generators gen)
    {
        if (_openGenerators.Contains(gen))
            throw new ArgumentException(nameof(gen));

        _openGenerators.Add(gen);
    }

    public void OpenCooling(Coolings cooling)
    {
        if (_openCoolings.Contains(cooling))
            throw new ArgumentException(nameof(cooling));

        _openCoolings.Add(cooling);
    }

    public void OpenVoodoo(Voodoos voodoo)
    {
        if (_openVoodoos.Contains(voodoo))
            throw new ArgumentException(nameof(voodoo));

        _openVoodoos.Add(voodoo);
    }
}
