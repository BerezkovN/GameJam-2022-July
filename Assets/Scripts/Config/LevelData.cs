using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NullSoftware.Serialization;
using UnityEngine;

public class LevelData
{
    #region Serialization

    private static BinarySerializer<LevelData> _serializer = new BinarySerializer<LevelData>();
    private static string _directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Ten-Dice");

    public static LevelData Default { get; } = Load() ?? new LevelData();

    #endregion

    #region Config Content

    [BinIndex(0)]
    public uint OpenedLevel { get; set; } = 1;

    #endregion

    #region Save/Load Methods

    private static void Save(LevelData levelData)
    {
        if (!Directory.Exists(_directory))
            Directory.CreateDirectory(_directory);

        string path = Path.Combine(_directory, "data.bin");

        using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
        {
            _serializer.Serialize(levelData);
        }
    }

    private static LevelData Load()
    {
        string path = Path.Combine(_directory, "data.bin");

        if (!File.Exists(path))
            return null;
        using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            return _serializer.Deserialize(fs);
        }
    }

    #endregion
}
