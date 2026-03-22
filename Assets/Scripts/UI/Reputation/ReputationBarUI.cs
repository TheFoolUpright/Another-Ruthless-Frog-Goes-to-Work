using UnityEngine;

public class ReputationBarUI : MonoBehaviour
{
    [SerializeField] private MissionGenerator[] zones;
    [SerializeField] private RectTransform reputationBackground;
    [SerializeField] private RectTransform reputationFill;
    [SerializeField] private float maxTotalReputation = 400f;
    [SerializeField] private PigInventoryController pigInventoryController;

    [SerializeField] private float[] unlockThresholds = { 240f, 280f, 320f, 360f };

    public float totalReputationPercentage;
    private float totalReputation;
    private int nextUnlockIndex = 0;

    public void Update()
    {
        UpdateReputationBar();
    }

    public void UpdateReputationBar()
    {
        totalReputation = 0f;

        for (int i = 0; i < zones.Length; i++)
        {
            if (zones[i] != null && zones[i].zone != null)
            {
                totalReputation += zones[i].zoneReputation;
            }
        }

        float normalizedValue = totalReputation / maxTotalReputation;
        normalizedValue = Mathf.Clamp01(normalizedValue);

        float backgroundWidth = reputationBackground.rect.width;
        float targetWidth = backgroundWidth * normalizedValue;

        Vector2 newSize = reputationFill.sizeDelta;
        newSize.x = targetWidth;
        reputationFill.sizeDelta = newSize;

        totalReputationPercentage = normalizedValue * 100f;

        CheckPigUnlocks();
    }

    private void CheckPigUnlocks()
    {
        if (pigInventoryController == null)
        {
            return;
        }

        if (nextUnlockIndex >= unlockThresholds.Length)
        {
            return;
        }

        if (totalReputation >= unlockThresholds[nextUnlockIndex])
        {
            pigInventoryController.UnlockNextPig();
            nextUnlockIndex++;
        }
    }
}
