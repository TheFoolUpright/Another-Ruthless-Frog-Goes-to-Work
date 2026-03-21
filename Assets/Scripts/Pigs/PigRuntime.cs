using System.Reflection;
using UnityEngine;

public class PigRuntime
{
    public PigsScriptableObject data;

    public float currentStamina;
    public MissionInfo currentMission;

    public PigRuntime(PigsScriptableObject data)
    {
        this.data = data;
        currentStamina = data.GetStamina();
    }

    public bool HasMission()
    {
        return currentMission != null;
    }
}