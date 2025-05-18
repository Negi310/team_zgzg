using UnityEngine;
using PieceSystem;
using System.Collections.Generic;

public class PieceFunction : MonoBehaviour //ピースのオブジェクトそれぞれが持つコンポーネント
{
    [Header("ピースの種類"),SerializeField]private PieceInfo pi;
    public IEnumerable<PieceDirection> ValidDirections() //どの方向につけられるかをプレイヤーに返す
    {
        return pi.ValidDirections();
    }

    public PieceInfo PieceInfo() //ピースのデータをプレイヤーに返し消滅
    {
        Destroy(gameObject);
        return pi;
    }
}
