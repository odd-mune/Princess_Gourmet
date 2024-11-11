using UnityEngine;
using UnityEngine.UI;

public enum HudType
{
    Crafting,
    Inventory,
    Option,
}

public class HudButton : MonoBehaviour
{
    [Tooltip("연결할 HUD")]
    public HudType HudType;
    private Button mButton;
    private PlayerManager mPlayerManager;

    private PauseCookManager mPauseCookManager;
    private PauseInventoryManager mPauseInventoryManager;
    private PauseManager mPauseManager;

    // Start is called before the first frame update
    void Start()
    {
        mPauseCookManager = FindObjectOfType<PauseCookManager>();
        if (mPauseCookManager == null)
        {
            Debug.LogError("PauseCookManager가 없습니다!!");
            Debug.Break();
        }

        mPauseInventoryManager = FindObjectOfType<PauseInventoryManager>();
        if (mPauseInventoryManager == null)
        {
            Debug.LogError("PauseInventoryManager가 없습니다!!");
            Debug.Break();
        }

        mPauseManager = FindObjectOfType<PauseManager>();
        if (mPauseManager == null)
        {
            Debug.LogError("PauseManager가 없습니다!!");
            Debug.Break();
        }

        mButton = GetComponent<Button>();
        if (mButton == null)
        {
            Debug.LogError("현재 GameObject에 Button Component가 있어야 합니다!! 추가해주세요!!");
            Debug.Break();
        }

        mButton.onClick.AddListener(onClick);
    }

    private void onClick()
    {
        switch (HudType)
        {
            case HudType.Crafting:
                mPauseCookManager.ChangePause(false);
                break;
            case HudType.Inventory:
                mPauseInventoryManager.ChangePause(false);
                break;
            case HudType.Option:
                mPauseManager.ChangePause(false);
                break;
            default:
                Debug.LogError($"Invalid HUD type {HudType}");
                Debug.Break();
                break;
        }
    }
}
