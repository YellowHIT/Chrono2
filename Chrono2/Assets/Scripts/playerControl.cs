using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    private GameObject SpawnManager;
    public int index;
    public int childCount;
    private GameObject selected;
    
    // Start is called before the first frame update
    void Start()
    {   
        //start with menu selection/
        // objectSelected
        SpawnManager = GameObject.Find("Objects");
        childCount = SpawnManager.transform.childCount;

        index = (int) childCount/2;

        selected = SpawnManager.transform.GetChild(index).gameObject;
        //moves the cursor beneath the object
        transform.position = new Vector3(selected.transform.position.x,-3.5f,0);

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
            selected = SpawnManager.transform.GetChild(index).gameObject;
            //moves the cursor beneath the object
            transform.position = new Vector3(selected.transform.position.x,-3.5f,0);

        }
    }


    void Interact()
    {
        //do some shit maybe
        SpawnManager.transform.GetComponent<SpawnManager>().Select(index);
    }
}
