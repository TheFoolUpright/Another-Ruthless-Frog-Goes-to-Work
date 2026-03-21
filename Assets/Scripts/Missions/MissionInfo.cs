using UnityEngine;

public class MissionInfo : MonoBehaviour
{
    public MissionsScriptableObject missionsScriptableObject;
    public MissionGenerator missionGenerator;
    public MissionController missionController;
    public int missionDangerLevel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DetermineSpecificMission();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void DetermineSpecificMission()
    {
        MissionsScriptableObject newMission;
        switch (missionDangerLevel)
        {
            case 0:
                newMission = missionController.availableEasyMissions[Random.Range(0, missionController.availableEasyMissions.Length - 1)];
                newMission.missionPopUpCooldown = 3;
                newMission.missionProfessionalismRequirement = Random.Range(0, 4);
                break;
            case 1:
                newMission = missionController.availableNormalMissions[Random.Range(0, missionController.availableNormalMissions.Length - 1)];
                newMission.missionPopUpCooldown = 6;
                newMission.missionProfessionalismRequirement = Random.Range(3, 7);
                break;
            case 2:
                newMission = missionController.availableHardMissions[Random.Range(0, missionController.availableHardMissions.Length - 1)];
                newMission.missionPopUpCooldown = 9;
                newMission.missionProfessionalismRequirement = Random.Range(6, 10);
                break;
            case 3:
                newMission = missionController.availableExtremeMissions[Random.Range(0, missionController.availableExtremeMissions.Length - 1)];
                newMission.missionPopUpCooldown = 12;
                newMission.missionProfessionalismRequirement = Random.Range(9, 13);
                break;
            default:
                newMission = missionController.availableEasyMissions[Random.Range(0, missionController.availableEasyMissions.Length - 1)];
                newMission.missionPopUpCooldown = 3;
                newMission.missionProfessionalismRequirement = Random.Range(0, 4);
                Debug.Log("Mission Generation Error");
                break;
        }
        missionsScriptableObject = newMission;
        missionController.activeMissionList.Add(newMission);
    }
}
