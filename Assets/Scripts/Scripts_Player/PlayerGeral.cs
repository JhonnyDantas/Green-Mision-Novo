using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGeral : MonoBehaviour
{
    public int totalWater = 0;
    private int maxWater = 50;

    public void WaterLimit(int water)
    {
        totalWater = Mathf.Min(totalWater + water, maxWater);
    }
}
