using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartItem : MonoBehaviour
{
    [Header("加算するスコア")] public int plusHeart;
    [Header("プレイヤーの判定")] public PlayerTriggerCheck playerCheck;

　　　　// Update is called once per frame
    void Update()
    {
　　　　　　//プレイヤーが判定内に入ったら
        if(playerCheck.isOn)
        {
            if(GManager.instance != null)
            {
                if(GManager.instance.heartNum <= 2)
                {
                    GManager.instance.heartNum += plusHeart;
                    Destroy(this.gameObject);
                }
                else
                {
                    Destroy(this.gameObject);                    
                }
            }
        }
    }
}