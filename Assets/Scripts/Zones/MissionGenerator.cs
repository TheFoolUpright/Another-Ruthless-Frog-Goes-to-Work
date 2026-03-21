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
    private float reputationFactor;
    private float missionGenerationTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        missionGenerationTimer = zone.zoneMissionGenerationTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (missionGenerationTimer >= 0)
        {
            missionGenerationTimer -= Time.deltaTime;
        }
        else
        {
            DetermineMissionPosition();
            CalculateMissionDanger();
            DetermineSpecificMission();
        }

    }

    void DetermineMissionPosition()
    {
        var random = new System.Random();
        int index = zone.zoneMissionWaypoints.Count;
        mission.missionPosition = (zone.zoneMissionWaypoints[index]);
    }

    void CalculateMissionDanger()
    {
        reputationFactor = (5 - zone.zoneReputation) / 10;
        switch (zone.zoneDangerLevel)
        {
            case 0:
                mission.missionDangerLevel = Mathf.RoundToInt(NextGaussian(0 + reputationFactor, 4, -0.5f, 3.49f));
                break;
            case 1:
                mission.missionDangerLevel = Mathf.RoundToInt(NextGaussian(1 + reputationFactor, 4, -0.5f, 3.49f));
                break;
            case 2:
                mission.missionDangerLevel = Mathf.RoundToInt(NextGaussian(2 + reputationFactor, 4, -0.5f, 3.49f));
                break;
            case 3:
                mission.missionDangerLevel = Mathf.RoundToInt(NextGaussian(3 + reputationFactor, 4, -0.5f, 3.49f));
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
