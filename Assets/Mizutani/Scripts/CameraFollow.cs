using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;   // 追従対象
    public float smoothSpeed = 0.125f; // カメラの追従スピード
    public Vector3 offset;     // プレイヤーからのずれ

    void LateUpdate()//Update関数の後
    {
        if (target == null) return;

        // プレイヤー位置 + オフセット
        Vector3 desiredPosition = target.position + offset;

        // スムーズに補間
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}
