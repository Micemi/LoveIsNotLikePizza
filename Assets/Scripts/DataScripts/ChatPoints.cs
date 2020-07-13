using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Chat Points Configuration")]
public class ChatPoints : ScriptableSingleton<ChatPoints>
{
    [Serializable]
    private struct PointConversion
    {
        [Range(0, 1)]
        public float UpperLimit;
        public float Points;
    }
    
    [SerializeField]
    private List<PointConversion> pointConversions = new List<PointConversion>();

    private void OnValidate()
    {
        for (int i = 0; i < pointConversions.Count; i++)
        {
            PointConversion pointConversion = pointConversions[i];
            if (pointConversion.UpperLimit < 0)
                pointConversion.UpperLimit = 0;
            else if (pointConversion.UpperLimit > 1)
                pointConversion.UpperLimit = 1;
            pointConversions[i] = pointConversion;
        }
    }

    private void Awake()
    {
        pointConversions.Sort((p, q) => p.UpperLimit.CompareTo(q.UpperLimit));
    }

    private float PGetPoints(float hotness)
    {
        for (int i = 0; i < pointConversions.Count; i++)
        {
            if (pointConversions[i].UpperLimit > hotness)
                return pointConversions[i].Points;
        }

        return pointConversions[pointConversions.Count - 1].Points; // maximum points
    }

    public static float GetPoints(float hotness) => instance.PGetPoints(hotness);
}
