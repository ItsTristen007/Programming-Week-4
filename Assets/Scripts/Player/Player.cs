using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class Player : MonoBehaviour
{
    
    [SerializeField] private float speed;
    private float movementX;
    private float movementY;
    
    private Rigidbody rb;

    public TextMeshProUGUI countText;
    private int count;

    public GameObject winTextObject;
    
    public TextMeshProUGUI timerText;
    private float startTime;

    private bool win = false;
    
    [SerializeField] private AudioSource coinGet;
    [SerializeField] private AudioSource winSound;
    
    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        count = 0;
        startTime = Time.time;
        SetCountText();
        
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 8)
        {
            winTextObject.SetActive(true);
            win = true;
            winSound.Play();
        }
        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        
        rb.AddForce(movement * speed);
        
        if (win == false)
        {
            float t = Time.time - startTime;
            string seconds = (t % 60).ToString("N");
            timerText.text = seconds;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Collectable"))
        {
            other.gameObject.SetActive(false);
            count++;
            coinGet.Play();
            SetCountText();
        }
    }
}
