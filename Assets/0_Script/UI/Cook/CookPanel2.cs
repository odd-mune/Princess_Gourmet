using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookPanel2 : MonoBehaviour
{
    [Tooltip("임시. Cook animation 실행 시간 (초)")]
    public float Duration;
    private float mCurrentDuration;

    // Start is called before the first frame update
    void Start()
    {
        mCurrentDuration = Duration;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mCurrentDuration -= Time.fixedDeltaTime;
        if (mCurrentDuration < 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        IPauseManager.SetPausable(false);
    }

    private void OnDisable()
    {
        IPauseManager.SetPausable(true);
    }
}
