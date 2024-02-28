using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class Player : MonoBehaviour
{
    //Sets Movement 
    [SerializeField] private float speed;
    private float movementX;
    private float movementY;
    [SerializeField] private float jumpForce;

    //checks to see if the player is grounded 
    private bool isGrounded;
    private Rigidbody rb;

    //collectables 
    public TextMeshProUGUI countText;
    private int count;

    //UI 
    public GameObject winTextObject;
    public GameObject mainMenuButton;
    public GameObject playAgainButton;
    
    //Timer
    public TextMeshProUGUI timerText;
    private float startTime;

    //Win state 
    private bool win = false;
    
    //Audio 
    [SerializeField] private AudioSource coinGet;
    [SerializeField] private AudioSource winSound;
    
    // Start is called before the first frame update
    void Start()
    {
        //Makes sure that player cant see these until they win 
        winTextObject.SetActive(false);
        mainMenuButton.SetActive(false);
        playAgainButton.SetActive(false);
        
        
        rb = GetComponent<Rigidbody>();
        
        // Sets the coin count to 0 
        count = 0;
        
        // Starts the timer 
        startTime = Time.time;
        
        //Calls the count text method 
        SetCountText();
        

    }

    void OnMove(InputValue movementValue)
    {
        //Moves the player when they input a direction 
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    public void jump()
    {
        
        if (isGrounded)
        {
            //if player is grounded adds force to the rigidbody instantly 
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        
        
    }
    
    private void checkGround()
    {
        //checks to see if the object is touching the ground by drawing a invisible line bellow the ball at all times 
        isGrounded = Physics.Raycast(transform.position, Vector3.down, GetComponent<Collider>().bounds.size.y);
        Debug.DrawRay(transform.position,Vector3.down * GetComponent<Collider>().bounds.size.y, Color.green,0,false);
        
    }
    
    void SetCountText()
    {
        
        //Sets the coin count to be equal to the amount the player has gotten 
        countText.text = "Count: " + count.ToString();
        
        // if they got all the coins display the win text sounds and buttons 
        if (count >= 15)
        {
            //shows all the buttons 
            playAgainButton.SetActive(true);
            winTextObject.SetActive(true);
            mainMenuButton.SetActive(true);
            
            //sets win status to true and plays the win sound  
            win = true;
            winSound.Play();
            

        }
        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Adds force to the player to move it when they are holding a direction 
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        
        rb.AddForce(movement * speed);
        
        //if the player hasn't won keep the stopwatch going 
        if (win == false)
        {
            
            //changes the time from a float to a string and displays it 
            float t = Time.time - startTime;
            string seconds = (t % 60).ToString("N");
            timerText.text = seconds;
        }
        
    }

    
    private void Update()
    {
        //if the player presses the jump button 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            checkGround();
            if (isGrounded)
            {
                jump();    
            }
            
        }
    }


    private void OnTriggerEnter(Collider other)
    {

        //if the player collides with a collectable do this 
        if (other.gameObject.CompareTag("Collectable"))
        {
            //Add 1 to the count, destroy the object, play the sound effect and update the text  
            other.gameObject.SetActive(false);
            count++;
            coinGet.Play();
            SetCountText();
        }

    }
}
