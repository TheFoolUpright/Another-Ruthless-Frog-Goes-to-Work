using UnityEngine;
using static Randomness;

public class MissionGenerator : MonoBehaviour
{
    [SerializeField] private ZoneScriptableObject zone;
    [SerializeField] private MissionsScriptableObject mission;
    private float reputationFactor;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        reputationFactor = (5 - zone.zoneReputation)/10;
        switch (zone.zoneDangerLevel)
        {
            case 0:
                mission.missionDangerLevel = Mathf.RoundToInt(NextGaussian(0 + reputationFactor, 4, -0.5f, 3.4f));
                break;
            case 1:
                mission.missionDangerLevel = Mathf.RoundToInt(NextGaussian(1 + reputationFactor, 4, -0.5f, 3.4f));
                break;
            case 2:
                mission.missionDangerLevel = Mathf.RoundToInt(NextGaussian(2 + reputationFactor, 4, -0.5f, 3.4f));
                break;
            case 3:
                mission.missionDangerLevel = Mathf.RoundToInt(NextGaussian(3 + reputationFactor, 4, -0.5f, 3.4f));
                break;
        }
    }
}
