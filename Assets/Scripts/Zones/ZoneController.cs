using Unity.VisualScripting;
using UnityEngine;

public class ZoneController : MonoBehaviour
{
    public int globalReputation;

    [SerializeField] public ReputationBarUI reputationBarUI;

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            reputationBarUI.totalReputationPercentage += 20;
        }
    }

}
