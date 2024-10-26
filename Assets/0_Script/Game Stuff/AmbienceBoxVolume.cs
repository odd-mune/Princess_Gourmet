using UnityEngine;

public class AmbienceBoxVolume : MonoBehaviour
{
    [System.Serializable]
    public struct PlayableTimeInfo
    {
        public bool Day;
        public bool Night;
    };

    public CheckToday checkToday;
    public string audioName;
    public bool enableAttenuation = false;
    public float attenuationBeginRatio = 1.0f;
    public PlayableTimeInfo playableTimeInfo;
    private AudioManager mAudioManager = null;
    private bool mbIsPlaying = false;
    private GameObject mPlayer = null;

    private bool mbIsTurningOff = false;
    private bool mbIsTurningOn = false;
    private float mCurrentVolume = -1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (mbIsPlaying == true)
        {
            bool isDay = checkToday != null ? checkToday.IsDay() : true;
            if (checkToday != null && (isDay && playableTimeInfo.Day == false)
                || (isDay == false && playableTimeInfo.Night == false))
            {
                if (mbIsTurningOff == false)
                {
                    mbIsTurningOff = true;
                    mbIsTurningOn = false;
                }
            }
            else
            {
                if (mbIsTurningOn == false)
                {
                    mbIsTurningOn = true;
                    mbIsTurningOff = false;
                }
            }

            {
                float distance = Vector3.Distance(mPlayer.transform.position, transform.position);

                float maxDistance = 0.0f;

                CircleCollider2D circleCollider2DOrNull = GetComponent<CircleCollider2D>();
                if (circleCollider2DOrNull != null)
                {
                    maxDistance = circleCollider2DOrNull.radius;
                }

                PolygonCollider2D polygonCollider2DOrNull = GetComponent<PolygonCollider2D>();
                if (polygonCollider2DOrNull != null)
                { // Get the bounds of the PolygonCollider2D
                    Bounds bounds = polygonCollider2DOrNull.bounds;

                    // Calculate the closest point on the bounds
                    Vector2 closestPoint = new Vector2(
                        Mathf.Clamp(mPlayer.transform.position.x, bounds.min.x, bounds.max.x),
                        Mathf.Clamp(mPlayer.transform.position.y, bounds.min.y, bounds.max.y)
                    );
                }

                float attenuationBeginDistance = maxDistance * attenuationBeginRatio;

                float scale = Mathf.Abs(maxDistance - attenuationBeginDistance);
                float x = (distance - maxDistance);
                if (scale != 0.0f)
                {
                    x /= scale;
                }
                x *= -1.0f;
                float ratio = x <= 0.0f ? 0.0f : (x >= 1.0f ? 1.0f : 3 * x * x - 2 * x * x * x);

                bool isPlaying = mAudioManager.IsPlaying(audioName);
                if (isPlaying == false)
                {
                    mAudioManager.ResetVolumn(audioName);
                    mCurrentVolume = mAudioManager.GetVolume(audioName);
                }

                if (mbIsTurningOff == true && mbIsTurningOn == false)
                {
                    mCurrentVolume = mAudioManager.GetVolume(audioName);
                    float deltaVolume = (Time.fixedDeltaTime / 1.5f) * mCurrentVolume;
                    mCurrentVolume -= deltaVolume;
                    if (mCurrentVolume <= 0.0f)
                    {
                        mAudioManager.Stop(audioName);
                        isPlaying = false;
                        mbIsTurningOff = false;
                    }
                    else
                    {
                        mAudioManager.SetVolumn(audioName, mCurrentVolume);
                    }
                }
                else if (mbIsTurningOff == false && mbIsTurningOn == true)
                {
                    mCurrentVolume = mAudioManager.GetVolume(audioName);
                    if (mCurrentVolume < 0.0f)
                    {
                        mAudioManager.Play(audioName);
                        isPlaying = true;
                    }
                    float deltaVolume = (Time.fixedDeltaTime / 1.5f) * mCurrentVolume;
                    mCurrentVolume += deltaVolume;
                }

                if (mCurrentVolume >= ratio)
                {
                    mbIsTurningOff = false;
                }

                float nextVolume = Mathf.Clamp(mCurrentVolume, 0.0f, ratio);
                if (isPlaying == false && nextVolume >= 0.0f)
                {
                    mAudioManager.Play(audioName);
                }
                mAudioManager.SetVolumn(audioName, nextVolume);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isDay = checkToday != null ? checkToday.IsDay() : true;
        if (checkToday != null 
            && (isDay && playableTimeInfo.Day == false)
            || (isDay == false && playableTimeInfo.Night == false))
        {
            return;
        }

        if (mPlayer == collision.gameObject || (mPlayer == null && collision.CompareTag("Player")))
        {
            if (mAudioManager == null)
            {
                mAudioManager = GetComponentInParent<AudioManager>();
            }
            mPlayer = collision.gameObject;
            if (mAudioManager.IsPlaying(audioName) == false)
            {
                mbIsTurningOff = false;
            }
            mbIsPlaying = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        bool isDay = checkToday != null ? checkToday.IsDay() : true;
        if (checkToday != null 
            && (isDay && playableTimeInfo.Day == false)
            || (isDay == false && playableTimeInfo.Night == false))
        {
            return;
        }

        if (mPlayer == collision.gameObject || (mPlayer == null && collision.CompareTag("Player")))
        {
            if (mAudioManager == null)
            {
                mAudioManager = GetComponentInParent<AudioManager>();
            }
            mPlayer = collision.gameObject;
            mbIsPlaying = false;

            if (mAudioManager.GetVolume(audioName) <= float.MinValue)
            {
                mbIsTurningOff = false;
                mAudioManager.Stop(audioName);
            }
        }
    }
}
