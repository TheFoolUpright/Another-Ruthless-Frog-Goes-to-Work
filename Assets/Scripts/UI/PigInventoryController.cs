using UnityEngine;

public class PigInventoryController : MonoBehaviour
{
    public GameObject pigInventoryPanel;
    public GameObject slotPrefab;
    public int slotCount;
    public GameObject[] pigPrefabs;

    private void Start()
    {
        for(int i = 0; i < slotCount; i++)
        {
            PigSlot slot = Instantiate(slotPrefab, pigInventoryPanel.transform).GetComponent<PigSlot>();
            if(i < pigPrefabs.Length)
            {
                GameObject pig = Instantiate(pigPrefabs[i], slot.transform);
                pig.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentPig = pig;
            }
        }
    }
}
