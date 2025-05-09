using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitPoint : MonoBehaviour
{
   private Text HitPointText = null;
   private int oldHitPoint = 0;

  // Start is called before the first frame update
  void Start()
  {
      HitPointText = GetComponent<Text>();
      if(GManager.instance != null)
      {
             HitPointText.text = "HP" + GManager.instance.HitPoint;
      }
      else
      {
            Debug.Log("ゲームマネージャー置き忘れてるよ！");
            Destroy(this);
      }
  }

   // Update is called once per frame
   void Update()
   {
       if(oldHitPoint != GManager.instance.HitPoint)
       {
            HitPointText.text = "HP " + GManager.instance.HitPoint;
            oldHitPoint = GManager.instance.HitPoint;
       }
   }
}   