using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_for_test : MonoBehaviour
{
   //インスペクターで設定する
   public float speed;
   public GroundCheck ground;
   public GManager gManager;
   public float jumpPower = 5f;
   public float damageInterval = 1.0f; // 何秒ごとにダメージを与えるか

   private Rigidbody2D rb = null;
   private bool isGround = false;
   private Vector3 respawnPoint;
   private bool isDamaging = false;
   private Coroutine damageCoroutine;

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

   void Update()
   {
      if (Input.GetKeyDown(KeyCode.Space) && ground.IsGround())
      {
         rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
         rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
      }
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

   private void OnTriggerEnter2D(Collider2D collision)
   {
      if (collision.CompareTag("Thorn"))//トゲとぶつかったら
      {
         TakeDamage(1);
         Respawn();
      }

      if (collision.CompareTag("CheckPoint"))//"CheckPoint"タグのオブジェクトに触れたら
      {
         respawnPoint = collision.transform.position;
      }

      if (collision.CompareTag("Poison"))
      {
         if (!isDamaging)
         {
            isDamaging = true;
            damageCoroutine = StartCoroutine(DamageOverTime(collision.gameObject));
         }
      }
   }

   void OnTriggerExit2D(Collider2D collision)
   {
      if (collision.CompareTag("Poison"))
      {
         if (isDamaging)
         {
            isDamaging = false;
            StopCoroutine(damageCoroutine);
         }
      }
   }
   
   private IEnumerator DamageOverTime(GameObject player)
   {
      while (isDamaging)
      {
         TakeDamage(1);
         yield return new WaitForSeconds(damageInterval);
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
    
   void Respawn()
   {
      if (respawnPoint != null)
      {
         transform.position = respawnPoint;
      }
   }

    
}
