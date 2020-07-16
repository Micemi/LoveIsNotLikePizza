using System.Collections.Generic;

public class Difficulty
{
    public string Name;
    public int FlavorsQuantity;
    
    public float InitialTime;
    public float TimePenalty;

    public float HotnessBonus;
    public float HotnessPenalty;
    public float CoolingPerSec;

    public List<PointConversion> pointConversions;

    public Difficulty() { }

    public Difficulty(DifficultyData difficultyData)
    {
        Name = difficultyData.Name;
        FlavorsQuantity = difficultyData.FlavorsQuantity;
        
        InitialTime = difficultyData.InitialTime;
        TimePenalty = difficultyData.TimePenalty;

        HotnessBonus = difficultyData.HotnessBonus;
        HotnessPenalty = difficultyData.HotnessPenalty;
        CoolingPerSec = difficultyData.CoolingPerSec;

        pointConversions = new List<PointConversion>(difficultyData.pointConversions);
    }

    public float GetPoints(float hotness)
    {
        pointConversions.Sort((p, q) => p.UpperLimit.CompareTo(q.UpperLimit));

        for (int i = 0; i < pointConversions.Count; i++)
        {
            if (pointConversions[i].UpperLimit > hotness)
                return pointConversions[i].Points;
        }

        return pointConversions[pointConversions.Count - 1].Points; // maximum points
    }
}
