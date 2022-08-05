using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    private GameObject SpawnManager;
    public int index;
    public int childCount;
    
    // Start is called before the first frame update
    void Start()
    {   
        //start with menu selection/
        // objectSelected
        SpawnManager = GameObject.Find("SpawnManager");
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        childCount = SpawnManager.transform.childCount;

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
        //  Space
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Interact();
        }
    }

    void Movement(string direction)
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
    }

    void Interact()
    {
        GameObject selected;
        selected = SpawnManager.transform.GetChild(index).gameObject;
        //do some shit maybe
    }
}
