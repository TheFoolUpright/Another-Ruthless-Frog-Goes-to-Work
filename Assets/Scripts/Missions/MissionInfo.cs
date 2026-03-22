using System.Collections.Generic;
using UnityEngine;

public class MissionInfo : MonoBehaviour
{

    public GameObject missionPopUpUIObject;
    public MissionsScriptableObject missionsScriptableObject;
    public MissionGenerator missionGenerator;
    public MissionController missionController;
    public bool missionStartedAtLocation;
    public float missionDuration;
    public float missionDurationTimer;
    public float missionPopUpCooldownTimer;
    public int missionDangerLevel;
    public bool missionOver;
    public int pigProfessionalism;

    public int maxAssignedPigs = 4;
    public List<PigRuntime> assignedPigs = new List<PigRuntime>();

    void Start()
    {
        DetermineSpecificMission();
        missionPopUpUIObject = missionGenerator.missionPopUpUIObject;
        DetermineMissionDuration();
        missionPopUpCooldownTimer = missionsScriptableObject.missionPopUpCooldown;
    }

    private void Update()
    {
        if (missionPopUpCooldownTimer > 0)
        {
            missionPopUpCooldownTimer -= Time.deltaTime;
        }
        else
        {

        }

            MissionResult();
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
                break;
            case 1:
                newMission = missionController.availableNormalMissions[Random.Range(0, missionController.availableNormalMissions.Length)];
                break;
            case 2:
                newMission = missionController.availableHardMissions[Random.Range(0, missionController.availableHardMissions.Length)];
                break;
            case 3:
                newMission = missionController.availableExtremeMissions[Random.Range(0, missionController.availableExtremeMissions.Length)];
                break;
            default:
                newMission = missionController.availableEasyMissions[Random.Range(0, missionController.availableEasyMissions.Length)];
                Debug.Log("Mission Generation Error");
                break;
        }

        missionsScriptableObject = newMission;
        missionController.activeMissionList.Add(newMission);
    }

    void DetermineMissionDuration()
    {
        switch (missionDangerLevel)
        {
            case 0:
                missionDuration = 2;
                break;
            case 1:
                missionDuration = 4;
                break;
            case 2:
                missionDuration = 6;
                break;
            case 3:
                missionDuration = 8;
                break;
        }
        missionDurationTimer = missionDuration;
    }

    void MissionResult()
    {
        if (missionStartedAtLocation)
        {
            if (missionDurationTimer > 0)
            {
                missionDurationTimer -= Time.deltaTime;
            }
            else
            {
                missionOver = true;
            }
        }

        if (missionOver)
        {
            if (missionsScriptableObject.missionProfessionalismRequirement <= pigProfessionalism)
            {
                switch (missionDangerLevel)
                {
                    case 0:
                        missionGenerator.zoneReputation = Mathf.Clamp(missionGenerator.zoneReputation + 5, 0, 100);
                        break;
                    case 1:
                        missionGenerator.zoneReputation = Mathf.Clamp(missionGenerator.zoneReputation + 10, 0, 100);
                        break;
                    case 2:
                        missionGenerator.zoneReputation = Mathf.Clamp(missionGenerator.zoneReputation + 15, 0, 100);
                        break;
                    case 3:
                        missionGenerator.zoneReputation = Mathf.Clamp(missionGenerator.zoneReputation + 20, 0, 100);
                        break;
                }
                Destroy(gameObject);
            }
            else
            {
                switch (missionDangerLevel)
                {
                    case 0:
                        missionGenerator.zoneReputation = Mathf.Clamp(missionGenerator.zoneReputation - 5, 0, 100);
                        break;
                    case 1:
                        missionGenerator.zoneReputation = Mathf.Clamp(missionGenerator.zoneReputation - 10, 0, 100);
                        break;
                    case 2:
                        missionGenerator.zoneReputation = Mathf.Clamp(missionGenerator.zoneReputation - 15, 0, 100);
                        break;
                    case 3:
                        missionGenerator.zoneReputation = Mathf.Clamp(missionGenerator.zoneReputation - 20, 0, 100);
                        break;
                }
                missionController.activeMissionList.Remove(missionsScriptableObject);
                Destroy(gameObject);
            }
        }
    }
}