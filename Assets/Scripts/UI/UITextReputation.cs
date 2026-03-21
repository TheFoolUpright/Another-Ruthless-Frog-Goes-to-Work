using TMPro;
using UnityEngine;

public class UITextReputation : MonoBehaviour
{
    [SerializeField] private ZoneScriptableObject zoneData;
    [SerializeField] private TMP_Text reputationText;

    private void Start()
    {
        UpdateReputationText();
    }

    public void UpdateReputationText()
    {
        if (zoneData == null)
        {
            Debug.LogWarning("ZoneData is not assigned.", this);
            return;
        }

        if (reputationText == null)
        {
            Debug.LogWarning("ReputationText is not assigned.", this);
            return;
        }

        reputationText.text = "%" + zoneData.zoneReputation.ToString();
    }
}