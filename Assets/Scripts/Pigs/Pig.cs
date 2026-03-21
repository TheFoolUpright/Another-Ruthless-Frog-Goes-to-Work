using UnityEngine;
using UnityEngine.UIElements;

public class Pig : MonoBehaviour
{
    public PigsScriptableObject data;
    public float currentStamina;

    [SerializeField] private Image pigImage;
    [SerializeField] private Image pigRankImage;

    public void Initialize(PigsScriptableObject pigData)
    {
        data = pigData;
        currentStamina = pigData.GetStamina();

        // TODO: Setup visuals here
    }

    private void UpdateVisuals()
    {
        // Set pig portrait
        pigImage.sprite = data.GetImage();

        // Set rank image
        pigRankImage.sprite = RankController.Instance.GetRankSprite(data.GetProfessionalism());
    }
}
