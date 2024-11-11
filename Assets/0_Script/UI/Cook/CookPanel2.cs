using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class CookPanel2 : MonoBehaviour
{
    [Tooltip("Pause Cook Manager")]
    public PauseCookManager PauseCookManager;
    [Tooltip("Cook Scene")]
    public GameObject CookScene;
    private AudioManager mAudioManager;

    [Tooltip("요리 이미지")]
    public Image ResultImage;
    [Tooltip("요리 이미지")]
    public TMPro.TextMeshProUGUI ResultText;

    [Tooltip("요리 PlayableDirector")]
    public PlayableDirector CookingPlayableDirector;
    [Tooltip("요리 성공 PlayableDirector")]
    public PlayableDirector SuccessPlayableDirector;
    [Tooltip("요리 실패 PlayableDirector")]
    public PlayableDirector FailurePlayableDirector;

    [Tooltip("요리 결과 이펙트 prefab")]
    private CookResultEffect[] mEffects;

    [Tooltip("요리 결과 이펙트가 나오기 시작할 최소 반지름. 이 반지름보다 작은 구역에서는 이펙트가 나오지 않음.")]
    public float EffectInnerRadius = 0.0f;
    [Tooltip("요리 결과 이펙트가 나오기 시작할 최대 반지름. 이 반지름보다 큰 구역에서는 이펙트가 나오지 않음.")]
    public float EffectOutRadius = 0.0f;

    private InventoryItem mResultItem;
    private bool mbHasFaileDish;

    public bool hasDishFailed { get { return mbHasFaileDish; } }

    public void InitCooking(InventoryItem resultItem, bool hasFailedDish)
    {
        mResultItem = resultItem;
        mbHasFaileDish = hasFailedDish;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (mAudioManager == null)
        {
            mAudioManager = FindObjectOfType<AudioManager>();
            if (mAudioManager == null)
            {
                Debug.LogError("AudioManager가 Scene에 없습니다!!");
                Debug.Break();
            }
        }

        mEffects = GetComponentsInChildren<CookResultEffect>();
    }

    private void OnEnable()
    {
        CookingPlayableDirector.Play();
    }

    public void StartCooking()
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

    public void EndCooking()
    {
        mAudioManager.Stop("boiling");
        mAudioManager.Stop("pouring_milk");
        ResultImage.gameObject.SetActive(true);
        ResultText.gameObject.SetActive(true);
        ResultImage.transform.localScale = Vector3.zero;
        Color prevColor = ResultText.color;
        prevColor.a = 0.0f;
        ResultText.color = prevColor;
        if (mbHasFaileDish == true)
        {
            FailurePlayableDirector.Play();
        }
        else
        {
            SuccessPlayableDirector.Play();
        }
    }

    public void StartShowingCookingResult()
    {
        ResultImage.sprite = mResultItem.itemImage;

        if (mbHasFaileDish)
        {
            ResultText.text = $"너무 난해한 레시피 마법이었어요...";
        }
        else
        {
            ResultText.text = $"맛있는 {mResultItem.itemName}을 만들었다!!";
        }
    }

    public void EndShowingCookingResult()
    {
        foreach (var effect in mEffects)
        {
            effect.gameObject.SetActive(false);
        }

        ResultImage.gameObject.SetActive(false);
        ResultText.gameObject.SetActive(false);
        gameObject.SetActive(false);

        IPauseManager.SetPausable(true);
        GameStateManager.ChangeState(GameState.IDLE);

        CookScene.SetActive(true);
        PauseCookManager.ChangePause(true);
    }

    public void OnResultReveal(bool isSuccess)
    {
        foreach (var effect in mEffects)
        {
            effect.bIsSuccess = isSuccess;

            float randomValueForRadius = Random.value;
            float randomRadius = (EffectOutRadius - EffectInnerRadius) * randomValueForRadius + EffectInnerRadius;

            float randomValueForRadian = Random.value;
            float randomRadian = randomValueForRadian * 2.0f * Mathf.PI - Mathf.PI;

            effect.Position = new Vector2(randomRadius * Mathf.Cos(randomRadian), randomRadius * Mathf.Sin(randomRadian));
        }
    }
}
