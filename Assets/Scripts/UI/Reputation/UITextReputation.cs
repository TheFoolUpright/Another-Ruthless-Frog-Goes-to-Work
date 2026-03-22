using TMPro;
using UnityEngine;

public class UITextReputation : MonoBehaviour
{
    [SerializeField] private MissionGenerator zonePrefab;
    [SerializeField] private TMP_Text reputationText;

    private void Update()
    {
        UpdateReputationText();
    }

    public void UpdateReputationText()
    {
        reputationText.text = zonePrefab.zone.zoneReputation.ToString() + "%";
    }
}