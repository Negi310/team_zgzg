using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [Header("横歩行速度"),SerializeField]private float nomalSpeed = 5f;
    [Header("横最高速度"),SerializeField]private float accelSpeed = 10f;
    [Header("横加速割合"),SerializeField]private float accelRatio = 2f;
    [Header("タテ初速度"),SerializeField]private float initialSpeed = 25f;
    [Header("タテ第一減速度（ジャンプの最初のギュンってやつ）（長押しにより減少）"),SerializeField]private float strongAccel = 400f; 
    [Header("タテ最高第一減速度"),SerializeField]private float initialStrAcl = 400f;
    [Header("タテ最低第一減速度"),SerializeField]private float minAccel = 50f;
    [Header("第一減速度減少割合"),SerializeField]private float strongAccelRatio = 30f;
    [Header("減速度移行速度"),SerializeField]private float topSpeed = 5f;
    [Header("タテ第二減速度（ジェットコースターのエアタイム的な期間の減速度）"),SerializeField]private float weakAccel = 10f; 
    [Header("重力"),SerializeField]private float gravity = -15f;
    [Header("接地判定"),SerializeField]private bool groundFlag = false;
    [Header("右壁ないか判定"),SerializeField]private bool rightFlag = true;
    [Header("左壁ないか判定"),SerializeField]private bool leftFlag = true;
    [Header("天井ないか判定"),SerializeField]private bool ceilFlag = true;
    [Header("右進行方向"),SerializeField]private Vector2 moveDirR;
    [Header("下からぶつかると天井と認識されるもの"),SerializeField]private LayerMask groundLayer;
    private Rigidbody2D rb;
    private float moveSpeed = 5f; //プレイヤーの横移動速度
    private float vtcSpeed = 0f; //プレイヤーの縦移動速度
    private float dirAngle; //接している床の角度
    private bool jumpFlag = false; //ジャンプ入力検知
    private bool jumpingFlag = false; //ジャンプ中検知
    // Update is called once per frame
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        RayCheck();
        Move();
        jumpFlag = Input.GetKey(KeyCode.Space);
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
        rb.linearVelocity =  new Vector2(0f, 0f);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("FallFloor"))
        {
            DirCheck(collision);
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("FallFloor"))
        {
            vtcSpeed = 0f;
            AngleCheck();
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("FallFloor"))
        {
            moveDirR = transform.right;
            rightFlag = true;
            leftFlag = true;
            groundFlag = false;
        }
    }
    void Move() //横移動（ダッシュ可能）
    {
        if(Input.GetKey(KeyCode.A) && leftFlag)
        {
            if(Input.GetKey(KeyCode.LeftShift) && !jumpingFlag)
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
            if(Input.GetKey(KeyCode.LeftShift) && !jumpingFlag)
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
    void DirCheck(Collision2D collision) //接している地面の角度算出
    {
        var normal = collision.contacts[0].normal;
        Vector2 dir = normal.normalized;
        moveDirR = Quaternion.Euler(0f,0f,-90f) * new Vector3(dir.x, dir.y, 0f);
        moveDirR = new Vector3(moveDirR.x, moveDirR.y, 0f);
        dirAngle = Mathf.Atan2(moveDirR.y, moveDirR.x) * Mathf.Rad2Deg;
    }
    void AngleCheck() //地面の角度による振る舞い分け
    {
        if(60f < dirAngle && dirAngle <= 90f)
        {
            moveDirR = new Vector3(1f, 0f, 0f);
            if(!jumpingFlag)
            {
                rightFlag = false;
                groundFlag = true;
            }
        }
        else if(90f < dirAngle)
        {
            moveDirR = new Vector3(1f, 0f, 0f);
        }
        else if(-90f <= dirAngle && dirAngle < -60f)
        {
            moveDirR = new Vector3(1f, 0f, 0f);
            if(!jumpingFlag)
            {
                leftFlag = false;
                groundFlag = true;
            }
        }
        else if(dirAngle < -90f)
        {
            moveDirR = new Vector3(1f, 0f, 0f);
        }
        else
        {
            groundFlag = true;
        }
    }
    void RayCheck() //天井検知
    {
        Vector2 position = transform.position;
        Vector2 v = new Vector2(-0.499f, 0.65f);
        RaycastHit2D hitCeil = Physics2D.Raycast(position + v, Vector2.right, 0.998f, groundLayer);
        ceilFlag = (hitCeil.collider == null);
    }
    void StartJump() //ジャンプ開始時の初期化処理
    {
        jumpingFlag = true;
        rightFlag = true;
        leftFlag = true;
        moveDirR = new Vector3(1f, 0f, 0f);
        dirAngle = Mathf.Atan2(0f, 1f) * Mathf.Rad2Deg;
        vtcSpeed = initialSpeed;
    }
    void Jump() //最初ギュンってなってフワッてなって落ちるやつ
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
    void PushJump() //長押ししてる間に次第にギュン弱体化
    {
        strongAccel -= strongAccelRatio * Mathf.Pow(2,Time.deltaTime);
        strongAccel = Mathf.Max(strongAccel,minAccel);
    }
    void Fall() //落下
    {
        vtcSpeed += gravity * Time.deltaTime;
    }
}