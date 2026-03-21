using UnityEngine;

public class RankController : MonoBehaviour
{
    public static RankController Instance;

    [SerializeField] private Sprite[] rankSprites;

    private void Awake()
    {
        Instance = this;
    }

    public Sprite GetRankSprite(int professionalism)
    {
        int index = Mathf.Clamp(professionalism, 0, rankSprites.Length - 1);
        return rankSprites[index];
    }
}
