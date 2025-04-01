using UnityEngine;

public class PieceFunction : MonoBehaviour
{
    [Header("ピース番号"),SerializeField]private int pieceId;
    [Header("追加で装着可能か"),SerializeField]private bool canAttach;
    [Header("上で装着可能か"),SerializeField]private bool validUpArrow;
    [Header("下で装着可能か"),SerializeField]private bool validDownArrow;
    [Header("右で装着可能か"),SerializeField]private bool validRightArrow;
    [Header("左で装着可能か"),SerializeField]private bool validLeftArrow;
    public PlayerMovement playerMovement;
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(playerMovement != null)
        {
            playerMovement.attachFlag = true; //プレイヤーが範囲内なら装着を可能に
            if(validUpArrow && Input.GetKeyDown(KeyCode.UpArrow))
            {
                Vector2 validDir = Vector2.up;
                playerMovement.AddPiece(validDir, new PieceData(pieceId, canAttach));
                Destroy(this);
            }
            else if(validDownArrow && Input.GetKeyDown(KeyCode.DownArrow))
            {
                Vector2 validDir = Vector2.down;
                playerMovement.AddPiece(validDir, new PieceData(pieceId, canAttach));
                Destroy(this);
            }
            else if(validRightArrow && Input.GetKeyDown(KeyCode.RightArrow))
            {
                Vector2 validDir = Vector2.right;
                playerMovement.AddPiece(validDir, new PieceData(pieceId, canAttach));
                Destroy(this);
            }
            else if(validLeftArrow && Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Vector2 validDir = Vector2.left;
                playerMovement.AddPiece(validDir, new PieceData(pieceId, canAttach));
                Destroy(this);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(playerMovement != null)
        {
            playerMovement.attachFlag = false;
        }
    }
}
