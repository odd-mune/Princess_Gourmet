using System.Collections;
using System.Collections.Generic;
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

    public PlayableDirector CookingPlayableDirector;
    public PlayableDirector SuccessPlayableDirector;
    public PlayableDirector FailurePlayableDirector;

    private InventoryItem mResultItem;
    private bool mbHasFaileDish;

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
        ResultImage.gameObject.SetActive(false);
        ResultText.gameObject.SetActive(false);
        gameObject.SetActive(false);

        IPauseManager.SetPausable(true);
        GameStateManager.ChangeState(GameState.IDLE);

        CookScene.SetActive(true);
        PauseCookManager.ChangePause(true);
    }
}
