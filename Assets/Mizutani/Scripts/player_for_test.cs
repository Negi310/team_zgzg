using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_for_test : MonoBehaviour
{
    public float speed; //インスペクターで設定する
    private Rigidbody2D rb = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalKey = Input.GetAxis("Horizontal"); 
        float xSpeed = 0.0f; 
         if (horizontalKey > 0) 
         {
            transform.localScale = new Vector3(1, 1, 1);
            xSpeed = speed;
         }
         else if (horizontalKey < 0) 
         {
            transform.localScale = new Vector3(-1, 1, 1);
            xSpeed = -speed;
         }
         else
         {
            xSpeed = 0.0f;
         }
         rb.linearVelocity = new Vector2(xSpeed, rb.linearVelocity.y);
    }
}
