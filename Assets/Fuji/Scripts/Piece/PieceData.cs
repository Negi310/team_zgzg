using UnityEngine;

public struct PieceData //プレイヤーが格納する、ピースの持つ情報を表す構造体
{
    public int id;
    public bool canAttach;
    public PieceData(int id, bool canAttach)
    {
        this.id = id;
        this.canAttach = canAttach;
    }
}
