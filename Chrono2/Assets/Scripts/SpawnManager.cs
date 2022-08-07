using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
public class SpawnManager : MonoBehaviour
{
    public float spawnYLocation;
    public float[] spawnXLocation;
    public GameObject prefab;
    public GameObject emptyPrefab;
    public GameObject sprites;
    public int score;
    public int numberOfItems;

    public bool newRound;
    public bool timeOut;
    public bool correct;
    public bool gameOver;

    public List<int> objectsAge;    
    public List<int> objectsIndex; 
    public JSONReader json;

    public MenuUIHandler MUH;
    
    // Start is called before the first frame update
    void Start()
    {
        json = new JSONReader();
        json.Load();
        //get MUH
        MUH = GameObject.Find("Menu").GetComponent<MenuUIHandler>();
        //score
        score=0;

        newRound  = true;

        numberOfItems=3;
        spawnYLocation = 6.0f;
        spawnXLocation = new float[3];
        objectsAge = new List<int>{0,0,0};
        objectsIndex = new List<int>{-2,-2,-2};

        //assing values 
        spawnXLocation[0] = -5.0f;
        spawnXLocation[1] =  0.0f;
        spawnXLocation[2] =  5.0f;

        //get sprites
        sprites = GameObject.Find("Sprites");

        // get a empty prefab for "destroy"
        emptyPrefab = transform.GetChild(0).gameObject;
    }

    public void pickObjects()
    {
         var childCount = sprites.transform.childCount;

       
        for(var i=0; i<numberOfItems;i++)
        {
            //location
            Vector3 spawnLocation = new Vector3(spawnXLocation[i], spawnYLocation,0.0f); 
            //get a random item index
            int randomChild = -1;
            do 
            {
                randomChild = Random.Range(0,childCount-1);

            }while (objectsIndex.Contains(randomChild));
            //save index
            objectsIndex[i] = randomChild;
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
        if(newRound == true && MUH.isGameActive == true)
        {
            pickObjects();
            newRound=false;
        }
     
    }
    public bool Select(int index)
    {
        if(index == objectsAge.IndexOf(objectsAge.Min()))
        {
            
            Debug.Log("ACERTOU "+index+" "+objectsAge.IndexOf(objectsAge.Min()));
            newRound=true;
            score++;

            return false;
        }
        else
        {
            Debug.Log("ERROU "+index+" "+objectsAge.IndexOf(objectsAge.Min()));
            Debug.Log("Score  : "+score);
            MUH.isGameActive=false;
            for(var i=0; i<numberOfItems;i++)
            {
                prefab = transform.GetChild(i).gameObject;
                //set the sprite
                prefab.GetComponent<SpriteRenderer>().sprite = null ;
                //set the text
                prefab.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Text>().text ="";
                //set item age value
                objectsAge[i] = 0;
            }
            //game over
            gameOver=true;
            newRound=true;
            score=0;
            return true;

        }
    }
}
