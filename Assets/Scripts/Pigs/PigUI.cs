using UnityEngine;
using UnityEngine.UI;

public class PigUI : MonoBehaviour
{
    [SerializeField] private Image pigImage;
    [SerializeField] private Image pigRankImage;
    [SerializeField] private CanvasGroup canvasGroup;

    private PigRuntime pig;

    public void Initialize(PigRuntime pigRuntime)
    {
        pig = pigRuntime;

        pigImage.sprite = pig.data.GetImage();
        pigRankImage.sprite = RankController.Instance.GetRankSprite(pig.data.GetProfessionalism());

        RefreshVisualState();
    }

    public PigRuntime GetPig()
    {
        return pig;
    }

    public void RefreshVisualState()
    {
        bool available = pig != null && !pig.isDispatched;

        if (canvasGroup != null)
        {
            canvasGroup.alpha = available ? 1f : 0.45f;
        }

        PigDragHandler dragHandler = GetComponent<PigDragHandler>();
        if (dragHandler != null)
        {
            dragHandler.SetDraggable(available);
        }
    }
}