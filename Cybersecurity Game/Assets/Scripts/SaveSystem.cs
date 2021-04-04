using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static readonly string
        SAVE_FOLDER = Application.dataPath + "/Saves/";

    private static readonly string
        DICTIONARY_FOLDER = Application.dataPath + "/Dictionary/";

    public static void Init()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory (SAVE_FOLDER);
            Debug.Log("Save folder not founded - created new");
        }
    }

    public static void Save(string saveString)
    {
        File.WriteAllText(SAVE_FOLDER + "save.json", saveString);
    }

    public static string Load()
    {
        // DONT DELETE - Might need in the future
        // DirectoryInfo directoryInfo = new DiretoryInfo(SAVE_FOLDER);
        // FileInfo[] SaveFiles = directoryInfo.GetFiles();
        if (File.Exists(SAVE_FOLDER + "save.json"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "save.json");
            return saveString;
        }
        else
        {
            return null;
        }
    }

    public static string LoadDictionary(string jsonString)
    {
        if (File.Exists(DICTIONARY_FOLDER + jsonString))
        {
            string saveString =
                File.ReadAllText(DICTIONARY_FOLDER + jsonString);
            return saveString;
        }
        else
        {
            Debug.Log("Error, Unable to find " + jsonString + " dictionary");
            return null;
        }
    }
}
