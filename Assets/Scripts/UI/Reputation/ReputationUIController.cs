using TMPro;
using UnityEngine;

public class ReputationUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text[] reputationTexts;

    private bool isVisible;

    private void Start()
    {
        isVisible = false;
        SetTextsVisible(false);
    }

    public void ToggleReputationTexts()
    {
        isVisible = !isVisible;
        SetTextsVisible(isVisible);
    }

    private void SetTextsVisible(bool visible)
    {
        if (reputationTexts == null)
        {
            return;
        }

        for (int i = 0; i < reputationTexts.Length; i++)
        {
            if (reputationTexts[i] != null)
            {
                reputationTexts[i].gameObject.SetActive(visible);
            }
        }
    }
}
