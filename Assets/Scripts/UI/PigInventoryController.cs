using UnityEngine;

public class PigInventoryController : MonoBehaviour
{
    public GameObject pigInventoryPanel;
    public GameObject slotPrefab;
    public int slotCount;
    public GameObject pigPrefab;
    public PigsScriptableObject[] pigsScriptableObjects;

    private void Start()
    {
        ////Make the Slots
        //for(int i = 0; i < slotCount; i++)
        //{
        //    PigSlot slot = Instantiate(slotPrefab, pigInventoryPanel.transform).GetComponent<PigSlot>();
        //    if(i < pigPrefabs.Length)
        //    {
        //        GameObject pig = Instantiate(pigPrefabs[i], slot.transform);
        //        pig.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        //        slot.currentPig = pig;
        //    }
        //}

        // Create slots
        for (int i = 0; i < slotCount; i++)
        {
            Instantiate(slotPrefab, pigInventoryPanel.transform);
        }

        // Spawn pigs into slots
        for (int i = 0; i < pigsScriptableObjects.Length; i++)
        {
            PigSlot slot = pigInventoryPanel.transform.GetChild(i).GetComponent<PigSlot>();

            GameObject pigGO = Instantiate(pigPrefab, slot.transform);
            pigGO.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            Pig pig = pigGO.GetComponent<Pig>();
            pig.Initialize(pigsScriptableObjects[i]);

            slot.currentPig = pigGO;
        }

        ////Generate the police list
        //pigPrefabs = new GameObject[pigsScriptableObjects.Length];

        //for (int i = 0; i < pigPrefabs.Length; i++)
        //{
        //    pigPrefabs[i] = Instantiate()
        //}

        //Assign a random 4
        //When rank increase add one more. 
    }
}
