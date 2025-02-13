using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float nomalSpeed = 5f;
    public float accelSpeed = 10f;
    public float accelRatio = 2f;
    public float vtcSpeed = 0f;
    public bool jumpFlag = false;
    public bool jumpingFlag = false;
    public bool groundFlag = false;
    public float rightLength = 0.1f;
    public float leftLength = 0.1f;
    public float ceilLength = 0.1f;
    public LayerMask groundLayer;
    public bool rightFlag = true;
    public bool leftFlag = true;
    public bool ceilFlag = true;
    public float rightRatio = 0.5f;
    public float leftRatio = 0.5f;
    public float ceilRatio = 0.5f;
    public float strongAccel = 400f; 
    public float weakAccel = 10f; 
    public float gravity = -15f;
    public float topSpeed = 5f;
    public float minAccel = 50f;
    public float strongAccelRatio = 30f;
    public float initialSpeed = 25f;
    public float initialStrAcl = 400f;
    private Vector2 moveDirR;
    private bool attachUp = true;
    private bool attachDown = true;
    private bool attachRight = true;
    private bool attachLeft = true;
    private List<PieceData> upPieces = new List<PieceData>();
    private List<PieceData> downPieces = new List<PieceData>();
    private List<PieceData> rightPieces = new List<PieceData>();
    private List<PieceData> leftPieces = new List<PieceData>();
    public bool attachFlag = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        jumpFlag = Input.GetKey(KeyCode.Space);
        RayCheck();
        if(ceilFlag || !jumpingFlag)
        {
            transform.position += transform.up * vtcSpeed * Time.deltaTime * 0.5f;
        }
        if(jumpFlag)
        {
            PushJump();
            if(groundFlag)
            {
                StartJump();
            }
        }
        else
        {
            strongAccel = initialStrAcl;
        }
        if(jumpingFlag)
        {
            Jump();
        }
        else if(!groundFlag)
        {
            strongAccel = initialStrAcl;
            Fall();
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            groundFlag = true;
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            vtcSpeed = 0f;
            groundFlag = true;
            var normal = collision.contacts[0].normal;
            Vector2 dir = normal.normalized;
            moveDirR = Quaternion.Euler(0f,0f,-90f) * new Vector3(dir.x, dir.y, 0f);
            moveDirR = new Vector3(moveDirR.x, moveDirR.y, 0f);
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            moveDirR = transform.right;
            groundFlag = false;
        }
    }
    void Move()
    {
        if(Input.GetKey(KeyCode.A) && leftFlag)
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                moveSpeed += accelRatio * Time.deltaTime;
            }
            transform.position -= (Vector3)moveDirR * moveSpeed * Time.deltaTime;
        }
        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = nomalSpeed;
        }
        if(Input.GetKey(KeyCode.D) && rightFlag)
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                moveSpeed += accelRatio * Time.deltaTime;
            }
            transform.position += (Vector3)moveDirR * moveSpeed * Time.deltaTime;
        }
        if(moveSpeed > accelSpeed)
        {
            moveSpeed = accelSpeed;
        }
    }
    void RayCheck()
    {
        Vector2 position = transform.position;
        RaycastHit2D hitRight = Physics2D.Raycast(position + Vector2.right * rightRatio, Vector2.right, rightLength, groundLayer);
        RaycastHit2D hitLeft = Physics2D.Raycast(position + Vector2.left * leftRatio, Vector2.left, leftLength, groundLayer);
        RaycastHit2D hitCeil = Physics2D.Raycast(position + Vector2.up * ceilRatio, Vector2.up, ceilLength, groundLayer);
        rightFlag = (hitRight.collider == null);
        leftFlag = (hitLeft.collider == null);
        ceilFlag = (hitCeil.collider == null);
    }
    void StartJump()
    {
        jumpingFlag = true;
        vtcSpeed = initialSpeed;
    }
    void Jump()
    {
        if(vtcSpeed > topSpeed)
        {
            vtcSpeed -= strongAccel * Time.deltaTime;
        }
        else
        {
            vtcSpeed -= weakAccel * Time.deltaTime;
        }
        if(vtcSpeed < 0)
        {
            jumpingFlag = false;
        }
    }
    void PushJump()
    {
        strongAccel -= strongAccelRatio * Mathf.Pow(2,Time.deltaTime);
        strongAccel = Mathf.Max(strongAccel,minAccel);
    }
    void Fall()
    {
        vtcSpeed += gravity * Time.deltaTime;
    }
    public void AddPiece(Vector2 direction, PieceData piece)
    {
        if(direction == Vector2.up && attachUp)
        {
            upPieces.Add(piece);
            attachUp = piece.canAttach;
        }
        else if(direction == Vector2.down && attachDown)
        {
            downPieces.Add(piece);
            attachDown = piece.canAttach;
        }
        else if(direction == Vector2.right && attachRight)
        {
            rightPieces.Add(piece);
            attachRight = piece.canAttach;
        }
        else if(direction == Vector2.left && attachLeft)
        {
            leftPieces.Add(piece);
            attachLeft = piece.canAttach;
        }
    }
    void InputArrow()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) && !attachFlag)
        {
            DetachPiece(upPieces);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow) && !attachFlag)
        {
            DetachPiece(downPieces);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow) && !attachFlag)
        {
            DetachPiece(rightPieces);
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow) && !attachFlag)
        {
            DetachPiece(leftPieces);
        }
    }
    void DetachPiece(List<PieceData> targetList)
    {
        if(targetList != null && targetList.Count > 0)
        {
            PieceData detachedPiece = targetList[targetList.Count - 1];
        }
    }
}