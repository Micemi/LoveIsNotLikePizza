using System;
using System.Collections.Generic;
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

    [Header("Points")]
    [SerializeField]
    public List<PointConversion> pointConversions = new List<PointConversion>();

}


[Serializable]
public struct PointConversion
{
    [Range(0, 1)]
    public float UpperLimit;
    public float Points;
}
