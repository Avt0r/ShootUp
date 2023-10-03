using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager
{
    public void SaveDataInt(string name,int data )
    {
        PlayerPrefs.SetInt(name, data);
    }

    public int GetDataInt(string name)
    {
        if(PlayerPrefs.HasKey(name))
        {
            return PlayerPrefs.GetInt(name);
        }
        return 0;
    }

    public void SaveData(string name, string data)
    {
        PlayerPrefs.SetString(name, data);
    }
    
    public string GetData(string name)
    {
        if(PlayerPrefs.HasKey(name))
        {
            return PlayerPrefs.GetString(name);
        }
        return null;
    }

    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

}
