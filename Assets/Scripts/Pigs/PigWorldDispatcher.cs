using UnityEngine;

public class PigWorldDispatcher : MonoBehaviour
{
    [SerializeField] private GameObject pigWorldPrefab;
    [SerializeField] private Transform hqSpawnPoint;

    public void DispatchMission(MissionInfo mission)
    {
        if (mission == null)
        {
            Debug.LogError("DispatchMission called with null mission.");
            return;
        }

        if (pigWorldPrefab == null)
        {
            Debug.LogError("Pig world prefab is missing.");
            return;
        }

        if (hqSpawnPoint == null)
        {
            Debug.LogError("HQ spawn point is missing.");
            return;
        }

        for (int i = 0; i < mission.assignedPigs.Count; i++)
        {
            PigRuntime pig = mission.assignedPigs[i];

            if (pig == null)
                continue;

            Vector3 spawnOffset = new Vector3(i * 1.5f, 0f, 0f);
            Vector3 spawnPosition = hqSpawnPoint.position + spawnOffset;

            GameObject pigGO = Instantiate(pigWorldPrefab, spawnPosition, Quaternion.identity);

            PigWorld pigWorld = pigGO.GetComponent<PigWorld>();
            if (pigWorld == null)
            {
                Debug.LogError("Spawned pig world prefab does not have PigWorld.");
                continue;
            }

            pigWorld.Initialize(pig);
            pigWorld.GoToMission();
        }
    }
}