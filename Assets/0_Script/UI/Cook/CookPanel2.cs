using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookPanel2 : MonoBehaviour
{
    [Tooltip("Pause Cook Manager")]
    public PauseCookManager PauseCookManager;
    [Tooltip("Cook Scene")]
    public GameObject CookScene;
    [Tooltip("임시. Cook animation 실행 시간 (초)")]
    public float Duration;
    private float mCurrentDuration;
    private AudioManager mAudioManager;

    // Start is called before the first frame update
    void Start()
    {
        mCurrentDuration = Duration;
        if (mAudioManager == null)
        {
            mAudioManager = FindObjectOfType<AudioManager>();
            if (mAudioManager == null)
            {
                Debug.LogError("AudioManager가 Scene에 없습니다!!");
                Debug.Break();
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mCurrentDuration -= Time.fixedDeltaTime;
        if (mCurrentDuration < 0)
        {
            gameObject.SetActive(false);
            CookScene.SetActive(true);
            PauseCookManager.ChangePause(true);
        }
    }

    private void OnEnable()
    {
        IPauseManager.SetPausable(false);
        GameStateManager.ChangeState(GameState.COOKING);
        if (mAudioManager == null)
        {
            mAudioManager = FindObjectOfType<AudioManager>();
            if (mAudioManager == null)
            {
                Debug.LogError("AudioManager가 Scene에 없습니다!!");
                Debug.Break();
            }
        }
        mAudioManager.Play("boiling");
        mAudioManager.Play("pouring_milk");
    }

    private void OnDisable()
    {
        IPauseManager.SetPausable(true);
        GameStateManager.ChangeState(GameState.IDLE);
        mAudioManager.Stop("boiling");
        mAudioManager.Stop("pouring_milk");
    }

    public void OnStirring()
    {

    }
}
