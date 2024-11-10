using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class EndingCredit : MonoBehaviour
{
    [Tooltip("영상 종료 시 수행할 이벤트")]
    public UnityEvent OnVideoEndEvents;
    private VideoPlayer mVideoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        mVideoPlayer = GetComponent<VideoPlayer>();
        if (mVideoPlayer == null)
        {
            Debug.LogError("VideoPlayer가 필요합니다!!");
            Debug.Break();
        }

        mVideoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        OnVideoEndEvents.Invoke();
    }

    void OnDestroy()
    {
        // Unsubscribe to prevent potential memory leaks
        if (mVideoPlayer != null)
        {
            mVideoPlayer.loopPointReached -= OnVideoEnd;
        }
    }
}
