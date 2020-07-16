using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Reputazzione Configuration")]
public class ReputazzioneData : ScriptableSingleton<ReputazzioneData>
{
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

    public static string GetReputazzione(float points) => instance.PGetReputazzione(points);
}
