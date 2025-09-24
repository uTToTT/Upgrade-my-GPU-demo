using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class DataLocalProvider : IDataProvider
{
    private const string FILE_NAME = "PlayerSave";
    private const string SAVE_FILE_EXTENSION = ".json";

    private IPersistentData _persistentData;

    public DataLocalProvider(IPersistentData persistentData) => _persistentData = persistentData;

    private string SavePath => Application.persistentDataPath;
    private string FullPath => Path.Combine(SavePath, $"{FILE_NAME}{SAVE_FILE_EXTENSION}");

    public bool TryLoad()
    {
        if (IsDataAlreadyExist() == false)
            return false;

        _persistentData.PlayerData = 
            JsonConvert.DeserializeObject<PlayerData>(File.ReadAllText(FullPath));

        return true;
    }

    public void Save()
    {
        File.WriteAllText(FullPath, JsonConvert.SerializeObject(_persistentData.PlayerData, Formatting.Indented, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        }));
    }

    public void Delete() => File.Delete(FullPath);

    private bool IsDataAlreadyExist() => File.Exists(FullPath);
}
