using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
public class JSONReader : MonoBehaviour
{
    public string path;
    public TextAsset jsonFile;
    public JSONNode data;
    public void Load() 
    {
        path="./Assets/Scripts/TimeLine.json";
        string jsonString = File.ReadAllText(path); 
        data = JSON.Parse(jsonString);
        foreach(JSONNode item in data["Items"])
        {
            // Debug.Log ("name: " + item["name"].Value + " time: " + item["time"].AsInt);
        }
        //proliferate data


    }
}

[System.Serializable]
public class Item
{
    public string name;
    public string time;
}