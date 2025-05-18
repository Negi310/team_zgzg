using UnityEngine;
using PieceSystem;

public static class PieceDirFromKey //方向キー=>方向
{
    public static PieceDirection ToDir(KeyCode key)
    {
        if (key == KeyCode.UpArrow) return PieceDirection.Up;
        else if (key == KeyCode.DownArrow) return PieceDirection.Down;
        else if (key == KeyCode.LeftArrow) return PieceDirection.Left;
        else if (key == KeyCode.RightArrow) return PieceDirection.Right;
        return PieceDirection.Null;
    }
}