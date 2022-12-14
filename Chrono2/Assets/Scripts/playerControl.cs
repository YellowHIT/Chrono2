using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    public int index;
    public int childCount;
    private GameObject selected;
    private GameObject MenuUiObject;
    private GameObject Score;
    private GameObject SpawnManager;
    public GameObject Menu;
    public float offset;
    private GameObject activeObject;
    public AudioSource gameAudio;
    public AudioClip playerMoveSFX;

    public AudioClip correctSFX;
    public AudioClip wrongSFX;
    // Start is called before the first frame update

    void Start()
    {   
        gameAudio = GetComponent<AudioSource>();

        //start with menu selection
        Menu = GameObject.Find("Menu");

        //SpawnManager
        SpawnManager = GameObject.Find("Objects");

        MenuUiObject = GameObject.Find("MenuUiObject");

        
        //Start Menu
        StartMenu();

        // childCount = SpawnManager.transform.childCount;

        // index = (int) childCount/2;

        // selected = SpawnManager.transform.GetChild(index).gameObject;
        // //moves the cursor beneath the object
        // transform.position = new Vector3(selected.transform.position.x,-3.5f,0);

    }

    void StartMenu()
    {
        //get start button
        var position = MenuUiObject.transform.GetChild(0).GetChild(0).transform.position;
        transform.position = position; // element is the Text show in the UI.
        activeObject = MenuUiObject.transform.GetChild(0).gameObject;//Start menu
        Debug.Log(activeObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        childCount = activeObject.transform.childCount;
        selected = activeObject.transform.GetChild(index).gameObject;
        if(activeObject.name=="Objects")
            offset=4.0f;
        else if(activeObject.name == "GameOver")
            offset=1;
        else
            offset=0;

        //moves the cursor beneath the object
        transform.position = new Vector3(selected.transform.position.x,selected.transform.position.y-offset,0);
        // <-
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gameAudio.PlayOneShot(playerMoveSFX,1.0f);
            Movement("left");
        }
        // ->
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            gameAudio.PlayOneShot(playerMoveSFX,1.0f);
            Movement("right");
        }
        //Space
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // gameAudio.PlayOneShot(playerMoveSFX,1.0f);
            Interact();
        }
    }

    void Movement(string direction)
    {
        // selected = null;
        if(childCount!=0)
        {
            if(direction=="left")
            {
                if(index==0)
                    index=childCount-1;
                else
                    index--;
            }
            else if(direction=="right")
            {
                if(index==childCount-1)
                    index=0;
                else
                    index++;
            }
            Debug.Log(index);
            // selected = activeObject.transform.GetChild(index).gameObject;
            // //moves the cursor beneath the object
            // transform.position = new Vector3(selected.transform.position.x,selected.transform.position.y,0);

        }
    }


    void Interact()
    {
        //do some shit maybe
        // activeObject.transform.GetComponent<SpawnManager>().Select(index);
        // Debug.Log("Class name " + activeObject.name);
        if(activeObject.name == "Start")
        {
            gameAudio.PlayOneShot(playerMoveSFX,1.0f);

            if(index == 0)
            {
                SpawnManager.transform.GetComponent<SpawnManager>().timer=11;
                Menu.transform.GetComponent<MenuUIHandler>().StartGame();
                var obj= Resources.FindObjectsOfTypeAll<Score>();
                obj[0].gameObject.SetActive(true);
                activeObject = SpawnManager;
                
                index=1;
                // Debug.Log("Class name " + activeObject.name);
                // Debug.Log("Comeca ai mermao");    
            }
            else
            {
                Menu.transform.GetComponent<MenuUIHandler>().ExitGameOnMenu();   
                // Debug.Log("flws ai mermao");    
            }
        }

        else if(activeObject.name == "Objects")
        {
            // Debug.Log("cara entrei aqui?");
            bool didPlayerMiss = activeObject.transform.GetComponent<SpawnManager>().Select(index);

            if(didPlayerMiss)
            {
                gameAudio.PlayOneShot(wrongSFX,7.0f);
                //gameover
                playerGameOver();
            }
            else
            {
                gameAudio.PlayOneShot(correctSFX,0.8f);
                // Debug.Log("fuku");
            }
        }
        else if(activeObject.name == "GameOver")
        {
            gameAudio.PlayOneShot(playerMoveSFX,1.0f);
            if(index == 0)
            {
                var obj= Resources.FindObjectsOfTypeAll<GameOver>();
                obj[0].gameObject.SetActive(false);
                Menu.transform.GetComponent<MenuUIHandler>().StartGame();
                activeObject = SpawnManager;
                activeObject.gameObject.transform.GetComponent<SpawnManager>().timer = 11;
                index=1;
                Debug.Log("Class name " + activeObject.name);
                Debug.Log("Comeca ai mermao");    
            }
            else
            {
                Menu.transform.GetComponent<MenuUIHandler>().ExitGameOnMenu();   
                Debug.Log("flws ai mermao");    
            }
        }



    }
    public void playerGameOver()
    {
        // // var gameOver = Resources.FindObjectsOfTypeAll<GameOver>();
        var obj= Resources.FindObjectsOfTypeAll<GameOver>();
        // Debug.Log(obj[0]);
        obj[0].gameObject.SetActive(true);
        //set gameoverui as active
        activeObject = MenuUiObject.transform.GetChild(1).gameObject;
        index=0;
    }
}
