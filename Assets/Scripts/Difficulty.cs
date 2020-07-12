public class Difficulty
{
    public string Name;
    public int FlavorsQuantity;
    
    public float InitialTime;
    public float TimePenalty;

    public float HotnessBonus;
    public float HotnessPenalty;
    public float CoolingPerSec;

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
    }
}
