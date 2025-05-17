using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn : MonoBehaviour
{
    [Header("加算するスコア")] public int HP;
    [Header("プレイヤーの判定")] public PlayerTriggerCheck playerCheck;

　　　　// Update is called once per frame
    void Update()
    {
　　　　　　//プレイヤーが判定内に入ったら
        if(playerCheck.isOn)
        {
            if(GManager.instance != null)
            {
            GManager.instance.HitPoint += HP;
            }
        }
    }
}