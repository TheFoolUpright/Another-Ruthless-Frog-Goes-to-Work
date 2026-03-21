using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Zone")]
public class ZoneScriptableObject : ScriptableObject
{
    public float zoneDangerLevel;
    public float zoneReputation;
    public float zoneMissionGenerationTime; //seconds

    private void Awake()
    {
        switch (zoneDangerLevel)
        {
            case 0:
                zoneMissionGenerationTime = 60;
                break;
            case 1:
                zoneMissionGenerationTime = 40;
                break;
            case 2:
                zoneMissionGenerationTime = 30;

                break;
            case 3:
                zoneMissionGenerationTime = 20;
                break;
            default:
                Debug.Log("Zone Danger Miss-Assigned");
                break;
        }
    }
}
