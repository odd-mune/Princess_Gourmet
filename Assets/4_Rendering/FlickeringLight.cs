using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlickeringLight : MonoBehaviour
{
    public Light2D lightToControl;
    public bool isAutomated;
    public bool isOffOnDay;

    private CheckToday mCheckToday;

    private float mBaseIntensity = 0.0f;
    public float intensityVariation = 1.0f;
    public float frequency = 10.0f;

    private float mBaseRadius;
    public float radiusVariation = 1.0f;
    public float radiusFrequency = 10.0f;

    private float mRandomOffset = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        mCheckToday = FindObjectOfType<CheckToday>();

        lightToControl = GetComponentInChildren<Light2D>();
        mBaseRadius = lightToControl.pointLightOuterRadius;

        mRandomOffset = Random.Range(0.0f, 100.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isAutomated == true)
        {
            if (isOffOnDay == true && mCheckToday.IsDay() == true)
            {
                lightToControl.intensity = 0.0f;
            }
            else
            {
                UpdateIntensity();
                UpdateRadius();
            }
        }
    }

    public void UpdateIntensity()
    {
        lightToControl.intensity = 1.0f;
        float newIntensity = mBaseIntensity + Mathf.PerlinNoise(Time.time * frequency + mRandomOffset, 0) * intensityVariation;
        lightToControl.falloffIntensity = newIntensity;
    }

    public void UpdateRadius()
    {
        float newRadius = mBaseRadius + Mathf.PerlinNoise(Time.time * radiusFrequency + mRandomOffset + 100.0f, 0) * radiusVariation;
        lightToControl.pointLightOuterRadius = Mathf.Max(newRadius, lightToControl.pointLightInnerRadius);
    }
}
