using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Pig")]
public class PigsScriptableObject : ScriptableObject
{
    [SerializeField] private string pigName; 
    [SerializeField] private Sprite image; 
    [SerializeField] private int professionalism; 
    [SerializeField] private float stamina;

    public string GetName() => pigName;
    public Sprite GetImage() => image;
    public int GetProfessionalism() => professionalism;
    public float GetStamina() => stamina;
    
}
