using UnityEngine;
using UnityEngine.UI;

public class PigUI : MonoBehaviour
{
    [SerializeField] private Image pigImage;
    [SerializeField] private Image pigRankImage;

    private PigRuntime pig;

    public void Initialize(PigRuntime pigRuntime)
    {
        pig = pigRuntime;

        pigImage.sprite = pig.data.GetImage();
        pigRankImage.sprite = RankController.Instance.GetRankSprite(pig.data.GetProfessionalism());
    }

    public PigRuntime GetPig()
    {
        return pig;
    }
}