using UnityEngine;
using UnityEngine.UI;

public class HeartDisplay : MonoBehaviour
{
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    public Image[] heartImages; // UI上に配置したハートを順に割り当て

    private GManager gManager;
    private int lastHeartNum = -1;

    void Start()
    {
        // シーン内の GManager を探す（1つだけ存在する前提）
        gManager = FindAnyObjectByType<GManager>();

    }

    void Update()
    {
        if (gManager != null && gManager.heartNum != lastHeartNum)//GManagerが存在かつheartNumに変更があったら
        {
            UpdateHearts(gManager.heartNum);
            lastHeartNum = gManager.heartNum;
        }
    }

    void UpdateHearts(int heartNum)
    {
        int tempHeart = heartNum;

        for (int i = 0; i < heartImages.Length; i++)
        {
            if (tempHeart >= 2)
            {
                heartImages[i].sprite = fullHeart;
                tempHeart -= 2;
            }
            else if (tempHeart == 1)
            {
                heartImages[i].sprite = halfHeart;
                tempHeart -= 1;
            }
            else
            {
                heartImages[i].sprite = emptyHeart;
            }
        }
    }
}
