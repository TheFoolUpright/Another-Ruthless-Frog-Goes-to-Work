using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Pig")]
public class PigsScriptableObject : ScriptableObject
{
    [SerializeField] private string pigName;
    [SerializeField] private Sprite image;
    [SerializeField] private int professionalism;
    [SerializeField] private float stamina;
    [SerializeField] private string state; //TODO: Change the type to be a state


}
