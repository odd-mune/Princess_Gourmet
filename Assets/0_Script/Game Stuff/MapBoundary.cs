using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapBoundary : MonoBehaviour
{
    [Tooltip("표기할 씬 이름")]
    public string placeName;

    [Tooltip("UI 상에 씬 이름을 표기할 이미지")]
    public Image PlaceTextHolderImage;
    private TMP_Text PlaceText;

    [Tooltip("UI 상에 씬 이름을 명확히 표기할 시간 (fade-in, fade-out 시간 제외)")]
    public float TextShowTimer = 5.0f;
    [Tooltip("UI 상에 씬 이름을 fade-in할 시간")]
    public float TextFadeInTimer = 1.5f;
    [Tooltip("UI 상에 씬 이름을 fade-out할 시간")]
    public float TextFadeOutTimer = 1.5f;

    private float mCurrentTextShowTimer = 0.0f;
    private float mCurrentFadeInTimer = 0.0f;
    private float mCurrentFadeOutTimer = 0.0f;

    private TextShowState mCurrentState = TextShowState.Hidden;
    private Color mDefaultColor;
    private Color mDefaultImageColor;

    // Start is called before the first frame update
    void Start()
    {
        PlaceText = PlaceTextHolderImage.GetComponentInChildren<TMP_Text>();
        mDefaultColor = PlaceText.color;
        mDefaultImageColor = PlaceTextHolderImage.color;

        PlaceTextHolderImage.gameObject.SetActive(true);
        PlaceText.text = placeName;
        mCurrentFadeInTimer = TextFadeInTimer;
        mCurrentState = TextShowState.FadeIn;
        PlaceText.color = new Color(mDefaultColor.r, mDefaultColor.g, mDefaultColor.b, 0.0f);
        PlaceTextHolderImage.color = new Color(mDefaultImageColor.r, mDefaultImageColor.g, mDefaultImageColor.b, 0.0f);
    }

    public void FixedUpdate()
    {
        switch (mCurrentState)
        {
            case TextShowState.Hidden:
                break;
            case TextShowState.FadeIn:
                {
                    mCurrentFadeInTimer -= Time.fixedDeltaTime;
                    float x = (TextFadeInTimer - mCurrentFadeInTimer);
                    float alpha = x * x / (TextFadeInTimer * TextFadeInTimer);
                    PlaceText.color = new Color(mDefaultColor.r, mDefaultColor.g, mDefaultColor.b, alpha);
                    PlaceTextHolderImage.color = new Color(mDefaultImageColor.r, mDefaultImageColor.g, mDefaultImageColor.b, alpha);

                    if (mCurrentFadeInTimer <= 0.0f)
                    {
                        mCurrentState = TextShowState.Show;
                        mCurrentTextShowTimer = TextShowTimer;
                        PlaceText.color = new Color(mDefaultColor.r, mDefaultColor.g, mDefaultColor.b, 1.0f);
                        PlaceTextHolderImage.color = new Color(mDefaultImageColor.r, mDefaultImageColor.g, mDefaultImageColor.b, 1.0f);
                    }
                }
                break;
            case TextShowState.Show:
                {
                    mCurrentTextShowTimer -= Time.fixedDeltaTime;

                    if (mCurrentTextShowTimer <= 0.0f)
                    {
                        mCurrentState = TextShowState.FadeOut;
                        mCurrentFadeOutTimer = TextFadeOutTimer;
                    }
                }
                break;
            case TextShowState.FadeOut:
                {
                    mCurrentFadeOutTimer -= Time.fixedDeltaTime;
                    float alpha = mCurrentFadeOutTimer * mCurrentFadeOutTimer / (TextFadeOutTimer * TextFadeOutTimer);
                    PlaceText.color = new Color(mDefaultColor.r, mDefaultColor.g, mDefaultColor.b, alpha);
                    PlaceTextHolderImage.color = new Color(mDefaultImageColor.r, mDefaultImageColor.g, mDefaultImageColor.b, alpha);

                    if (mCurrentFadeOutTimer <= 0.0f)
                    {
                        mCurrentState = TextShowState.Hidden;
                        PlaceText.color = new Color(mDefaultColor.r, mDefaultColor.g, mDefaultColor.b, 0.0f);
                        PlaceTextHolderImage.color = new Color(mDefaultImageColor.r, mDefaultImageColor.g, mDefaultImageColor.b, 0.0f);
                        PlaceTextHolderImage.gameObject.SetActive(false);
                    }
                }
                break;
            case TextShowState.Count:
            // intentional fallthrough
            default:
                Debug.LogError($"Invalid TextShowState{(int)mCurrentState}");
                Debug.Break();
                break;
        }
    }
}
