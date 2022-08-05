using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    private GameObject objectSelected;
    // Start is called before the first frame update
    void Start()
    {   
        //start with menu selection/
        // objectSelected
    }

    // Update is called once per frame
    void Update()
    {
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
        // if(Input.GetKeyDown(KeyCode.Space))
        // {
        //     Interact();
        // }
    }

    void Movement(string direction)
    {
        if(direction=="left")
        {

        }
        else if(direction=="right")
        {

        }
    }

    // void Interact()
    // {
    //     objectSelected.Interact();
    // }
}
