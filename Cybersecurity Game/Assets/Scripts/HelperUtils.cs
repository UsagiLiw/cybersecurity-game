using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperUtils
{ 
    public static List<int> UniqueRandomInt(int min, int max, int count)
    {
        List<int> rands = new List<int>();

        for (int i = 0; i < count; i++)
        {
            int val = (Random.Range(min, max));
            while (rands.Contains(val))
            {
                val = Random.Range(min, max);
            }
            rands.Add(val);
        }
        return rands;
    }

}
