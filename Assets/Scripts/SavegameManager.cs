using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavegameManager : MonoBehaviour
{
    public static MeinSpielNameSavegame loadedSavegame = null;
    public static string savegamePath = "savegame.json";

    public static void Load()
    {
        print("Loading savegame");
        System.IO.FileStream file = System.IO.File.Open(savegamePath, System.IO.FileMode.OpenOrCreate);
        System.IO.StreamReader reader = new System.IO.StreamReader(file);
        if (file.Equals(null))
        {
            Debug.Log("No savegame found");
            NewSave();
            return;
        }
        string content = reader.ReadToEnd();
        loadedSavegame = JsonUtility.FromJson<MeinSpielNameSavegame>(content);
        if (loadedSavegame == null)
        {
            Debug.Log("Savegame is empty");
            NewSave();
            return;
        }
        reader.Close();
        file.Close();
        print ("Savegame loaded succesfully");
        return;
    }

    public static void Save()
    {
        print("Saving savegame");
        System.IO.FileStream file = System.IO.File.Open(savegamePath, System.IO.FileMode.OpenOrCreate);
        System.IO.StreamWriter writer = new System.IO.StreamWriter(file);
        string content = JsonUtility.ToJson(loadedSavegame);
        writer.Write(content);
        writer.Close();
        file.Close();
        print("Savegame saved succesfully");
        return;
    }

    private static void NewSave()
    {
        loadedSavegame = new MeinSpielNameSavegame();
        print("New savegame created");
        Save();
    }


}
