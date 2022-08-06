using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SpawnManager : MonoBehaviour
{
    public float spawnYLocation;
    public float[] spawnXLocation;
    public GameObject prefab;
    public GameObject sprites;

    public int numberOfItems;

    public bool started;
    public bool timeOut;
    public bool correct;
    public bool gameOver;

    public int[] objectsAge;    
    public JSONReader json;

    // Start is called before the first frame update
    void Start()
    {
        json = new JSONReader();
        json.Load();

        started  = false; 
        timeOut  = false;
        correct  = false;
        gameOver = false;

        numberOfItems=3;
        spawnYLocation = 6.0f;
        spawnXLocation = new float[3];
        
        objectsAge = new int[3];

        //assing values 
        spawnXLocation[0] = -5.0f;
        spawnXLocation[1] =  0.0f;
        spawnXLocation[2] =  5.0f;

        //get sprites

        sprites = GameObject.Find("Sprites");

        var childCount = sprites.transform.childCount;

        Random.Range(0,childCount-1);
        for(var i=0; i<numberOfItems;i++)
        {
            //location
            Vector3 spawnLocation = new Vector3(spawnXLocation[i], spawnYLocation,0.0f); 
            //get a random item index
            int randomChild = Random.Range(0,childCount-1);
            //get game object
            prefab = transform.GetChild(i).gameObject;
            //set the sprite
            prefab.GetComponent<SpriteRenderer>().sprite = sprites.transform.GetChild(randomChild).gameObject.GetComponent<SpriteRenderer>().sprite ;
            //set the text
            prefab.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Text>().text = json.data["Items"][randomChild]["name"].Value;
            //set item age value
            objectsAge[i] = json.data["Items"][randomChild]["time"].AsInt;

        }


    }

    // Update is called once per frame
    void Update()
    {
        //checks if needs to spawn new objects
        if(started == true)
        {
            started=false;
            for(var i=0; i<numberOfItems;i++)
            {
                // Instantiate(prefab, spawnLocation, Quaternion.identity); 
                // prefab.name = names[i];//os nomes ordenados por numnero
                // prafeb.sprite = sprites[prefab.name]//vetor de spirtes ordenados por nome
            }
        }

        
    }
}
