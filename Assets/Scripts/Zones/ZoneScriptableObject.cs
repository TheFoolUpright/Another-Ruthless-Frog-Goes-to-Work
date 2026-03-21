using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Zone")]
public class ZoneScriptableObject : ScriptableObject
{
    public float zoneDangerLevel;
    public float zoneReputation;
    public float zoneMissionCooldown;
    public List<Transform> zoneMissionWaypoints;
}
