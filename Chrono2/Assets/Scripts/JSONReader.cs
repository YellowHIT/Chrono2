using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
public class JSONReader : MonoBehaviour
{
    string jsonString;
    public string path;
    public TextAsset jsonFile;
    public JSONNode data;
    public void Load() 
    {
    //     path="./Assets/Scripts/TimeLine.json";
    //     string jsonString = File.ReadAllText(path); 
        jsonString = "{\"Items\":[{\"name\":\"Egg\" ,   \"time\":  \"-340000000\"   },{\"name\":\"Fire\" ,  \"time\":  \"-300000\"   },{\"name\":\"Castle\", \"time\":  \"-3000\"   },{\"name\":\"Animal Domestication\", \"time\":  \"-8000\"   },{\"name\":\"Apocalypse\" , \"time\":  \"1000000\"   },{\"name\":\"Spear\" , \"time\":  \"-500000\"   },{\"name\":\"Beer\" ,  \"time\":  \"-7000\"   },{\"name\":\"Wine\" ,  \"time\":  \"-8500\"   },{\"name\":\"Cheese\", \"time\":  \"-8000\"   },{\"name\":\"Bread\" , \"time\":  \"-8000\"   },{\"name\":\"Fabric\" ,\"time\":  \"-5000\"   },{\"name\":\"Leather\",\"time\":  \"-1500\"   },{\"name\":\"Paper\" , \"time\":  \"220\"   },{\"name\":\"Coin\" ,  \"time\":  \"-600\"   },{\"name\":\"Candle\" ,\"time\":  \"-200\"   },{\"name\":\"Lantern\",\"time\":  \"-230\"   },{\"name\":\"Map\" ,   \"time\":  \"-600\"   },{\"name\":\"Axe\" ,   \"time\":  \"-6000\"   },{\"name\":\"Pickaxe\",\"time\":  \"-2700\"   },{\"name\":\"Sword\" , \"time\":  \"-3300\"   },{\"name\":\"Shield\" ,\"time\":  \"-1300\"   },{\"name\":\"Chicken\",\"time\":  \"-58000\"   }]}";
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