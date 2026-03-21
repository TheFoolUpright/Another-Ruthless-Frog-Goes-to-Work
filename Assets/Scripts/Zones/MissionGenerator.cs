using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Randomness;

public class MissionGenerator : MonoBehaviour
{
    [SerializeField] private ZoneScriptableObject zone;
    [SerializeField] private MissionsScriptableObject mission;
    [SerializeField] private MissionController missionController;
    [SerializeField] private ZoneController zoneController;
    public Transform[] missionWaypoints;
    public bool[] waypointOccupied;
    private float reputationFactor;
    private float missionGenerationTimer;
    private float missionGenerationTime;
    private float reputationEffectOnMissionCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        missionGenerationTimer = zone.zoneMissionGenerationTime;
    }

    // Update is called once per frame
    void Update()
    {
        reputationEffectOnMissionCount = Mathf.FloorToInt(zoneController.globalReputation / 5f);
        if (missionController.activeMissionList.Count == 2 + reputationEffectOnMissionCount)
        {
            missionGenerationTimer = missionGenerationTime;
            return;
        }

        if (missionGenerationTimer >= 0)
        {
            missionGenerationTimer -= Time.deltaTime;
        }
        else
        {
            if (!DetermineMissionPosition())
            {
                return;
            }
            CalculateMissionDanger();
            DetermineSpecificMission();
            missionGenerationTimer = missionGenerationTime;
        }
    }

    private bool DetermineMissionPosition()
    {
        for (int i = 0; i < waypointOccupied.Length; i++)
        {
            if (!waypointOccupied[i])
            {
                mission.missionPosition = missionWaypoints[i];
                return true;
            }
            else if (i == waypointOccupied.Length - 1)
            {
                return false;
            }
        }
        return false;
    }

    void CalculateMissionDanger()
    {
        reputationFactor = (5 - zone.zoneReputation) / 10;
        switch (zone.zoneDangerLevel)
        {
            case 0:
                mission.missionDangerLevel = Mathf.FloorToInt(NextGaussian(0 + reputationFactor, 4, 0f, 4.9f));
                break;
            case 1:
                mission.missionDangerLevel = Mathf.FloorToInt(NextGaussian(1 + reputationFactor, 4, 0f, 4.9f));
                break;
            case 2:
                mission.missionDangerLevel = Mathf.FloorToInt(NextGaussian(2 + reputationFactor, 4, 0f, 4.9f));
                break;
            case 3:
                mission.missionDangerLevel = Mathf.FloorToInt(NextGaussian(3 + reputationFactor, 4, 0f, 4.9f));
                break;
        }
    }

    void DetermineSpecificMission()
    {
        MissionsScriptableObject newMission;
        switch (mission.missionDangerLevel)
        {
            case 0:
                newMission = missionController.availableEasyMissions[UnityEngine.Random.Range(0, missionController.availableEasyMissions.Length - 1)];
                break;
            case 1:
                newMission = missionController.availableNormalMissions[UnityEngine.Random.Range(0, missionController.availableNormalMissions.Length - 1)];
                break;
            case 2:
                newMission = missionController.availableHardMissions[UnityEngine.Random.Range(0, missionController.availableHardMissions.Length - 1)];
                break;
            case 3:
                newMission = missionController.availableExtremeMissions[UnityEngine.Random.Range(0, missionController.availableExtremeMissions.Length - 1)];
                break;
            default:
                newMission = missionController.availableEasyMissions[UnityEngine.Random.Range(0, missionController.availableEasyMissions.Length - 1)];
                Debug.Log("Mission Generation Error");
                break;
        }
        missionController.activeMissionList.Add(newMission);
    }
}
