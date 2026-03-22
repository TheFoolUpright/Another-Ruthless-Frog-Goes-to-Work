using UnityEngine;

public class MissionClicking : MonoBehaviour
{
    public GameObject missionPopUpUIObject;
    public MissionPopupUI missionPopupUI;
    public MissionInfo missionInfo;

    void Start()
    {
        missionInfo = GetComponent<MissionInfo>();
        missionPopUpUIObject = missionInfo.missionPopUpUIObject;
    }

    void OnMouseDown()
    {
        Debug.Log(missionInfo);
        missionPopUpUIObject.SetActive(true);
        missionPopUpUIObject.GetComponent<MissionPopupUI>().Open(missionInfo);
    }
}
