using UnityEngine;

public class PigSlot : MonoBehaviour
{
    public enum SlotType
    {
        Inventory,
        Mission
    }

    public SlotType slotType;
    public GameObject currentPig;

    [HideInInspector] public MissionInfo linkedMission;

    [Header("Pig Display")]
    public Vector3 pigScale = Vector3.one;
}