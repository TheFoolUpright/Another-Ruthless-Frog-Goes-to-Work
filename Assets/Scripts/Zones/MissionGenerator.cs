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

    void Start()
    {
        waypointOccupied = new bool[missionWaypoints.Length];
        missionGenerationTimer = zone.zoneMissionGenerationTime;

        for (int i = 0; i < waypointOccupied.Length; i++)
        {
            waypointOccupied[i] = false;
        }
    }

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
            int waypointIndex = GetFreeWaypointIndex();

            if (waypointIndex == -1)
            {
                return;
            }

            CalculateMissionDanger();
            SpawnMissionAtWaypoint(waypointIndex);
            missionGenerationTimer = zone.zoneMissionGenerationTime;
        }
    }

    private int GetFreeWaypointIndex()
    {
        for (int i = 0; i < waypointOccupied.Length; i++)
        {
            if (!waypointOccupied[i])
            {
                waypointOccupied[i] = true;
                return i;
            }
        }

        return -1;
    }

    void CalculateMissionDanger()
    {
        reputationFactor = (5 - zone.zoneReputation) / 10f;

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

            default:
                missionDangerLevel = 0;
                break;
        }
    }

    void SpawnMissionAtWaypoint(int index)
    {
        GameObject mission = Instantiate(missionPrefab);

        MissionInfo missionInfo = mission.GetComponent<MissionInfo>();

        missionInfo.missionDangerLevel = missionDangerLevel;
        missionInfo.missionController = missionController;
        missionInfo.missionGenerator = this.GetComponent<MissionGenerator>();

        mission.transform.position = new Vector2(
            missionWaypoints[index].position.x,
            missionWaypoints[index].position.y
        );
    }
}