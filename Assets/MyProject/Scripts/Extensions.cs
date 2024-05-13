using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static T Random<T>(this List<T> list)
    {
        if (list.Count == 0) return default;

        int index = UnityEngine.Random.Range(0, list.Count);
        return list[index];
    }   
    
    public static Vector3 ToV3(this Vector2 vector)
    {
        return new Vector3(vector.x, 0, vector.y);
    }
}
