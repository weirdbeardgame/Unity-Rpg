using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gauge : MonoBehaviour
{
    public float gauge;
    bool isFilled;
    float maxTicks = 150;

    public bool fill(float speed)
    {
        // Consider an algorithm that calculates speed to Gauge fill, we have ticks in milliseconds  
        if (gauge < maxTicks && isFilled == false)
        {
            gauge += speed / maxTicks;
            Debug.Log("Gauge: " + gauge);
        }

        if (gauge >= maxTicks)
        {
            isFilled = true;
        }
        else
        {
            isFilled = false;
        }

        return isFilled;
    }

    public bool getFilled()
    {
        return isFilled;
    }

    public float Reset()
    {
        isFilled = false;
        return gauge = 0;
    }
}
