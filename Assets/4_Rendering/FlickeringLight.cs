using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlickeringLight : MonoBehaviour
{
    [Tooltip("제어할 Light2D")]
    public Light2D lightToControl;
    [Tooltip("True일 시 자동으로 배치된 Scene에서 깜빡인다.")]
    public bool isAutomated;
    [Tooltip("True일 시 밤에만 켜진다.")]
    public bool isOffOnDay;

    private CheckToday mCheckToday;

    private float mBaseIntensity = 0.0f;
    [Tooltip("밝기 깜빡임 정도. 크면 클 수록 깜빡이는 범위가 커진다.")]
    public float intensityVariation = 1.0f;
    [Tooltip("밝기 깜빡임 속도. 값이 작을 수록 느려진다.")]
    public float frequency = 10.0f;

    private float mBaseRadius;
    [Tooltip("빛 영향 반지름 깜빡임 정도. 크면 클 수록 깜빡이는 범위가 커진다.")]
    public float radiusVariation = 1.0f;
    [Tooltip("반지름 깜빡임 속도. 값이 작을 수록 느려진다.")]
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
