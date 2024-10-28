using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CheckToday : MonoBehaviour
{
    public static float elapsedSeconds;
    [Tooltip("해가 떠있는 시간 (초)")]
    public float numSecondsInSun;
    [Tooltip("해가 져있는 시간 (초)")]
    public float numSecondsInMoon;
    [Tooltip("태양 빛 최대값 (기본 1.0)")]
    public float maxIntensity;
    [Tooltip("태양 빛 최소값 (기본 0.0)")]
    public float minIntensity;
    [Tooltip("태양 역할을 할 Global Light 2D")]
    public Light2D globalLight2D;
    [Tooltip("현재 Scene에서 태양빛 시뮬레이션을 적용할 지 여부")]
    public bool controlGlobalLight;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Function to check if it is currently day
    public bool IsDay()
    {
        // Calculate the total cycle time
        float totalCycleTime = numSecondsInSun + numSecondsInMoon;

        if (totalCycleTime <= 0) return false; // If there's no cycle, assume it's not day

        // Determine if the current elapsed time is in the daytime
        float currentTimeInCycle = elapsedSeconds % totalCycleTime;

        return currentTimeInCycle < numSecondsInSun;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        elapsedSeconds += Time.fixedDeltaTime;

        if (controlGlobalLight == true)
        {
            // Calculate the total cycle time
            float totalCycleTime = numSecondsInSun + numSecondsInMoon;

            // Ensure totalCycleTime is not zero to prevent division by zero
            if (totalCycleTime > 0)
            {
                // Normalize elapsed time to a range of 0 to 1
                float normalizedTime = (elapsedSeconds % totalCycleTime) / totalCycleTime;

                // Get the brightness based on normalized time
                float brightnessValue = GetBrightness(normalizedTime);

                globalLight2D.intensity = brightnessValue * (maxIntensity - minIntensity) + minIntensity;
            }
        }
    }

    // Function to get brightness based on normalized time (0 to 1)
    private float GetBrightness(float normalizedTime)
    {
        // Calculate sunlight contribution
        float sunlight = getSunLight(normalizedTime);

        // Calculate moonlight contribution
        float moonlight = 0.15f;

        // Starlight: constant low brightness
        float starlight = 0.05f;

        // Total brightness
        return sunlight + moonlight + starlight;
    }

    private float getSunLight(float normalizedTime)
    {
        float totalCycleTime = numSecondsInSun + numSecondsInMoon;
        if (totalCycleTime <= 0.0f)
        {
            return 0.0f;
        }

        float sunRatio = numSecondsInSun / totalCycleTime;

        if (normalizedTime <= 0.0f)
        {
            return 0.0f;
        }
        else if (normalizedTime > sunRatio)
        {
            return 0.0f;
        }

        float x = (2.0f * normalizedTime / sunRatio) - 1.0f; 

        return 1.0f - x * x;
    }
}