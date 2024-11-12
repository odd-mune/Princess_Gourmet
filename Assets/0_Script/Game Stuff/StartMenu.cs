using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.Video;

public class StartMenu : MonoBehaviour
{
    [Tooltip("오디오 매니저")]
    public AudioManager AudioManager;
    [Tooltip("영상 출력할 raw image")]
    public RawImage TargetRawImage;
    [Tooltip("영상 종료 시 수행할 이벤트")]
    public UnityEvent OnVideoEndEvents;
    [Tooltip("영상 플레이 시 나올 음악 이름")]
    public string AudioNameOnVideo;
    private VideoPlayer mVideoPlayer;
    private bool mbIsPlayingTimeline = true;

    // Start is called before the first frame update
    void Start()
    {
        mVideoPlayer = GetComponentInChildren<VideoPlayer>();
        if (mVideoPlayer == null)
        {
            Debug.LogError("VideoPlayer가 필요합니다!!");
            Debug.Break();
        }

        mVideoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        if (AudioNameOnVideo.Length > 0)
        {
            AudioManager.Stop(AudioNameOnVideo);
        }
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

    public void PlayIntroLoop()
    {
        mbIsPlayingTimeline = false;
        AudioManager.Play("intro");
    }

    private void OnDisable()
    {
        AudioManager.Stop("intro");
    }

    public void PlayIntro()
    {
        if (mbIsPlayingTimeline == true)
        {
            PlayableDirector playableDirector = GetComponent<PlayableDirector>();
            playableDirector.Stop();
        }

        AudioManager.Stop("intro");
        VideoPlayer videoPlayer = GetComponentInChildren<VideoPlayer>();
        videoPlayer.Play();
        if (AudioNameOnVideo.Length > 0)
        {
            AudioManager.Play(AudioNameOnVideo);
        }
        TargetRawImage.gameObject.SetActive(true);
    }
}
