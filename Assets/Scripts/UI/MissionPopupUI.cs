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
    [SerializeField] private Color activeColor = Color.yellow;
    [SerializeField] private Color inactiveColor = Color.white;

    [SerializeField] private PigSlot[] missionSlots;

    [SerializeField] private MissionInfo debugMission; // temp for testing

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
        UpdateStars(Mathf.RoundToInt(mission.missionsScriptableObject.missionProfessionalismRequirement));

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

    void UpdateStars(int professionalism)
    {
        for (int i = 0; i < starImages.Length; i++)
        {
            if (i < professionalism)
            {
                starImages[i].color = activeColor;
            }
            else
            {
                starImages[i].color = inactiveColor;
            }
        }
    }
}