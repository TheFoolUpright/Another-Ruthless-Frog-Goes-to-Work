using UnityEngine;

public class MissionClicking : MonoBehaviour
{
    public GameObject missionPopUpUIObject;
    public MissionPopupUI missionPopupUI;
    public MissionInfo missionInfo;
    public bool missionClicked;
    [SerializeField] private SpriteRenderer sprite;

    void Start()
    {
        missionInfo = GetComponent<MissionInfo>();
        missionPopUpUIObject = missionInfo.missionPopUpUIObject;
    }

    void OnMouseDown()
    {
        if (!missionClicked)
        {
            missionPopUpUIObject.SetActive(true);
            missionPopUpUIObject.GetComponent<MissionPopupUI>().Open(missionInfo);
            sprite.enabled = false;
            missionClicked = true;
        }

    }
}
