using NUnit.Framework;
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
                missionPopUpCooldown = 4;
                missionProfessionalismRequirement = Random.Range(0, 2);
                break;
            case 1:
                missionPopUpCooldown = 8;
                missionProfessionalismRequirement = Random.Range(1, 4);
                break;
            case 2:
                missionPopUpCooldown = 12;
                missionProfessionalismRequirement = Random.Range(3, 6);

                break;
            case 3:
                missionPopUpCooldown = 16;
                missionProfessionalismRequirement = Random.Range(5, 8);
                break;
            default:
                Debug.Log("pop UpCooldown Miss-Assigned");
                break;
        }
        missionCompletionTime = missionProfessionalismRequirement * 2;
    }
}
