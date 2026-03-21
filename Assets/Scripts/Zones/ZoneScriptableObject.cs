using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Zone")]
public class ZoneScriptableObject : ScriptableObject
{
    [SerializeField] private float dangerLevel;
    [SerializeField] private float currentReputation;
    [SerializeField] private float missionCooldown;
    [SerializeField] private List<Transform> missionWaypoints;
}
