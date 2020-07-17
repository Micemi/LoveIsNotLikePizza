using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Reputazzione Configuration")]
public class ReputazzioneData : ScriptableObject
{
    private static ReputazzioneData instance;
    public static ReputazzioneData Instance
    {
        get
        {
            if (instance == null)
                instance = Resources.LoadAll<ReputazzioneData>("Data")[0];
            return instance;
        }
    }

    [Serializable]
    private struct PointConversion
    {
        public float PointsUpperLimit;
        public string Title;
    }

    [SerializeField]
    private List<PointConversion> pointConversions = new List<PointConversion>();

    private string PGetReputazzione(float points)
    {
        pointConversions.Sort((p, q) => p.PointsUpperLimit.CompareTo(q.PointsUpperLimit));

        for (int i = 0; i < pointConversions.Count; i++)
        {
            if (pointConversions[i].PointsUpperLimit > points)
                return pointConversions[i].Title;
        }

        return pointConversions[pointConversions.Count - 1].Title; // maximum points
    }

    public static string GetReputazzione(float points) => Instance.PGetReputazzione(points);
}
