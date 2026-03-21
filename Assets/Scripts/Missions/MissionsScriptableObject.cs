using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Mission")]
public class MissionsScriptableObject : ScriptableObject
{
    [SerializeField] private string title;
    [SerializeField] private string description;
    [SerializeField] private Sprite image;
    [SerializeField] private int dangerLevel;
    [SerializeField] private float popUpCooldown;
    [SerializeField] private float missionCompletionTime;
    [SerializeField] private float professionalismRequirement;

    private void Awake()
    {
        switch(dangerLevel)
        {
            case 0:
                popUpCooldown = 3;
                professionalismRequirement = Random.Range(0, 4);
                break;
            case 1:
                popUpCooldown = 6;
                professionalismRequirement = Random.Range(3, 7);
                break;
            case 2:
                popUpCooldown = 9;
                professionalismRequirement = Random.Range(6, 10);

                break;
            case 3:
                popUpCooldown = 12;
                professionalismRequirement = Random.Range(9, 13);
                break;
            default:
                Debug.Log("pop UpCooldown Miss-Assigned");
                break;
        }
        missionCompletionTime = professionalismRequirement * 2;
    }
}
