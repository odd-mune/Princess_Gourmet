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
            CookScene.SetActive(true);
            PauseCookManager.ChangePause(true);
        }
    }

    private void OnEnable()
    {
        IPauseManager.SetPausable(false);
        GameStateManager.ChangeState(GameState.COOKING);
    }

    private void OnDisable()
    {
        IPauseManager.SetPausable(true);
        GameStateManager.ChangeState(GameState.IDLE);
    }
}
