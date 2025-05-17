using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class PlayerPieceAttach : MonoBehaviour
{
    public bool attachFlag = false;
    private bool attachUp = true; //ピース上付けられる
    private bool attachDown = true; //ピース下付けられる
    private bool attachRight = true; //ピース右付けられる
    private bool attachLeft = true; //ピース左付けられる
    private List<PieceData> upPieces = new List<PieceData>(); //上側についてるピース
    private List<PieceData> downPieces = new List<PieceData>(); //下側についてるピース
    private List<PieceData> rightPieces = new List<PieceData>(); //右側についてるピース
    private List<PieceData> leftPieces = new List<PieceData>(); //左側についてるピース
    private List<GameObject> upPieceObjects;
    private List<GameObject> downPieceObjects;
    private List<GameObject> rightPieceObjects;
    private List<GameObject> leftPieceObjects;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void AddPiece(Vector2 direction, PieceData piece) //ピースの近くにいるときでのピースの情報受理
    {
        if(direction == Vector2.up && attachUp)
        {
            upPieces.Add(piece);
            attachUp = piece.canAttach;
        }
        else if(direction == Vector2.down && attachDown)
        {
            downPieces.Add(piece);
            attachDown = piece.canAttach;
        }
        else if(direction == Vector2.right && attachRight)
        {
            rightPieces.Add(piece);
            attachRight = piece.canAttach;
        }
        else if(direction == Vector2.left && attachLeft)
        {
            leftPieces.Add(piece);
            attachLeft = piece.canAttach;
        }
    }
    void InputArrow()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) && !attachFlag)
        {
            DetachPiece(upPieces);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow) && !attachFlag)
        {
            DetachPiece(downPieces);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow) && !attachFlag)
        {
            DetachPiece(rightPieces);
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow) && !attachFlag)
        {
            DetachPiece(leftPieces);
        }
    }
    void DetachPiece(List<PieceData> targetList) //ピース外す
    {
        if(targetList != null && targetList.Count > 0)
        {
            PieceData detachedPiece = targetList[targetList.Count - 1];
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
