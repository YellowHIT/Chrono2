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
    public int highScore;
    public int currentScore;
    public int numberOfItems;
    public float timer;
    public bool newRound;
    public bool timeOut;
    public bool correct;
    public bool isGameOver;

    public List<int> objectsAge;    
    public List<int> objectsIndex; 
    public JSONReader json;

    public MenuUIHandler MUH;
    public playerControl player;
    
    // Start is called before the first frame update
    void Start()
    {
        json = new JSONReader();
        json.Load();
        //get MUH
        MUH = GameObject.Find("Menu").GetComponent<MenuUIHandler>();
        player = GameObject.Find("Player").GetComponent<playerControl>();
        //score
        score=0;
        highScore=0;
        currentScore=0;
        
        timer=0;
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
        if(timer>=0 && MUH.isGameActive == true)
        {
            timer -= Time.deltaTime;
            if(timer<0)
            {
                gameOver();
                player.playerGameOver();
            }
            var timerObj= Resources.FindObjectsOfTypeAll<Score>();
            timerObj[0].gameObject.transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>().text = ((int)timer).ToString();
            Debug.Log(timer);    
        }       
        
        if(newRound == true && MUH.isGameActive == true)
        {
            pickObjects();
            newRound=false;

            //set score text
            var obj= Resources.FindObjectsOfTypeAll<Score>();
            obj[0].gameObject.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = score.ToString();

        }
     
    }
    public bool Select(int index)
    {
        if(index == objectsAge.IndexOf(objectsAge.Min()))
        {
            
            Debug.Log("ACERTOU "+index+" "+objectsAge.IndexOf(objectsAge.Min()));
            newRound=true;
            timer=10;
            score++;

            return false;
        }
        else
        {
            Debug.Log("ERROU "+index+" "+objectsAge.IndexOf(objectsAge.Min()));
            Debug.Log("Score  : "+score);
            gameOver();
            return true;

        }
    }
    void gameOver()
    {
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
        isGameOver=true;
        newRound=true;
        currentScore=score;
        if(highScore<currentScore)
            highScore=currentScore;

        score=0;
        // timer=10;
    }
}
