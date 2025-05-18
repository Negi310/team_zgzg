using UnityEngine;
using System.Collections.Generic;
using PieceSystem;

public class PlayerPieceAttach : MonoBehaviour
{
    private KeyCode attachKey;
    private Dictionary<PieceDirection, bool> attachable = new() //追加装着可能か
    {
        { PieceDirection.Up, true },
        { PieceDirection.Down, true },
        { PieceDirection.Left, true },
        { PieceDirection.Right, true },
    };
    private Dictionary<PieceDirection, List<PieceData>> attachedPieces = new() //装着ピース
    {
        { PieceDirection.Up, new() },
        { PieceDirection.Down, new() },
        { PieceDirection.Left, new() },
        { PieceDirection.Right, new() },
    };
    private List<PieceFunction> nearbyPieces = new(); // 接触中のピース
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Update()
    {
        if (Input.GetKeyDown(attachKey))
        {
            TryAttachNearest(PieceDirFromKey.ToDir(attachKey));
        }
    }

    private void TryAttachNearest(PieceDirection inputDir)
    {
        foreach (var piece in nearbyPieces)
        {
            // 有効な方向を順に確認
            foreach (var validDir in piece.ValidDirections())
            {
                if (attachable[validDir] && inputDir == validDir)
                {
                    AddPiece(inputDir, piece.PieceInfo());
                    return; // 1個のみ装着
                }
            }
        }
        DetachPiece(inputDir);
    }

    public void AddPiece(PieceDirection direction, PieceInfo piece) //ピース装着
    {
        var data = new PieceData(piece.PieceId, piece.CanAttach);
        attachedPieces[direction].Add(data);
        attachable[direction] = piece.CanAttach;
    }
    private void DetachPiece(PieceDirection inputDir) //ピース外す
    {
        if (attachedPieces.TryGetValue(inputDir, out var targetList) && targetList.Count > 0)
        {
            PieceData detachedPiece = targetList[targetList.Count - 1];
            targetList.RemoveAt(targetList.Count - 1);
            attachable[inputDir] = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PieceFunction>(out var piece))
        {
            if (!nearbyPieces.Contains(piece))
            {
                nearbyPieces.Add(piece);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<PieceFunction>(out var piece))
        {
            nearbyPieces.Remove(piece);
        }
    }

    
    int FindPieceStatus(List<PieceData> pieceList, int targetId)
    {
        if (pieceList.Count == 0)
        {   
            return 0;
        }
        for (int i = 0; i < pieceList.Count; i++)
        {
            if (pieceList[i].id == targetId)
            {
                if (i == pieceList.Count - 1)
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
            }
        }
        return 0;
    }
    public void UpdatePieceVisibility(List<PieceData> pieceList, List<GameObject> objList)
    {
        // すべてのピースオブジェクトを非表示にする
        foreach (GameObject obj in objList)
        {
            obj.SetActive(false);
        }

        // 最大3つまで表示する
        int maxDisplay = Mathf.Min(3, pieceList.Count);

        for (int i = 0; i < maxDisplay; i++)
        {
            objList[pieceList[i].id].SetActive(true);
        }
    }
}
