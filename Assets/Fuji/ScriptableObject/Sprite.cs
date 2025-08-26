using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class SpriteDatabase : ScriptableObject
{
    [SerializeField] private List<PieceSprite> sprites;

    private Dictionary<int, Sprite> recipient = new();

    private void Awake()
    {
        foreach (var sprite in sprites)
        {
            recipient[sprite.id] = sprite.sprite;
        }
    }

    public Sprite SpriteFromId(int id)
    {
        return recipient.TryGetValue(id, out var sprite) ? sprite : null;
    }
}
