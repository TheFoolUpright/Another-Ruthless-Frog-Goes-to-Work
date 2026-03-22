using UnityEngine;

public class ReputationBarUI : MonoBehaviour
{
    [SerializeField] private MissionGenerator[] zones;
    [SerializeField] private RectTransform reputationBackground;
    [SerializeField] private RectTransform reputationFill;
    [SerializeField] private float maxTotalReputation = 400f;
    public float totalReputationPercentage;

    public void Update()
    {
        UpdateReputationBar();
    }

    public void UpdateReputationBar()
    {
        float totalReputation = 0f;

        for (int i = 0; i < zones.Length; i++)
        {
            if (zones[i] != null && zones[i].zone != null)
            {
                totalReputation += zones[i].zone.zoneReputation;
            }
        }

        float normalizedValue = totalReputation / maxTotalReputation;
        normalizedValue = Mathf.Clamp01(normalizedValue);

        float backgroundWidth = reputationBackground.rect.width;
        float targetWidth = backgroundWidth * normalizedValue;

        Vector2 newSize = reputationFill.sizeDelta;
        newSize.x = targetWidth;
        reputationFill.sizeDelta = newSize;

        totalReputationPercentage = normalizedValue * 100;
    }
}
