using UnityEngine;

public class PieceFunction : MonoBehaviour
{
    [Header("ピースの種類"),SerializeField]private PieceInfo pi;
    private PlayerPieceAttach playerPieceAttach;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerPieceAttach = collision.gameObject.GetComponent<PlayerPieceAttach>();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //Attach(Vector2.up, KeyCode.UpArrow, pi.ValidUpArrow);
            //Attach(Vector2.down, KeyCode.DownArrow, pi.ValidDownArrow);
            //Attach(Vector2.right, KeyCode.RightArrow, pi.ValidRightArrow);
            //Attach(Vector2.left, KeyCode.LeftArrow, pi.ValidLeftArrow);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerPieceAttach.attachFlag = false;
        }
    }
    public void Attach(Vector2 validDir, KeyCode key, bool validArrow)
    {
        if(validArrow && Input.GetKeyDown(key))
        {
            playerPieceAttach.attachFlag = true; //プレイヤーが範囲内なら装着を可能に
            playerPieceAttach.AddPiece(validDir, new PieceData(pi.PieceId, pi.CanAttach));
            Destroy(this);
        }
    }
}
