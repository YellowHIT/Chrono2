using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONReader : MonoBehaviour
{
    public TextAsset jsonFile;
    private void Start() 
    {
        // Load();
        Items ItemsInJson = JsonUtility.FromJson<Items>(jsonFile.text);
        Debug.Log( JsonUtility.FromJson<Items>(jsonFile.text));
        PropertyInfo[] property = typeof(MyCustomClass).GetProperties(); 
        foreach (Item item in ItemsInJson.ItemsList)
        {
            Debug.Log(ItemsInJson.ItemsList);

            Debug.Log("Found Item: " + item.name + " " + item.time);
        }
    }
    public void Load()
    {

    }
}
[System.Serializable]
public class Items
{
    //Items is case sensitive and must match the string "Items" in the JSON.
    public Item[] ItemsList;
}
[System.Serializable]
public class Item
{
    public string name;
    public string time;
}