using TMPro;
using UnityEngine;

public class UITextReputation : MonoBehaviour
{
    [SerializeField] private MissionGenerator zonePrefab;
    [SerializeField] private TMP_Text reputationText;

    private void Start()
    {
        UpdateReputationText();
    }

    public void UpdateReputationText()
    {
        if (zonePrefab == null)
        {
            Debug.LogWarning("Zone Prefab is not assigned.", this);
            return;
        }

        if (reputationText == null)
        {
            Debug.LogWarning("Reputation Text is not assigned.", this);
            return;
        }

        if (zonePrefab.zone == null)
        {
            Debug.LogWarning("Zone Scriptable Object inside MissionGenerator is null.", this);
            return;
        }

        reputationText.text = "%" + zonePrefab.zone.zoneReputation.ToString();
    }
}