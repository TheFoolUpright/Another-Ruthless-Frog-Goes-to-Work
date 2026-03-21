using UnityEngine;

public class PigRuntime
{
    public PigsScriptableObject data;
    public float currentStamina;
    public MissionInfo currentMission;

    public PigRuntime(PigsScriptableObject data)
    {
        this.data = data;

        if (data != null)
        {
            currentStamina = data.GetStamina();
        }
        else
        {
            Debug.LogError("PigRuntime was created with null data");
            currentStamina = 0f;
        }
    }

    public bool HasMission()
    {
        return currentMission != null;
    }
}