using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0,-Time.deltaTime * speed,0);
        if(transform.position.y >= -6.0f)
        {
            Destroy(this);
        }
        //TODO if select % if right/wrong
    }
}
