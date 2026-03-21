using UnityEngine;
using UnityEngine.EventSystems;

public class PigDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform originalParent;
    CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(transform.root);
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

        PigSlot dropSlot = eventData.pointerEnter?.GetComponent<PigSlot>();
        if (dropSlot == null)
        {
            GameObject item = eventData.pointerEnter;
            if (item != null)
            {
                dropSlot = item.GetComponentInParent<PigSlot>();
            }
        }

        PigSlot originalSlot = originalParent.GetComponent<PigSlot>();

        if (dropSlot != null)
        {
            
            if(dropSlot.currentPig != null)
            {
                //Swap Pigs
                dropSlot.currentPig.transform.SetParent(originalSlot.transform);
                originalSlot.currentPig = dropSlot.currentPig;
                dropSlot.currentPig.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
            else
            {
                originalSlot.currentPig = null;
            }

            transform.SetParent(dropSlot.transform);
            dropSlot.currentPig = gameObject;
        }
        else
        {
            //No slot 
            transform.SetParent(originalParent);
        }

        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
}
