using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItens : MonoBehaviour
{
    [Header("Amounts")]
    public int totalWood;
    public int totalCarrot;
    public float currentWater;
    public int fishes;

    [Header("Limits")]
    public float woodLimit = 50;
    public float carrotLimit = 50;
    public float waterLimit = 10;
    public float fishesLimit = 3f;

    public void WaterLimit(float water)
    {
        if(currentWater < waterLimit)
        {
            currentWater += water;
            if(currentWater > waterLimit)
            {
                currentWater = waterLimit;
            }
        }
    }
}
