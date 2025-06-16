using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_for_test : MonoBehaviour
{
   //インスペクターで設定する
   public float speed;
   public GroundCheck ground;
   public Transform respawnPoint;
   public GManager gManager;

   private Rigidbody2D rb = null;
   private bool isGround = false;

   //private string thornTag = "Thorn";

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {
      rb = GetComponent<Rigidbody2D>();
   }

   void FixedUpdate()
   {
      //接地判定を得る
      isGround = ground.IsGround();

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


   /*
   #region//接触判定
   private void OnCollisionEnter2D(Collision2D collision)
   {
      if (collision.collider.tag == thornTag)
      {
         Debug.Log("トゲと接触した！");
      }
   }
   #endregion
   */

   private void OnTriggerEnter2D(Collider2D collision)//トゲとぶつかったら
   {
      if (collision.CompareTag("Thorn"))
      {
         TakeDamage(1);
         Respawn();
      }
   }

   void TakeDamage(int amount)//ダメージを受ける処理
   {
      if (gManager != null)
      {
         gManager.heartNum -= amount;
         gManager.heartNum = Mathf.Max(0, gManager.heartNum);//heartNumが0を下回らないように
      }
   }
    
   void Respawn()//1点にしか戻れないから改良して
   {
      if (respawnPoint != null)
      {
         transform.position = respawnPoint.position;
      }
   }

    
}
