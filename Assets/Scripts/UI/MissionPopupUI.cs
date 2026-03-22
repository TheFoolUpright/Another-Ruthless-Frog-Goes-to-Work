using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionPopupUI : MonoBehaviour
{
    [SerializeField] private GameObject popupRoot;
    [SerializeField] private TMP_Text missionTitleText;
    [SerializeField] private TMP_Text missionDescriptionText;
    [SerializeField] private Image missionImage;

    [SerializeField] private Image[] starImages;
    [SerializeField] private Image[] assignedStarImages;

    [SerializeField] private Color activeColor = Color.yellow;
    [SerializeField] private Color inactiveColor = Color.white;
    [SerializeField] private Color assignedColor = Color.white;

    [SerializeField] private Sprite filledStarSprite;
    [SerializeField] private Sprite emptyStarSprite;

    [SerializeField] private PigSlot[] missionSlots;
    [SerializeField] private MissionInfo debugMission;

    [SerializeField] private PigWorldDispatcher pigWorldDispatcher;

    private MissionInfo currentMission;

    private void Start()
    {
        if (debugMission != null)
        {
            Open(debugMission);
        }
    }

    public void Open(MissionInfo mission)
    {
        currentMission = mission;
        popupRoot.SetActive(true);

        missionTitleText.text = mission.missionsScriptableObject.missionTitle;
        missionDescriptionText.text = mission.missionsScriptableObject.missionDescription;
        missionImage.sprite = mission.missionsScriptableObject.missionImage;

        int requiredProfessionalism = Mathf.RoundToInt(
            mission.missionsScriptableObject.missionProfessionalismRequirement
        );

        UpdateRequirementStars(requiredProfessionalism);
        UpdateAssignedStars(GetAssignedProfessionalism());

        for (int i = 0; i < missionSlots.Length; i++)
        {
            missionSlots[i].slotType = PigSlot.SlotType.Mission;
            missionSlots[i].linkedMission = currentMission;
        }
    }

    public void Close()
    {
        popupRoot.SetActive(false);
        currentMission = null;
    }

    public void RefreshStars()
    {
        if (currentMission == null) return;

        int requiredProfessionalism = Mathf.RoundToInt(
            currentMission.missionsScriptableObject.missionProfessionalismRequirement
        );

        UpdateRequirementStars(requiredProfessionalism);
        UpdateAssignedStars(GetAssignedProfessionalism());
    }

    public void OnDispatchPressed()
    {
        if (currentMission == null)
            return;

        if (currentMission.assignedPigs == null || currentMission.assignedPigs.Count == 0)
        {
            Debug.Log("No pigs assigned to dispatch.");
            return;
        }

        if (pigWorldDispatcher == null)
        {
            Debug.LogError("PigWorldDispatcher reference is missing on MissionPopupUI.");
            return;
        }

        pigWorldDispatcher.DispatchMission(currentMission);
        ReturnAssignedPigVisualsToInventory();
        Close();
    }

    private void UpdateRequirementStars(int professionalismRequired)
    {
        professionalismRequired = Mathf.Clamp(professionalismRequired, 0, starImages.Length);

        for (int i = 0; i < starImages.Length; i++)
        {
            starImages[i].sprite = emptyStarSprite;
            starImages[i].color = i < professionalismRequired ? activeColor : inactiveColor;
        }
    }

    private void UpdateAssignedStars(int assignedProfessionalism)
    {
        assignedProfessionalism = Mathf.Clamp(assignedProfessionalism, 0, assignedStarImages.Length);

        for (int i = 0; i < assignedStarImages.Length; i++)
        {
            assignedStarImages[i].sprite = filledStarSprite;
            assignedStarImages[i].color = assignedColor;
            assignedStarImages[i].enabled = i < assignedProfessionalism;
        }
    }

    private int GetAssignedProfessionalism()
    {
        if (currentMission == null) return 0;

        int total = 0;

        for (int i = 0; i < currentMission.assignedPigs.Count; i++)
        {
            PigRuntime pig = currentMission.assignedPigs[i];

            if (pig != null)
            {
                total += pig.data.GetProfessionalism() + 1;
            }
        }

        return total;
    }

    private void ReturnAssignedPigVisualsToInventory()
    {
        for (int i = 0; i < missionSlots.Length; i++)
        {
            GameObject pigGO = missionSlots[i].currentPig;

            if (pigGO == null)
                continue;

            PigUI pigUI = pigGO.GetComponent<PigUI>();
            if (pigUI == null)
                continue;

            PigRuntime pig = pigUI.GetPig();
            if (pig == null || pig.homeInventorySlot == null)
                continue;

            PigSlot homeSlot = pig.homeInventorySlot;

            missionSlots[i].currentPig = null;

            pigGO.transform.SetParent(homeSlot.transform);
            RectTransform rect = pigGO.GetComponent<RectTransform>();
            rect.anchoredPosition = Vector2.zero;
            rect.localScale = homeSlot.pigScale;

            homeSlot.currentPig = pigGO;

            pig.isDispatched = true;
            pigUI.RefreshVisualState();
        }
    }
}