using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    private GameObject SpawnManager;
    public int index;
    public int childCount;
    private GameObject selected;
    private GameObject MenuUiObject;

    public GameObject Menu;
    
    private GameObject activeObject;
    // Start is called before the first frame update

    void Start()
    {   
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

        // <-
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Movement("left");
        }
        // ->
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            Movement("right");
        }
        //Space
        if(Input.GetKeyDown(KeyCode.Space))
        {
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
            selected = activeObject.transform.GetChild(index).gameObject;
            //moves the cursor beneath the object
            transform.position = new Vector3(selected.transform.position.x,selected.transform.position.y,0);

        }
    }


    void Interact()
    {
        //do some shit maybe
        // activeObject.transform.GetComponent<SpawnManager>().Select(index);
        // Debug.Log("Class name " + activeObject.name);
        if(activeObject.name == "Start")
        {
            if(index == 0)
            {
                Menu.transform.GetComponent<MenuUIHandler>().StartGame();
                activeObject = SpawnManager;
                Debug.Log("Class name " + activeObject.name);
                Debug.Log("Comeca ai mermao");    
            }
            else
            {
                Menu.transform.GetComponent<MenuUIHandler>().ExitGameOnMenu();   
                Debug.Log("flws ai mermao");    
            }
        }

        if(activeObject.name == "Objects")
        {
            Debug.Log("cara entrei aqui?");
            bool didPlayerMiss = activeObject.transform.GetComponent<SpawnManager>().Select(index);

            if(didPlayerMiss)
            {
                //gameover
                // // var gameOver = Resources.FindObjectsOfTypeAll<GameOver>();
                var obj= Resources.FindObjectsOfTypeAll<GameOver>();
                // Debug.Log(obj[0]);
                obj[0].gameObject.SetActive(true);

            }
            else
            {
                Debug.Log("fuku");
            }
        }
    }

}
