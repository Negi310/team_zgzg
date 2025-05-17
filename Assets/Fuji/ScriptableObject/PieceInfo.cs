using UnityEngine;
using System.Collections.Generic;
using PieceSystem;

[CreateAssetMenu]
public class PieceInfo : ScriptableObject
{
    [Header("ピース番号"),SerializeField]private int pieceId;
    [Header("追加で装着可能か"),SerializeField]private bool canAttach;
    [SerializeField] private List<DirectionFlag> validDirections;

    public int PieceId => pieceId;
    public bool CanAttach => canAttach;

    public Dictionary<PieceDirection, bool> ValidDirections()
    {
        var dict = new Dictionary<PieceDirection, bool>();
        foreach(var flag in validDirections)
        {
            dict[flag.direction] = flag.isValid;
        }
        return dict;
    }
}
