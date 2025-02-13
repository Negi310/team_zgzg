using UnityEngine;

public class PieceFunction : MonoBehaviour
{
    public string pieceName;
    public bool canAttach;
    public bool validUpArrow;
    public bool validDownArrow;
    public bool validRightArrow;
    public bool validLeftArrow;
    public PlayerMovement playerMovement;
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(playerMovement != null)
        {
            playerMovement.attachFlag = true;
            if(validUpArrow && Input.GetKeyDown(KeyCode.UpArrow))
            {
                Vector2 validDir = Vector2.up;
                playerMovement.AddPiece(validDir, new PieceData(pieceName, canAttach));
                Destroy(gameObject);
            }
            else if(validDownArrow && Input.GetKeyDown(KeyCode.DownArrow))
            {
                Vector2 validDir = Vector2.down;
                playerMovement.AddPiece(validDir, new PieceData(pieceName, canAttach));
                Destroy(gameObject);
            }
            else if(validRightArrow && Input.GetKeyDown(KeyCode.RightArrow))
            {
                Vector2 validDir = Vector2.right;
                playerMovement.AddPiece(validDir, new PieceData(pieceName, canAttach));
                Destroy(gameObject);
            }
            else if(validLeftArrow && Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Vector2 validDir = Vector2.left;
                playerMovement.AddPiece(validDir, new PieceData(pieceName, canAttach));
                Destroy(gameObject);
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
