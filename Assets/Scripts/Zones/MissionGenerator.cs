using UnityEngine;
using static Randomness;

public class MissionGenerator : MonoBehaviour
{
    [SerializeField] private MissionController missionController;
    [SerializeField] private ZoneController zoneController;
    [SerializeField] private ZoneScriptableObject zone;
    [SerializeField] private GameObject missionPrefab;
    [SerializeField] private float reputationEffectOnMissionCount;
    [SerializeField] private float missionGenerationTimer;
    [SerializeField] private float reputationFactor;
    public Transform[] missionWaypoints;
    public bool[] waypointOccupied;
    private int missionDangerLevel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waypointOccupied = new bool[missionWaypoints.Length];
        missionGenerationTimer = zone.zoneMissionGenerationTime;
        for (int i = 0; i < waypointOccupied.Length; i++)
        {
            waypointOccupied[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        reputationEffectOnMissionCount = Mathf.FloorToInt(zoneController.globalReputation / 5f);
        if (missionController.activeMissionList.Count == 2 + reputationEffectOnMissionCount)
        {
            missionGenerationTimer = zone.zoneMissionGenerationTime;
            return;
        }

        if (missionGenerationTimer >= 0)
        {
            missionGenerationTimer -= Time.deltaTime;
        }
        else
        {
            if (!CheckForAvailablePosition())
            {
                return;
            }
            CalculateMissionDanger();
            DetermineMissionPosition();
            missionGenerationTimer = zone.zoneMissionGenerationTime;
        }
    }

    private bool CheckForAvailablePosition()
    {
        for (int i = 0; i < waypointOccupied.Length; i++)
        {
            if (!waypointOccupied[i])
            {
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
                missionDangerLevel = Mathf.FloorToInt(NextGaussian(0 + reputationFactor, 4, 0f, 4.9f));
                break;
            case 1:
                missionDangerLevel = Mathf.FloorToInt(NextGaussian(1 + reputationFactor, 4, 0f, 4.9f));
                break;
            case 2:
                missionDangerLevel = Mathf.FloorToInt(NextGaussian(2 + reputationFactor, 4, 0f, 4.9f));
                break;
            case 3:
                missionDangerLevel = Mathf.FloorToInt(NextGaussian(3 + reputationFactor, 4, 0f, 4.9f));
                break;
        }
    }

    void DetermineMissionPosition()
    {
        for (int i = 0; i < waypointOccupied.Length; i++)
        {
            if (!waypointOccupied[i])
            {
                GameObject mission = Instantiate(missionPrefab);
                mission.GetComponent<MissionInfo>().missionDangerLevel = missionDangerLevel;
                mission.transform.position = new Vector2(missionWaypoints[i].position.x, missionWaypoints[i].position.y);
            }
        }
    }
}
