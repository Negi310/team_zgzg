using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartItem : MonoBehaviour
{
    [Header("加算するスコア")] public int plusHeart = 2;
    [Header("プレイヤーの判定")] public PlayerTriggerCheck playerCheck;

　　　　// Update is called once per frame
    void Update()
    {
　　　　　　//プレイヤーが判定内に入ったら
        if(playerCheck.isOn)
        {
            if(GManager.instance != null)
            {
                if(GManager.instance.heartNum <= 4)
                {
                    GManager.instance.heartNum += plusHeart;
                    Destroy(this.gameObject);
                }
                else if(GManager.instance.heartNum <= 5)
                {
                    GManager.instance.heartNum += 1;
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