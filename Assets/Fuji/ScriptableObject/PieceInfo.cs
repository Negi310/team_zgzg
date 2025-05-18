using UnityEngine;
using System.Collections.Generic;
using PieceSystem;

[CreateAssetMenu]
public class PieceInfo : ScriptableObject //ピースそれぞれが持つピースの情報のスクリプタブルオブジェクト（インスタンス化する必要のないデータ）
{
    [Header("ピース番号"),SerializeField]private int pieceId;
    [Header("追加で装着可能か"),SerializeField]private bool canAttach;
    [SerializeField] private List<PieceDirection> validDirections;

    public int PieceId => pieceId;
    public bool CanAttach => canAttach;

    public IEnumerable<PieceDirection> ValidDirections()
    {
        return validDirections;
    }
}
