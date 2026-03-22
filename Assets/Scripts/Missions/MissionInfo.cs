using System.Collections.Generic;
using UnityEngine;

public class MissionInfo : MonoBehaviour
{

    public GameObject missionPopUpUIObject;
    public MissionsScriptableObject missionsScriptableObject;
    public MissionGenerator missionGenerator;
    public MissionController missionController;
    public bool missionStarted;
    public int missionDangerLevel;

    public int maxAssignedPigs = 4;
    public List<PigRuntime> assignedPigs = new List<PigRuntime>();

    void Start()
    {
        DetermineSpecificMission();
        missionPopUpUIObject = missionGenerator.missionPopUpUIObject;
    }

    public Transform GetTarget()
    {
        return transform;
    }

    public bool TryAssignPig(PigRuntime pig)
    {
        if (pig == null) return false;

        if (assignedPigs.Contains(pig))
            return false;

        if (assignedPigs.Count >= maxAssignedPigs)
            return false;

        assignedPigs.Add(pig);
        pig.currentMission = this;
        return true;
    }

    public void RemovePig(PigRuntime pig)
    {
        if (pig == null) return;

        if (assignedPigs.Remove(pig))
        {
            if (pig.currentMission == this)
            {
                pig.currentMission = null;
            }
        }
    }

    public bool ContainsPig(PigRuntime pig)
    {
        return assignedPigs.Contains(pig);
    }

    void DetermineSpecificMission()
    {
        MissionsScriptableObject newMission;

        switch (missionDangerLevel)
        {
            case 0:
                newMission = missionController.availableEasyMissions[Random.Range(0, missionController.availableEasyMissions.Length)];
                newMission.missionPopUpCooldown = 3;
                newMission.missionProfessionalismRequirement = Random.Range(0, 4);
                break;
            case 1:
                newMission = missionController.availableNormalMissions[Random.Range(0, missionController.availableNormalMissions.Length)];
                newMission.missionPopUpCooldown = 6;
                newMission.missionProfessionalismRequirement = Random.Range(3, 7);
                break;
            case 2:
                newMission = missionController.availableHardMissions[Random.Range(0, missionController.availableHardMissions.Length)];
                newMission.missionPopUpCooldown = 9;
                newMission.missionProfessionalismRequirement = Random.Range(6, 10);
                break;
            case 3:
                newMission = missionController.availableExtremeMissions[Random.Range(0, missionController.availableExtremeMissions.Length)];
                newMission.missionPopUpCooldown = 12;
                newMission.missionProfessionalismRequirement = Random.Range(9, 13);
                break;
            default:
                newMission = missionController.availableEasyMissions[Random.Range(0, missionController.availableEasyMissions.Length)];
                newMission.missionPopUpCooldown = 3;
                newMission.missionProfessionalismRequirement = Random.Range(0, 4);
                Debug.Log("Mission Generation Error");
                break;
        }

        missionsScriptableObject = newMission;
        missionController.activeMissionList.Add(newMission);
    }
}