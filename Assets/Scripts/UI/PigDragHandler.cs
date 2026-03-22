using UnityEngine;
using UnityEngine.EventSystems;

public class PigDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform originalParent;
    private PigSlot originalSlot;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        originalSlot = originalParent.GetComponent<PigSlot>();

        transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        PigSlot dropSlot = GetDropSlot(eventData);

        if (dropSlot == null)
        {
            ReturnToOriginalSlot();
            return;
        }

        PigUI pigUI = GetComponent<PigUI>();
        PigRuntime pig = pigUI.GetPig();

        bool success = TryHandleDrop(originalSlot, dropSlot, pig);

        if (!success)
        {
            ReturnToOriginalSlot();
            return;
        }

        rectTransform.anchoredPosition = Vector2.zero;
    }

    private PigSlot GetDropSlot(PointerEventData eventData)
    {
        PigSlot dropSlot = eventData.pointerEnter?.GetComponent<PigSlot>();

        if (dropSlot == null && eventData.pointerEnter != null)
        {
            dropSlot = eventData.pointerEnter.GetComponentInParent<PigSlot>();
        }

        return dropSlot;
    }

    private bool TryHandleDrop(PigSlot fromSlot, PigSlot toSlot, PigRuntime pig)
    {
        if (fromSlot == null || toSlot == null || pig == null)
            return false;

        // inventory -> inventory
        if (fromSlot.slotType == PigSlot.SlotType.Inventory &&
            toSlot.slotType == PigSlot.SlotType.Inventory)
        {
            MoveOrSwap(toSlot, fromSlot);
            return true;
        }

        // inventory -> mission
        if (fromSlot.slotType == PigSlot.SlotType.Inventory &&
            toSlot.slotType == PigSlot.SlotType.Mission)
        {
            if (toSlot.linkedMission == null)
                return false;

            if (toSlot.currentPig != null)
                return false;

            bool assigned = toSlot.linkedMission.TryAssignPig(pig);
            if (!assigned)
                return false;

            fromSlot.currentPig = null;
            transform.SetParent(toSlot.transform);
            toSlot.currentPig = gameObject;
            return true;
        }

        // mission -> inventory
        if (fromSlot.slotType == PigSlot.SlotType.Mission &&
            toSlot.slotType == PigSlot.SlotType.Inventory)
        {
            if (toSlot.currentPig != null)
                return false;

            if (fromSlot.linkedMission == null)
                return false;

            fromSlot.linkedMission.RemovePig(pig);

            fromSlot.currentPig = null;
            transform.SetParent(toSlot.transform);
            toSlot.currentPig = gameObject;
            return true;
        }

        // mission -> same mission slot
        if (fromSlot.slotType == PigSlot.SlotType.Mission &&
            toSlot.slotType == PigSlot.SlotType.Mission)
        {
            if (fromSlot.linkedMission != toSlot.linkedMission)
                return false;

            MoveOrSwap(toSlot, fromSlot);
            return true;
        }

        return false;
    }

    private void MoveOrSwap(PigSlot dropSlot, PigSlot originalSlot)
    {
        if (dropSlot.currentPig != null)
        {
            dropSlot.currentPig.transform.SetParent(originalSlot.transform);
            dropSlot.currentPig.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            originalSlot.currentPig = dropSlot.currentPig;
        }
        else
        {
            originalSlot.currentPig = null;
        }

        transform.SetParent(dropSlot.transform);
        dropSlot.currentPig = gameObject;
    }

    private void ReturnToOriginalSlot()
    {
        transform.SetParent(originalParent);
        rectTransform.anchoredPosition = Vector2.zero;
    }
}