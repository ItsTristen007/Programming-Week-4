using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    //gets the player and offset 
    public GameObject player;
    private Vector3 offset; 
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        //makes the sets the offset from the player 
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // moves the camera based of the player and offset 
        transform.position = player.transform.position + offset;
    }
}
