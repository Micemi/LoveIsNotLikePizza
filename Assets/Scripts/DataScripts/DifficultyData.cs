using UnityEngine;

[CreateAssetMenu(menuName = "Create Difficulty")]
public class DifficultyData : ScriptableObject
{
    public string Name;
    [Range(0, 4)] public int FlavorsQuantity;
    
    [Header("Time")]
    public float InitialTime = 40;
    public float TimePenalty;

    [Header("Hotness")]
    [Range(0, 1)] public float HotnessBonus;
    [Range(0, 1)] public float HotnessPenalty;
    [Range(0, 1)] public float CoolingPerSec;

}
