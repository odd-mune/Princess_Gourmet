using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private bool mIsAvailable = true;

    private bool mbIsTurningOn = false;
    private bool mbIsTurningOff = false;
    private float mElapsedSeconds = 0.0f;

    private float defaultIntensity;

    public bool isAvailable
    {
        get { return mIsAvailable; }
    }

    public void SetAvailable()
    {
        mIsAvailable = true;
        OnLightOn();
    }

    public void SetUnavailable()
    {
        OnLightOff();
        mIsAvailable = false;
    }

    public void OnLightOn()
    {
        lightToControl.intensity = defaultIntensity;

        mbIsTurningOn = true;
        mbIsTurningOff = false;

        if (mElapsedSeconds <= 0.0f)
        {
            mElapsedSeconds = 0.0f;
        }
    }

    public void OnLightOff()
    {
        if (mElapsedSeconds >= 1.0f)
        {
            mElapsedSeconds = 1.0f;
        }
        mbIsTurningOn = false;
        mbIsTurningOff = true;

        lightToControl.intensity = 0.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        mCheckToday = FindObjectOfType<CheckToday>();

        lightToControl = GetComponentInChildren<Light2D>();
        mBaseRadius = lightToControl.pointLightOuterRadius;

        mRandomOffset = Random.Range(0.0f, 100.0f);

        defaultIntensity = lightToControl.intensity;
        lightToControl.intensity = mIsAvailable ? defaultIntensity : 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (mIsAvailable == true)
        {
            if (isAutomated == true)
            {
                if (isOffOnDay == true && mCheckToday.IsDay() == true)
                {
                    if (mbIsTurningOff == false && lightToControl.intensity > 0.0f)
                    {
                        OnLightOff();
                    }
                }
                else
                {
                    if (mbIsTurningOn == false && lightToControl.intensity == 0.0f )
                    {
                        OnLightOn();
                    }

                    UpdateIntensity();
                    UpdateRadius();
                }

                if (mbIsTurningOff == true)
                {
                    mElapsedSeconds -= Time.fixedDeltaTime;
                    float factor = Mathf.Min(Mathf.Max(mElapsedSeconds * mElapsedSeconds, 0.0f), 1.0f);

                    if (mElapsedSeconds <= 0.0f)
                    {
                        mElapsedSeconds = 0.0f;
                        mbIsTurningOff = false;
                        lightToControl.intensity = 0.0f;
                    }
                    else
                    {
                        lightToControl.intensity = Mathf.Clamp(factor * defaultIntensity, 0.0f, 1.0f);
                    }
                }
            }
        }
        else
        {
            lightToControl.intensity = 0.0f;
        }
    }

    public void UpdateIntensity()
    {
        lightToControl.intensity = defaultIntensity;
        float newIntensity = mBaseIntensity + Mathf.PerlinNoise(Time.time * frequency + mRandomOffset, 0) * intensityVariation;
        if (mbIsTurningOn == true)
        {
            mElapsedSeconds += Time.fixedDeltaTime;
            float factor = Mathf.Min(Mathf.Max(mElapsedSeconds * mElapsedSeconds, 0.0f), 1.0f);

            if (mElapsedSeconds >= 1.0f)
            {
                mbIsTurningOn = false;
                lightToControl.intensity = defaultIntensity;
            }
            lightToControl.intensity = Mathf.Clamp(factor * defaultIntensity, 0.0f, 1.0f);
        }
        
        lightToControl.falloffIntensity = newIntensity;
    }

    public void UpdateRadius()
    {
        float newRadius = mBaseRadius + Mathf.PerlinNoise(Time.time * radiusFrequency + mRandomOffset + 100.0f, 0) * radiusVariation;
        lightToControl.pointLightOuterRadius = Mathf.Max(newRadius, lightToControl.pointLightInnerRadius);
    }
}
