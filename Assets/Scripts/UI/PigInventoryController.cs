using System.Collections.Generic;
using UnityEngine;

public class PigInventoryController : MonoBehaviour
{
    public GameObject pigInventoryPanel;
    public GameObject slotPrefab;
    public int slotCount;

    public GameObject pigPrefab;
    public PigsScriptableObject[] pigsScriptableObjects;

    private List<PigsScriptableObject> shuffledPigs = new List<PigsScriptableObject>();
    private int currentUnlockedCount = 0;

    private List<PigRuntime> runtimePigs = new List<PigRuntime>();

    private void Start()
    {
        // Create slots
        for (int i = 0; i < slotCount; i++)
        {
            GameObject slotGO = Instantiate(slotPrefab, pigInventoryPanel.transform);
            PigSlot slot = slotGO.GetComponent<PigSlot>();
            slot.slotType = PigSlot.SlotType.Inventory;
        }

        // Copy and shuffle pigs
        shuffledPigs = new List<PigsScriptableObject>(pigsScriptableObjects);
        Shuffle(shuffledPigs);

        foreach (var pigData in shuffledPigs)
        {
            runtimePigs.Add(new PigRuntime(pigData));
        }

        // Spawn initial 4 pigs
        currentUnlockedCount = 4;
        SpawnPigs();

        
    }

    void SpawnPigs()
    {
        for (int i = 0; i < currentUnlockedCount; i++)
        {
            PigSlot slot = pigInventoryPanel.transform.GetChild(i).GetComponent<PigSlot>();

            if (slot.currentPig != null) continue;

            GameObject pigGO = Instantiate(pigPrefab, slot.transform);
            pigGO.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            PigRuntime runtimePig = runtimePigs[i];
            runtimePig.homeInventorySlot = slot;

            PigUI pigUI = pigGO.GetComponent<PigUI>();
            pigUI.Initialize(runtimePig);

            slot.currentPig = pigGO;
        }
    }


    // Call this when reputation increases
    public void UnlockNextPig()
    {
        if (currentUnlockedCount >= shuffledPigs.Count)
        {
            Debug.Log("All pigs already unlocked");
            return;
        }

        if (currentUnlockedCount >= slotCount)
        {
            Debug.Log("No empty slots available");
            return;
        }

        PigSlot slot = pigInventoryPanel.transform.GetChild(currentUnlockedCount).GetComponent<PigSlot>();

        GameObject pigGO = Instantiate(pigPrefab, slot.transform);
        pigGO.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        PigRuntime runtimePig = runtimePigs[currentUnlockedCount];
        runtimePig.homeInventorySlot = slot;

        PigUI pigUI = pigGO.GetComponent<PigUI>();
        pigUI.Initialize(runtimePig);

        slot.currentPig = pigGO;

        currentUnlockedCount++;
    }

    // Fisher-Yates shuffle for ScriptableObjects
    void Shuffle(List<PigsScriptableObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            PigsScriptableObject temp = list[i];
            list[i] = list[rand];
            list[rand] = temp;
        }
    }
}

