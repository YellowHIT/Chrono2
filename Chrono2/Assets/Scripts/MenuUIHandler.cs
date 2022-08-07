using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //engine to manage buttons.

public class MenuUIHandler : MonoBehaviour
{
    public bool isGameActive; //bool variable to check if the game is active.

    public GameObject menu;

    public Button restartButton;

    // Start is called before the first frame update
    void Start()
    {
        isGameActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        if (isGameActive == false)
        {
            isGameActive = true;
            menu.gameObject.SetActive(false);
        }
        
    }
}
