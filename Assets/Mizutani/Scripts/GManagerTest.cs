using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 public class GManagerTest : MonoBehaviour
 {
     public static GManagerTest instance = null;
     public int score;
     public int stageNum;
     public int continueNum;

     private void Awake()
     {
          if (instance == null)
          {
              instance = this;
              DontDestroyOnLoad(this.gameObject);
          }
          else
          {
              Destroy(this.gameObject);
          }
     }
 }