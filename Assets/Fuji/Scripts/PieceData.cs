using UnityEngine;

public struct PieceData
{
    public int id;
    public bool canAttach;
    public PieceData(int id, bool canAttach)
    {
        this.id = id;
        this.canAttach = canAttach;
    }
}
