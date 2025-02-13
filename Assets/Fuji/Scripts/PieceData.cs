using UnityEngine;

public struct PieceData
{
    public string name;
    public bool canAttach;
    public PieceData(string name, bool canAttach)
    {
        this.name = name;
        this.canAttach = canAttach;
    }
}
