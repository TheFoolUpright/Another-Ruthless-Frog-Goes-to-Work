using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Mission")]
public class MissionsScriptableObject : ScriptableObject
{
    public string missionTitle;
    public string missionDescription;
    public Sprite missionImage;
    public int missionDangerLevel;
    public float missionPopUpCooldown;
    public float missionCompletionTime;
    public float missionProfessionalismRequirement;

    private void Awake()
    {
        switch(missionDangerLevel)
        {
            case 0:
                missionPopUpCooldown = 3;
                missionProfessionalismRequirement = Random.Range(0, 4);
                break;
            case 1:
                missionPopUpCooldown = 6;
                missionProfessionalismRequirement = Random.Range(3, 7);
                break;
            case 2:
                missionPopUpCooldown = 9;
                missionProfessionalismRequirement = Random.Range(6, 10);

                break;
            case 3:
                missionPopUpCooldown = 12;
                missionProfessionalismRequirement = Random.Range(9, 13);
                break;
            default:
                Debug.Log("pop UpCooldown Miss-Assigned");
                break;
        }
        missionCompletionTime = missionProfessionalismRequirement * 2;
    }
}
